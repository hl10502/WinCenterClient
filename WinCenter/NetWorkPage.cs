﻿using WizardLib;
using System.Windows.Forms;
using Wizard.Core;
using System.Collections.Generic;
using WizardLib.Core;
using WinAPI;
using System.ComponentModel;
using System;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Threading;

namespace Wizard {
	public partial class NetWorkPage : InteriorWizardPage {
		
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		
		private readonly Regex MacRegex = new Regex(@"^([0-9a-fA-F]{2}:){5}[0-9a-fA-F]{2}$");

		private PIFOper pifOper = new PIFOper();
		private bool IsTestPing = false;
		private OpaqueCommand cmd;
		private bool isFirst = true;

		public NetWorkPage() {
			InitializeComponent();
			cmd = new OpaqueCommand();
		}

		protected override bool OnSetActive() {
			if (!base.OnSetActive())
				return false;

			log.InfoFormat("进入【网络配置】页面");
			InfoLabel.Text = "";
			getNetwork();
			Wizard.SetWizardButtons(WizardButton.Back | WizardButton.Next);
			return true;
		}

		protected override string OnWizardNext() {
			if (!pingButton.Enabled) {
				InfoLabel.Text = "正在测试Ping连接，请稍候！";
				return null;
			}

			InfoLabel.Text = "";
			if (ipBox1.textBox4.Text == "") {
				ipBox1.textBox4.Focus();
				InfoLabel.Text = "请输入IP地址最后一位！";
				return null;
			}

			string ip = ipBox1.Value;
			if (ip.Equals(ConnectManager.TargetHostName)) {
				InfoLabel.Text = "IP地址不能与目标主机相同！";
				return null;
			}

			IsTestPing = false;
			doPingConnect();
			return null;
		}

		private void getNetwork() {
			pifOper = new PIFOper();
			Dictionary<string, string> dict = pifOper.getNetworkInfo(ConnectManager.TargetHost.uuid);
			string ip = "";
			dict.TryGetValue("ip", out ip);
			ipBox1.Value = ip;

			ipBox1.textBox1.Enabled = false;
			ipBox1.textBox2.Enabled = false;
			ipBox1.textBox3.Enabled = false;
			//ipBox1.textBox4.Select(0, 0); //取消选中所有文本

			string netmask = "";
			dict.TryGetValue("netmask", out netmask);
			ipBox2.Value = netmask;
			ipBox2.Enabled = false;

			string gateway = "";
			dict.TryGetValue("gateway", out gateway);
			ipBox3.Value = gateway;
			ipBox3.Enabled = false;

			if (ConnectManager.IPMaskGatewayDict != null && ConnectManager.IPMaskGatewayDict.ContainsKey("ip")) {
				string manIp = "";
				ConnectManager.IPMaskGatewayDict.TryGetValue("ip", out manIp);
				if (ip.Substring(0, ip.LastIndexOf(".")).Equals(manIp.Substring(0, manIp.LastIndexOf(".")))) {
					ipBox1.Value = manIp;
				}
			}

			Dictionary<string, string> IPMaskGatewayDict = new Dictionary<string, string>();
			IPMaskGatewayDict.Add("ip", ipBox1.Value);
			IPMaskGatewayDict.Add("netmask", ipBox2.Value);
			IPMaskGatewayDict.Add("gateway", ipBox3.Value);
			ConnectManager.IPMaskGatewayDict = IPMaskGatewayDict;

		}

		private void pingButton_Click(object sender, EventArgs e) {
			IsTestPing = true;
			if (ipBox1.textBox4.Text == "") {
				ipBox1.textBox4.Focus();
				InfoLabel.Text = "请输入IP地址最后一位！";
				return;
			}
			InfoLabel.Text = "";
			doPingConnect();
		}

		private void doPingConnect() {
			pingButton.Enabled = false;

			ThreadStart threadStart = new ThreadStart(pingConnect);
			Thread thread = new Thread(threadStart);
			thread.Start();

			cmd.ShowOpaqueLayer(groupBox1, 125, true);
		}

		private void pingConnect() {
			string ip = ipBox1.Value;
			Ping p = new Ping();
			PingReply reply = p.Send(ip);
			ConnFinished(reply.Status.ToString());
		}

		private delegate void changeText(string status);
		public void ConnFinished(string status) {
			if (this.InvokeRequired) {
				this.BeginInvoke(new changeText(ConnFinished), status);
			}
			else {
				pingButton.Enabled = true;
				cmd.HideOpaqueLayer();
				if (IPStatus.Success.ToString().Equals(status)) {
					InfoLabel.Text = "IP地址已被使用，请输入其他IP！";
				}
				else {
					InfoLabel.Text = "此IP地址可以使用！";
					if (Wizard != null) {
						if (!IsTestPing) {
							foreach (WizardPage page in Wizard.m_pages) {
								if ("NetWorkPage" == page.Name) {
									setNetwork();
									Wizard.ActivatePage(Wizard.m_pages.IndexOf(page) + 1);
									break;
								}
							}
						}
					}
				}
				
			}
		}

		private void setNetwork() {
			NetworkOper networkOper = new NetworkOper();
			VIF vif = new VIF();
			vif.device = "0";
			vif.MAC = "";
			vif.MAC_autogenerated = true;
			networkOper.setVIF(vif, pifOper.ManageNetwork);

			string ip = ipBox1.Value;
			string netmask = ipBox2.Value;
			string gateway = ipBox3.Value;
			Dictionary<string, string> IPMaskGatewayDict = new Dictionary<string, string>();
			IPMaskGatewayDict.Add("ip", ip);
			IPMaskGatewayDict.Add("netmask", netmask);
			IPMaskGatewayDict.Add("gateway", gateway);
			ConnectManager.IPMaskGatewayDict = IPMaskGatewayDict;
			log.InfoFormat("=======已设置网络start======");
			log.InfoFormat("ip: {0}", ip);
			log.InfoFormat("netmask: {0}", netmask);
			log.InfoFormat("gateway: {0}", gateway);
			log.InfoFormat("=======已设置网络end======");
		}

	}
}