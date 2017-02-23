using System;
using WinAPI;
using System.Collections.Generic;

namespace Wizard.Core {
	class SROper {

		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private List<SR> sRList = new List<SR>();
		
		public SROper() { 
			
		}

		public List<Dictionary<string, string>> getStorageRepositories() {
			List<Dictionary<string, string>> srList = new List<Dictionary<string, string>>();
			List<XenRef<SR>> srRefs = SR.get_all(ConnectManager.session);
			foreach (XenRef<SR> srRef in srRefs) {
				SR sr = SR.get_record(ConnectManager.session, srRef);

				log.InfoFormat("Name: {0}", sr.name_label);
				log.InfoFormat("Description: {0}", sr.name_description);
				log.InfoFormat("Usage: {0:0.00}GB / {1:0.0}GB", sr.physical_utilisation / Constants.UINT, sr.physical_size / Constants.UINT);

				Dictionary<string, string> dict = new Dictionary<string, string>();
				string value = sr.name_label + "  可用" + (sr.physical_utilisation / Constants.UINT) + "GB，总共" + sr.physical_size / Constants.UINT + "GB";
				dict.Add(sr.uuid, value);
				srList.Add(dict);
			}
			return srList;
		}

		public List<SRVo> getSRByTargetHost() {
			HostOper hostOper = new HostOper();
			Host targetHost = hostOper.getTargetHost();

			log.InfoFormat("=======开始获取存储池======");
			List<SRVo> srList = new List<SRVo>();
			List<XenRef<SR>> srRefs = SR.get_all(ConnectManager.session);
			foreach (XenRef<SR> srRef in srRefs) {
				SR sr = SR.get_record(ConnectManager.session, srRef);

				//五种类型 nfs ext lvm lvmhba lvmoiscsi
				if ("nfs".Equals(sr.type) || "ext".Equals(sr.type) || (!String.IsNullOrEmpty(sr.type) && sr.type.IndexOf("lvm") >= 0)) {
					if ("lvm".Equals(sr.type) || "ext".Equals(sr.type)) {
						bool isLocalHost = false;
						List<XenRef<PBD>> pbdRefs = sr.PBDs;
						if (pbdRefs != null && pbdRefs.Count > 0) {
							foreach (XenRef<PBD> pbdRef in pbdRefs) {
								PBD pbd = PBD.get_record(ConnectManager.session, pbdRef);
								if (pbd.currently_attached) {
									Host host = Host.get_record(ConnectManager.session, pbd.host.opaque_ref);
									if (targetHost.uuid.Equals(host.uuid)) {
										isLocalHost = true;
										break;
									}
								}
							}
						}

						if (isLocalHost) {
							string info = "";
							if ((sr.physical_size - sr.physical_utilisation) < (long)ConnectManager.DiskCapacity) {
								info = string.Format("本地存储池[{0}]可用容量不足", sr.name_label);
							}
							srList.Add(AddSRData(sr, info));
						}
						else {
							log.InfoFormat("本地存储池未连接主机. sr=[uuid: {0}, Name: {1}, type: {2}, lave: {3:0.00}GB / {4:0.00}GB]", sr.uuid, sr.name_label, sr.type,
								(sr.physical_size - sr.physical_utilisation) / Constants.UINT, sr.physical_size / Constants.UINT);
						}
					}
					else {
						string info = "";
						if ((sr.physical_size - sr.physical_utilisation) < (long)ConnectManager.DiskCapacity) {
							info = string.Format("共享存储池[{0}]可用容量不足", sr.name_label);
							log.InfoFormat("{0}. sr=[uuid: {1}, Name: {2}, type: {3}, lave: {4:0.00}GB / {5:0.00}GB]", info, sr.uuid, sr.name_label, sr.type,
									(sr.physical_size - sr.physical_utilisation) / Constants.UINT, sr.physical_size / Constants.UINT);
						}
						List<XenRef<PBD>> pbdRefs = sr.PBDs;
						if (pbdRefs != null && pbdRefs.Count > 0) {
							bool isCurrentlyAttached = true;
							foreach (XenRef<PBD> pbdRef in pbdRefs) {
								PBD pbd = PBD.get_record(ConnectManager.session, pbdRef);
								if (!pbd.currently_attached) {
									isCurrentlyAttached = false;
									if (!string.IsNullOrEmpty(info)) {
										info += ",连接主机PBD异常";
									}
									else {
										info = string.Format("共享存储池[{0}]连接主机PBD异常", sr.name_label);
									}
									log.InfoFormat("{0}. pbd=[uuid:{1}], sr=[uuid: {2}, Name: {3}, type: {4}, lave: {5:0.00}GB / {6:0.00}GB]", info, pbd.uuid, sr.uuid, sr.name_label, sr.type,
										(sr.physical_size - sr.physical_utilisation) / Constants.UINT, sr.physical_size / Constants.UINT);
								}
							}
							if (isCurrentlyAttached) {
								//srList.Add(AddSRData(sr));
								//WriteLog(sr);
							}
						}
						else {
							if (!string.IsNullOrEmpty(info)) {
								info += ",未连接主机";
							}
							else {
								info = string.Format("共享存储池[{0}]未连接主机", sr.name_label);
							}
							log.InfoFormat("{0}. sr=[uuid: {1}, Name: {2}, type: {3}, lave: {4:0.00}GB / {5:0.00}GB]", info, sr.uuid, sr.name_label, sr.type,
									(sr.physical_size - sr.physical_utilisation) / Constants.UINT, sr.physical_size / Constants.UINT);
						}

						srList.Add(AddSRData(sr, info));
					}
				}
				else {
					log.InfoFormat("存储池类型不是五种类型nfs/ext/lvm/lvmhba/lvmoiscsi中的一种. sr=[uuid: {0}, Name: {1}, type: {2}, lave: {3:0.00}GB / {4:0.00}GB]", sr.uuid, sr.name_label, sr.type,
						(sr.physical_size - sr.physical_utilisation) / Constants.UINT, sr.physical_size / Constants.UINT);
				}
				//if ((sr.physical_size - sr.physical_utilisation) >= (long)ConnectManager.DiskCapacity) {
				//}
				//else {
				//    log.InfoFormat("存储池可用大小小于需要的存储容量. sr=[uuid: {0}, Name: {1}, type: {2}, lave: {3:0.00}GB / {4:0.00}GB]", sr.uuid, sr.name_label, sr.type,
				//            (sr.physical_size - sr.physical_utilisation) / Constants.UINT, sr.physical_size / Constants.UINT);
				//}
			}

			log.InfoFormat("=======获取存储池结束======");
			return srList;
		}


		private SRVo AddSRData(SR sr, string info) {
			sRList.Add(sr);
			SRVo sRVo = new SRVo();
			sRVo.Uuid = sr.uuid;
			sRVo.Name = sr.name_label;
			sRVo.AvailableCapacity = sr.physical_size - sr.physical_utilisation; 
			sRVo.PhysicalSize = sr.physical_size; 
			sRVo.Type = sr.type;
			if (!string.IsNullOrEmpty(info)) {
				sRVo.Info = info;
			}
			else {
				WriteLog(sr);
			}
			return sRVo;
		}

		private void WriteLog(SR sr) {
			log.InfoFormat("****************存储池uuid={0}满足条件start", sr.uuid);
			log.InfoFormat("uuid: {0}", sr.uuid);
			log.InfoFormat("Name: {0}", sr.name_label);
			log.InfoFormat("Description: {0}", sr.name_description);
			log.InfoFormat("content_type: {0}", sr.content_type);
			log.InfoFormat("type: {0}", sr.type);
			log.InfoFormat("Usage: {0:0.00}GB / {1:0.0}GB", sr.physical_utilisation / Constants.UINT, sr.physical_size / Constants.UINT);
			log.InfoFormat("lave: {0:0.00}GB", (sr.physical_size - sr.physical_utilisation) / Constants.UINT);
			log.InfoFormat("****************存储池uuid={0}满足条件end", sr.uuid);
		}


		public string getAeISORepoUuid() {
			string aeIsoRepoUuid = null;
			List<XenRef<SR>> srRefs = SR.get_by_name_label(ConnectManager.session, Constants.AE_ISO_REPO_NAME);
			foreach(XenRef<SR> srRef in srRefs) {
				SR sr = SR.get_record(ConnectManager.session, srRef);
				Dictionary<string, string> otherConfig = sr.other_config;
				if (!otherConfig.ContainsKey(Constants.AE_ISO_REPO_HOST_KEY)
						|| !otherConfig.ContainsKey(Constants.AE_ISO_REPO_OTHER_KEY)) {
					continue;
				}

				string hostUuid = "";
				otherConfig.TryGetValue(Constants.AE_ISO_REPO_HOST_KEY, out hostUuid);
				string author = "";
				otherConfig.TryGetValue(Constants.AE_ISO_REPO_OTHER_KEY, out author);

				if (ConnectManager.TargetHost.uuid.Equals(hostUuid) && Constants.AE_ISO_REPO_OTHER_VALUE.Equals(author)) {
					aeIsoRepoUuid = sr.uuid;
					break;
				}
			}
			
			if (string.IsNullOrEmpty(aeIsoRepoUuid)) {
				//如果iso sr不存在需要新建ISO
				aeIsoRepoUuid = createISOSr();
				log.InfoFormat("AE ISO SR不存在,已创建新的ISO,srUuid={0}", aeIsoRepoUuid);
			}

			scanSr(aeIsoRepoUuid);
			return aeIsoRepoUuid;
		}

		private string createISOSr() {
			string aeIsoRepoUuid = null;
			try {
				string hostUuid = ConnectManager.TargetHost.uuid;
				XenRef<Host> hostRef = Host.get_by_uuid(ConnectManager.session, hostUuid);

				Dictionary<string, string> deviceConfig = new Dictionary<string, string>();
				deviceConfig.Add("location", Constants.AE_ISO_REPO_LOCATION);
				deviceConfig.Add("legacy_mode", "true");
				Dictionary<string, string> smConfig = new Dictionary<string, string>();

				XenRef<SR> srRef = SR.create(ConnectManager.session, hostRef, deviceConfig, 0, Constants.AE_ISO_REPO_NAME,
					Constants.AE_ISO_REPO_LOCATION, "iso", "iso", false, smConfig);

				Dictionary<string, string> otherConfig = new Dictionary<string, string>();
				otherConfig.Add(Constants.AE_ISO_REPO_OTHER_KEY, Constants.AE_ISO_REPO_OTHER_VALUE);
				otherConfig.Add(Constants.AE_ISO_REPO_HOST_KEY, hostUuid);
				SR.set_other_config(ConnectManager.session, srRef, otherConfig);

				aeIsoRepoUuid = SR.get_uuid(ConnectManager.session, srRef);
			}
			catch (Exception e) {
				log.ErrorFormat("创建ISO失败: {0}" + e.Message);
			}
			return aeIsoRepoUuid;
		}

		public void scanSr(string srUuid) {
			XenRef<SR> srRef = SR.get_by_uuid(ConnectManager.session, srUuid);
			SR.scan(ConnectManager.session, srRef);
		}

		public XenRef<VDI> getVdiRef(string srUuid, string fileName) {
			XenRef<SR> srRef = SR.get_by_uuid(ConnectManager.session, srUuid);
			SR sr = SR.get_record(ConnectManager.session, srRef);
			List<XenRef<VDI>> vdiRefs = sr.VDIs;
			foreach (XenRef<VDI> vdiRef in vdiRefs) {
				VDI vdi = VDI.get_record(ConnectManager.session, vdiRef);
				string isoFileName = VDI.get_location(ConnectManager.session, vdiRef);
				if (fileName.Equals(isoFileName)) {
					return vdiRef;
				}
			}
			return null;
		}


		public SR getSrByUuid(string uuid) {
			foreach (SR sr in sRList) {
				if (uuid.Equals(sr.uuid)) {
					return sr;
				}
			}
			return null;
		}
		

		public ulong DiskCapacity {
			set {
				ConnectManager.DiskCapacity = value;
			}
			get {
				return ConnectManager.DiskCapacity;
			}
		}

		public int VifCount {
			set {
				ConnectManager.VifCount = value;
			}
			get {
				return ConnectManager.VifCount;
			}
		}

		public int VcpuCount {
			set {
				ConnectManager.VcpuCount = value;
			}
			get {
				return ConnectManager.VcpuCount;
			}
		}

		public ulong Memory {
			set {
				ConnectManager.Memory = value;
			}
			get {
				return ConnectManager.Memory;
			}
		}

		public int MaxVIFsAllowed {
			set {
				ConnectManager.MaxVIFsAllowed = value;
			}			
		}

		public string vmNameLabel {
			set {
				ConnectManager.VMName = value;
			}
		}
	}
}
