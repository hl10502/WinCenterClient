using System;
using System.Collections.Generic;
using WinAPI;
using System.Xml;
using Tamir.SharpSsh;
using System.IO;
using Renci.SshNet;
using System.Threading;

namespace Wizard.Core {
	class VMOper {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private const int HTTP_PUT_TIMEOUT = 3 * 60 * 60 * 1000; //3 hours
		private List<VM> VMs;


		public VMOper() {

		}

		public void getVMs() {
			VMs = new List<VM>();
			List<XenRef<VM>> vmRefs = VM.get_all(ConnectManager.session);
			foreach (XenRef<VM> vmRef in vmRefs) {
				VM vm = VM.get_record(ConnectManager.session, vmRef);
				VMs.Add(vm);
			}
		}

		public void ImpXvaFile() {
			log.InfoFormat("开始导入WinCenter xva文件:{0}", ConnectManager.FilePath);
			string vmRef = GetVmRef(applyFile());
			if (string.IsNullOrEmpty(vmRef)) {
				return;
			}
			log.InfoFormat("WinCenter xva文件导入完成");

			try {
				log.InfoFormat("导入后的WinCenter VM:[{0}]", vmRef);
				List<XenRef<VIF>> vifRefs = VM.get_VIFs(ConnectManager.session, vmRef);
				foreach (XenRef<VIF> vifRef in vifRefs) {
					log.InfoFormat("删除原有的VIF:[{0}]", vifRef.opaque_ref);
					VIF.destroy(ConnectManager.session, vifRef);
				}
				HTTPHelper.progressInfo = "删除原有VIF";
				HTTPHelper.progressPercent += 1;

				bool isTemplate = VM.get_is_a_template(ConnectManager.session, vmRef);
				if (isTemplate) {
					log.InfoFormat("导入的xva是模板，删除VM:[{0}]", vmRef);
					VM.destroy(ConnectManager.session, vmRef);
					return;
				}

				VIF newVif = ConnectManager.VIF;
				newVif.VM = new XenRef<VM>(vmRef);
				XenRef<VIF> newVifRef = VIF.create(ConnectManager.session, newVif);
				log.InfoFormat("重新创建VIF:[{0}]", newVifRef.opaque_ref);
				string mac = VIF.get_MAC(ConnectManager.session, newVifRef);
				log.InfoFormat("新的MAC地址为:[{0}]", mac);
				string newVifUuid = VIF.get_uuid(ConnectManager.session, newVifRef);
				log.InfoFormat("新的VIF UUID为:[{0}]", newVifUuid);

				HTTPHelper.progressInfo = "创建新的VIF";
				HTTPHelper.progressPercent += 1;

				DefaultVMName(ConnectManager.VMName);
				log.InfoFormat("检查WinCenter VM的名称，VM名称为:{0}", ConnectManager.VMName);
				VM.set_name_label(ConnectManager.session, vmRef, ConnectManager.VMName);
				HTTPHelper.progressInfo = "检查VM名称";
				HTTPHelper.progressPercent += 1;

				/**
				 * BUG：AE方式设置IP之后，由于ISO没有弹出，在控制台修改IP重启以后，AE会重新设置ISO配置中的IP
				 * 故不再使用AE这种方式，而是改成在VM启动后使用xenstore-write方式动态修改IP
				 */
				//初始化AE
				//initVmByAe(vmRef, mac);

				XenRef<Host> hostRef = Host.get_by_uuid(ConnectManager.session, ConnectManager.TargetHost.uuid);
				log.InfoFormat("设置WinCenter VM的所属主机:[{0}]", ConnectManager.TargetHostName);
                HTTPHelper.progressInfo = "设置主机";
                VM.set_affinity(ConnectManager.session, vmRef, hostRef);
				HTTPHelper.progressPercent += 1;

				setAutoPoweron(vmRef);
                HTTPHelper.progressInfo = "启动VM";
				VM.start(ConnectManager.session, vmRef, false, false);
				Thread.Sleep(2 * 60 * 1000); //休眠2分钟，等待虚拟机启动完成
				HTTPHelper.progressPercent += 2;
				log.InfoFormat("启动WinCenter VM:[{0}]成功", vmRef);

				//去掉AE方式设置IP，VM启动后使用xenstore-write方式动态设置IP
                HTTPHelper.progressInfo = "设置网络信息";
				setActiveVifIp(newVifUuid);
				HTTPHelper.progressPercent = 100;
				log.InfoFormat("设置IP信息成功");
			}
			catch (Exception ex) {
				log.ErrorFormat("安装失败: {0}", ex.Message);
				log.ErrorFormat("开始删除WinCenter VM:[{0}]", vmRef);
				try {
					vm_power_state power_state = VM.get_power_state(ConnectManager.session, vmRef);
					if (!vm_power_state.Halted.ToString().Equals(power_state.ToString())) {
						VM.shutdown(ConnectManager.session, vmRef);
					}
				}
				catch (Exception ex1) {
					log.ErrorFormat("WinCenter VM关机失败:{0}", ex1.Message);
				}

				try {
					VM.destroy(ConnectManager.session, vmRef);
				}
				catch (Exception ex1) {
					log.ErrorFormat("删除WinCenter VM失败:{0}", ex1.Message);
					throw ex1;
				}
				throw;
			}
		}

		private string applyFile() {
			log.InfoFormat("安装WinCenter VM从[{0}]到存储池{1}", ConnectManager.FilePath, ConnectManager.SelectedSR.name_label);

			Host host = null;
			if (!ConnectManager.SelectedSR.shared) {
				host = ConnectManager.TargetHost;
			}

			string hostURL;
			if (host == null) {
				Uri uri = new Uri(ConnectManager.session.Url);
				hostURL = uri.Host;
			}
			else {
				log.InfoFormat("导存储池不是共享存储，直接导入到目标主机:[{0}]", host.address);
				hostURL = host.address;
			}

            //添加port
            int port = HTTP.DEFAULT_HTTP_PORT;
            if (ConnectManager.ConnectPort != 0)
            {
                port = ConnectManager.ConnectPort;
            }
            
            hostURL = hostURL + ":" + port; //ip:port

			log.InfoFormat("导入URL:{0}", hostURL);
			XenRef<SR> srRef = SR.get_by_uuid(ConnectManager.session, ConnectManager.SelectedSR.uuid);

			return HTTPHelper.Put(HTTP_PUT_TIMEOUT, ConnectManager.FilePath, hostURL,
								  (HTTP_actions.put_ssbbs)HTTP_actions.put_import,
								  ConnectManager.session.uuid, false, false, srRef.opaque_ref);
		}

		private string GetVmRef(string result) {
			if (string.IsNullOrEmpty(result))
				return null;

			string head = "<value><array><data><value>";
			string tail = "</value></data></array></value>";

			if (!result.StartsWith(head) || !result.EndsWith(tail))
				return null;

			int start = head.Length;
			int length = result.IndexOf(tail) - start;

			return result.Substring(start, length);
		}

		public void DefaultVMName(string vmName) {
			getVMs();
			string name = vmName;
			int i = 0;
			while (VMsWithName(name) > 1) {
				i++;
				name = string.Format("{0} ({1})", vmName, i);
			}
			ConnectManager.VMName = name;
		}


		private int VMsWithName(string name) {
			int i = 0;

			foreach (VM v in VMs)
				if (v.name_label == name)
					i++;

			return i;
		}


		private void initVmByAe(string vmRef, string mac) {
			Dictionary<string, string> aeDict = new Dictionary<string, string>();
			setDictValue(aeDict, mac);
			string xmlString = GenerateXMLInfo(aeDict);
			log.InfoFormat("组装AE配置文件，xml:{0}", xmlString);
			HTTPHelper.progressInfo = "组装AE配置文件";
			HTTPHelper.progressPercent += 1;

			DateTime dt = System.DateTime.Now;
			string dateTime = string.Format("{0:yyyyMMddHHmmssffff}", dt);
			string aeFileName = "Sharp_AE_" + dateTime + ".iso";
			mkisofsByRenciSshNet(xmlString, aeFileName);
			//mkisofsBySharpSSH(xmlString, aeFileName);
			insertAEISO(vmRef, aeFileName);
		}

		/**
		 SSH连接使用RenciSshNet，需要.net4.0
		 */
		private void mkisofsByRenciSshNet(string xmlString, string aeFileName) {
			ConnectionInfo connectionInfo = new ConnectionInfo(ConnectManager.TargetHostName, Constants.SSHPort, ConnectManager.TargetHostUserName,
					new AuthenticationMethod[]{

						// Pasword based Authentication
						new PasswordAuthenticationMethod(ConnectManager.TargetHostUserName, ConnectManager.TargetHostPassword),

						// Key Based Authentication (using keys in OpenSSH Format)

						/**
						 new PrivateKeyAuthenticationMethod("username",new PrivateKeyFile[]{ 
							new PrivateKeyFile(@"..\openssh.key","passphrase")
						}),
						 */
					}
				);

			SshClient sshclient = new SshClient(connectionInfo);
			try {
				sshclient.Connect();
				//在winsever上新建xml文件
				string cmdStr = "echo '" + xmlString + "' > " + Constants.AE_XML_TMP_FILE;
				var cmd = sshclient.CreateCommand(cmdStr);
				cmd.Execute();
				log.InfoFormat("目标主机上新建AE文件，执行命令{0}", cmdStr);
				string info = cmd.Result;//获取返回结果
				string error = cmd.Error;//获取错误信息
				log.InfoFormat("目标主机上新建AE文件，执行命令输出{0}", info + error);
				HTTPHelper.progressInfo = "新建AE配置文件";
				HTTPHelper.progressPercent += 1;

				cmdStr = "ls " + Constants.AE_ISO_REPO_LOCATION;
				cmd = sshclient.CreateCommand(cmdStr);
				cmd.Execute();
				log.InfoFormat("目标主机上校验AE ISO库目录，执行命令{0}", cmdStr);
				info = cmd.Result;
				error = cmd.Error;
				log.InfoFormat("目标主机上校验AE ISO库目录，执行命令输出{0}", info + error);
				if (!string.IsNullOrWhiteSpace(error) && error.IndexOf("No such file or directory") != -1) {
					cmdStr = "mkdir -p " + Constants.AE_ISO_REPO_LOCATION;
					cmd = sshclient.CreateCommand(cmdStr);
					cmd.Execute();
					log.InfoFormat("目标主机上创建AE ISO库目录，执行命令{0}", cmdStr);
					info = cmd.Result;
					error = cmd.Error;
					log.InfoFormat("目标主机上创建AE ISO库目录，执行命令输出{0}", info + error);
				}

				//制作ISO文件
				cmdStr = "mkisofs -r -o " + Constants.AE_ISO_REPO_LOCATION + "/" + aeFileName + " " + Constants.AE_XML_TMP_FILE;
				cmd = sshclient.CreateCommand(cmdStr);
				cmd.Execute();
				log.InfoFormat("目标主机上根据AE文件制作ISO，执行命令{0}", cmdStr);
				info = cmd.Result;
				error = cmd.Error;
				log.InfoFormat("目标主机上根据AE文件制作ISO，执行命令输出{0}", info + error);
				HTTPHelper.progressInfo = "制作AE ISO文件";
				HTTPHelper.progressPercent += 1;
			}
			catch (Exception ex) {
				log.ErrorFormat("制作ISO失败：" + ex.Message);
				throw;
			}
			finally {
				//删除临时文件
				if (sshclient != null) {
					string cmdStr = "rm -f " + Constants.AE_XML_TMP_FILE;
					sshclient.CreateCommand(cmdStr).Execute();
					log.InfoFormat("删除AE临时文件，执行命令{0}", cmdStr);
					log.InfoFormat("删除AE临时文件成功");
					sshclient.Disconnect();
				}
			}
		}

		/**
		 * @deprecated
		 * SSH连接使用SharpSSH，需要.net2.0
		 * SharpSSH版本比较旧，一直未更新，does not support modern ciphers and KEX algorithms
		 * 就会报这个Algorithm negotiation fail错误
		 */
		private void mkisofsBySharpSSH(string xmlString, string aeFileName) {
			////获取启动了应用程序的可执行文件的路径目录
			//string startupPath = System.Windows.Forms.Application.StartupPath;
			//string privateKeyPath = startupPath + Path.DirectorySeparatorChar + ".ssh" + Path.DirectorySeparatorChar + Constants.IDENTITY_FILENAME;
			//if (!File.Exists(privateKeyPath)) {
			//    log.InfoFormat("SSH连接所需私钥文件[{0}]不存在", privateKeyPath);
			//}

			ShellHelp shell = new ShellHelp();
			try {
				if (shell.OpenShell(ConnectManager.TargetHostName, ConnectManager.TargetHostUserName, ConnectManager.TargetHostPassword, null)) {
					//在winsever上新建xml文件
					shell.Shell("echo '" + xmlString + "' > " + Constants.AE_XML_TMP_FILE);
					string info = shell.GetAllString();//获取返回结果
					log.InfoFormat("目标主机上新建AE文件，命令输出{0}", info);
					HTTPHelper.progressInfo = "新建AE配置文件";
					HTTPHelper.progressPercent += 1;

					shell.Shell("ls " + Constants.AE_ISO_REPO_LOCATION);
					info = shell.GetAllString();
					log.InfoFormat("目标主机上执行ls命令输出{0}", info);
					if (info.IndexOf("No such file or directory") != -1) {
						shell.Shell("mkdir -p " + Constants.AE_ISO_REPO_LOCATION);
					}

					//制作ISO文件
					shell.Shell("mkisofs -r -o " + Constants.AE_ISO_REPO_LOCATION + "/" + aeFileName + " " + Constants.AE_XML_TMP_FILE);
					info = shell.GetAllString();
					log.InfoFormat("目标主机上根据AE文件制作ISO，命令输出{0}", info);
					HTTPHelper.progressInfo = "制作AE ISO文件";
					HTTPHelper.progressPercent += 1;
				}
			}
			catch (Exception ex) {
				log.ErrorFormat("制作ISO失败：" + ex.Message);
				throw;
			}
			finally {
				//删除临时文件
				shell.Shell("rm -f " + Constants.AE_XML_TMP_FILE);
				log.InfoFormat("删除AE临时文件成功");
				shell.Close();
			}
		}

		private void insertAEISO(string vmRef, string aeFileName) {
			HTTPHelper.progressInfo = "获取AE ISO库";
			HTTPHelper.progressPercent += 1;
			SROper srOper = new SROper();
			string aeIsoRepoUuid = srOper.getAeISORepoUuid();

			XenRef<VBD> cdVBDRef = null;
			VM vm = VM.get_record(ConnectManager.session, vmRef);
			List<XenRef<VBD>> vbdRefs = vm.VBDs;
			foreach (XenRef<VBD> vbdRef in vbdRefs) {
				VBD vbd = VBD.get_record(ConnectManager.session, vbdRef);
				if (vbd_type.CD.Equals(vbd.type)) {
					cdVBDRef = vbdRef;
					try {
						VBD.eject(ConnectManager.session, vbdRef);
					}
					catch (Exception ex) {
						log.InfoFormat("设置VBD eject, error=[{0}]", ex.Message);
					}
					break;
				}
			}

			if (cdVBDRef == null) {
				string[] userdevices = VM.get_allowed_VBD_devices(ConnectManager.session, vmRef);
				if (userdevices != null && userdevices.Length > 0) {
					VBD vbd = new VBD();
					vbd.VM = new XenRef<VM>(vmRef);
					vbd.type = vbd_type.CD;
					vbd.mode = vbd_mode.RO;
					vbd.userdevice = userdevices[0];
					vbd.empty = true;
					cdVBDRef = VBD.create(ConnectManager.session, vbd);
					log.InfoFormat("WinCenter VM没有虚拟光驱，新建一个VBD成功");
				}
				else {
					log.InfoFormat("可供使用的VBD标识符列表数为0，不能创建VBD");
				}
			}

			XenRef<VDI> vdiRef = srOper.getVdiRef(aeIsoRepoUuid, aeFileName);
			VBD cdVBD = VBD.get_record(ConnectManager.session, cdVBDRef);
			bool isInsert = VBD.get_allowed_operations(ConnectManager.session, cdVBDRef).Contains(vbd_operations.insert);
			if (isInsert) {
				log.InfoFormat("加载AE ISO文件");
				VBD.insert(ConnectManager.session, cdVBDRef, vdiRef);
				HTTPHelper.progressInfo = "加载AE ISO文件";
				HTTPHelper.progressPercent += 1;
			}
			else {
				XenRef<VDI> cdVDIRef = cdVBD.VDI;
				string name = VDI.get_name_label(ConnectManager.session, cdVDIRef);
				log.InfoFormat("WinCenter VM已插入镜像[{0}]，不能插入AE镜像", name);
			}
		}


		private void setDictValue(Dictionary<string, string> aeDict, string mac) {
			foreach (KeyValuePair<string, string> net in ConnectManager.IPMaskGatewayDict) {
				if (net.Key.Equals("ip")) {
					aeDict.Add("com.huadi.ovf.wce.adapter.networking.ipv4addresses.1", net.Value);
				}
				else if (net.Key.Equals("netmask")) {
					aeDict.Add("com.huadi.ovf.wce.adapter.networking.ipv4netmasks.1", net.Value);
				}
				else if (net.Key.Equals("gateway")) {
					aeDict.Add("com.huadi.ovf.wce.system.networking.ipv4defaultgateway", net.Value);
				}
			}

			aeDict.Add("com.huadi.ovf.wce.system.networking.hostname", "wincenter"); //wincenter VM hostname
			aeDict.Add("com.huadi.ovf.wce.adapter.networking.mac.1", mac);
			aeDict.Add("com.huadi.ovf.wce.system.timezone", "");
			aeDict.Add("com.huadi.ovf.wce.adapter.networking.order.1", "");
			aeDict.Add("com.huadi.ovf.wce.system.networking.domainname", "");
			aeDict.Add("com.huadi.ovf.wce.adapter.networking.usedhcpv4.1", "false");
			aeDict.Add("com.huadi.ovf.wce.adapter.networking.useipv6autoconf.1", "false");
			aeDict.Add("com.huadi.ovf.wce.adapter.networking.ipv4hosttableentries.0", "");
			aeDict.Add("com.huadi.ovf.wce.adapter.networking.ipv6gateways.1", "");
			aeDict.Add("com.huadi.ovf.wce.adapter.networking.ipv6addresses.1", "");
			aeDict.Add("com.huadi.ovf.wce.adapter.networking.ipv6hosttableentries.1", "");
			aeDict.Add("com.huadi.ovf.wce.system.networking.dnsIPaddresses", "");
			aeDict.Add("com.huadi.ovf.wce.system.license", "");
		}

		private string GenerateXMLInfo(Dictionary<string, string> aeDict) {
			XmlDocument doc = new XmlDocument();
			XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
			doc.AppendChild(dec);

			XmlElement root = doc.CreateElement("Environment");
			doc.AppendChild(root);

			XmlElement platformSection = doc.CreateElement("PlatformSection");
			XmlElement locale = doc.CreateElement("Locale");
			locale.InnerText = "en_US";
			platformSection.AppendChild(locale);
			root.AppendChild(platformSection);

			XmlElement propertySection = doc.CreateElement("PropertySection");
			root.AppendChild(propertySection);

			foreach (KeyValuePair<string, string> ae in aeDict) {
				XmlElement property = doc.CreateElement("Property");
				property.SetAttribute("key", ae.Key);
				property.SetAttribute("value", ae.Value);
				propertySection.AppendChild(property);
			}

			return doc.OuterXml;
		}


		/**
		 * 设置虚拟机开机启动（需要在资源池和虚拟机上设置）
		 * 1、设置池开机启动 xe pool-param-set uuid=f0171d28-44e7-4444-0777-06f3b3805714 other-config:auto_poweron=true
		 * 2、设置虚拟机开启启动 xe vm-param-set uuid=af9e0b9b-b0e8-385c-eb8e-f9c2f3cbc5c5 other-config:auto_poweron=true
		 * 
		 */
		private void setAutoPoweron(String vmRef) {
			List<XenRef<Pool>> poolRefs = Pool.get_all(ConnectManager.session);
			foreach (XenRef<Pool> poolRef in poolRefs) {
				Dictionary<string, string> poolOtherConfig = Pool.get_other_config(ConnectManager.session, poolRef);
				//先判断是否含有auto_poweron，如果有的话先remove，再add（直接add会报错提示已含有相同的key）
				if (poolOtherConfig.ContainsKey("auto_poweron")) {
					poolOtherConfig.Remove("auto_poweron");
				}
				poolOtherConfig.Add("auto_poweron", "true");
				Pool.set_other_config(ConnectManager.session, poolRef, poolOtherConfig); //设置资源池开机启动
			}

			Dictionary<string, string> otherConfig = VM.get_other_config(ConnectManager.session, vmRef);
			//先判断是否含有auto_poweron，如果有的话先remove，再add（直接add会报错提示已含有相同的key）
			if (otherConfig.ContainsKey("auto_poweron")) {
				otherConfig.Remove("auto_poweron");
			}
			otherConfig.Add("auto_poweron", "true");
			VM.set_other_config(ConnectManager.session, vmRef, otherConfig); //设置虚拟机开机启动
		}


		/**
		 * 根据物理主机的product_brand、product_version来确定开放的https端口号，默认443
		 */
		private string getHostPort() {
			string productVersion = null;
			string productBrand = null;
			try {
				XenRef<Host> hostRef = Host.get_by_uuid(ConnectManager.session, ConnectManager.TargetHost.uuid);
				Dictionary<string, string> softwareVersion = Host.get_software_version(ConnectManager.session, hostRef);
				foreach (KeyValuePair<string, string> kv in softwareVersion) {
					if ("product_version".Equals(kv.Key)) {
						productVersion = kv.Value;
					}
					else if ("product_brand".Equals(kv.Key)) {
						productBrand = kv.Value;
					}
				}
				log.InfoFormat("目标主机产品信息: productVersion={0}, productBrand={1}", productVersion, productBrand);

				if (Constants.WinServer.Equals(productBrand) && !string.IsNullOrEmpty(productVersion)) {
					if (productVersion.IndexOf(".") > 0) {
						string[] productVersionArr = productVersion.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
						if (productVersionArr.Length > 0) {
							string version = productVersionArr[0].Trim();
							if (!string.IsNullOrEmpty(version)) {
								int productVersionNum = Int32.Parse(version);
								if (productVersionNum >= 2) {
									return "" + Constants.WinServerPort; //主版本号在2及以上的物理主机
								}
							}
						}
					}
				}
			}
			catch (Exception e) {
				log.ErrorFormat("获取目标主机产品版本号错误: {0}", e.Message);
			}
			return null;
		}


		/**
		 * 动态设置VM的IP信息
		 * SSH连接使用RenciSshNet，需要.net4.0
		 */
		private void setActiveVifIp(string newVifUuid) {
			ConnectionInfo connectionInfo = new ConnectionInfo(ConnectManager.TargetHostName, Constants.SSHPort, ConnectManager.TargetHostUserName,
					new AuthenticationMethod[]{

						// Pasword based Authentication
						new PasswordAuthenticationMethod(ConnectManager.TargetHostUserName, ConnectManager.TargetHostPassword),

						// Key Based Authentication (using keys in OpenSSH Format)

						/**
						 new PrivateKeyAuthenticationMethod("username",new PrivateKeyFile[]{ 
							new PrivateKeyFile(@"..\openssh.key","passphrase")
						}),
						 */
					}
				);

			SshClient sshclient = new SshClient(connectionInfo);
			try {
				sshclient.Connect();
				//动态设置IP
				/**
				 * /opt/xensource/libexec/set-active-vif-ip --vifuuid <vifuuid> [--ip <ip>] [--gateway <gateway>] [--netmask <netmask>] [--hostname <hostname>] [--ipv6 <ipv6>] [--gatewayv6 <ipv6 gateway>] [--dns <dns>] 
				 *  其中 --vifuuid <vifuuid>是必须的，其余参数可选
				 */
				string cmdStr = "/opt/xensource/libexec/set-active-vif-ip --vifuuid " + newVifUuid;

				foreach (KeyValuePair<string, string> net in ConnectManager.IPMaskGatewayDict) {
					cmdStr += " --" + net.Key + " " + net.Value; //key有三个，分别为ip,netmask,gateway

					//if (net.Key.Equals("ip")) {
					//    cmdStr= " --ip " + net.Value;
					//}
					//else if (net.Key.Equals("netmask")) {
					//    cmdStr = " --netmask " + net.Value;
					//}
					//else if (net.Key.Equals("gateway")) {
					//    cmdStr = " --gateway " + net.Value;
					//}
				}

				cmdStr += "  --hostname wincenter"; //wincenter VM hostname
				var cmd = sshclient.CreateCommand(cmdStr);
				cmd.Execute();
				log.InfoFormat("设置IP信息，执行命令{0}", cmdStr);
				string info = cmd.Result;//获取返回结果
				string error = cmd.Error;//获取错误信息
				log.InfoFormat("设置IP信息，执行命令输出{0}", info + error);
				if ((!String.IsNullOrEmpty(info) && info.IndexOf("ERROR") >= 0) || !String.IsNullOrEmpty(error)) {
					//设置IP信息输出错误信息，抛出异常
					throw new Exception(info + error);
				}
				HTTPHelper.progressInfo = "设置IP信息";
				HTTPHelper.progressPercent += 2;
			}
			catch (Exception ex) {
				log.ErrorFormat("设置IP信息失败：" + ex.Message);
				throw;
			}
			finally {
				if (sshclient != null) {
					sshclient.Disconnect();
				}
			}
		}
	}
}
