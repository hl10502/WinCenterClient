using System;
using WizardLib;
using WinAPI;
using Wizard.Core;
using System.Threading;
using System.Windows.Forms;


namespace Wizard {
	public partial class HostPage : InteriorWizardPage {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		//private bool isFirst = true;

		private string hostName;
		private string userName;
		private string password;

		private bool IsUserName = false;
		private bool IsPassword = false;

		private OpaqueCommand cmd;
		private bool IsTestConn = false;

		public HostPage() {
			InitializeComponent();
			cmd = new OpaqueCommand();
		}

		protected override bool OnSetActive() {
			if (!base.OnSetActive())
				return false;
						
			log.InfoFormat("进入【主机配置】页面");
			
			testConnectbutton.Enabled = true;
			Wizard.SetWizardButtons(WizardButton.Back | WizardButton.Next);

			//if (isFirst) {
			//    isFirst = false;
			//    testConnectbutton.Enabled = false;
			//    Wizard.SetWizardButtons(WizardButton.Back);
			//}
			//else {
			//    testConnectbutton.Enabled = true;
			//    Wizard.SetWizardButtons(WizardButton.Back | WizardButton.Next);
			//}
			return true;
		}


		protected override string OnWizardNext() {
			if (!checkInput()) {
				return null;
			}

			IsTestConn = false;
			Wizard.SetWizardButtons(WizardButton.Back);

			ThreadStart threadStart = new ThreadStart(DoConnect);
			Thread thread = new Thread(threadStart);
			thread.Start();

			cmd.ShowOpaqueLayer(panel1, 125, true);
			return null;
		}

		private bool checkInput() {
			targetIPInfoLabel.Text = "*";
			if (!this.ipBox1.IsValidate) {
				targetIPInfoLabel.Text = "请输入IP地址！";
				return false;
			}
			if (String.IsNullOrEmpty(usernameTextBox.Text)) {
				userNameInfoLabel.Text = "请输入用户名！";
				return false;
			}
			if (String.IsNullOrEmpty(passwordTextBox.Text)) {
				passInfoLabel.Text = "请输入密码！";
				return false;
			}

			return true;
		}


		private void testConnectbutton_Click(object sender, EventArgs e) {
			if (!checkInput()) {
				return;
			}

			testConnectbutton.Enabled = false;
			IsTestConn = true;
			Wizard.SetWizardButtons(WizardButton.Back);

			ThreadStart threadStart = new ThreadStart(DoConnect);
			Thread thread = new Thread(threadStart);
			thread.Start();

			cmd.ShowOpaqueLayer(panel1, 125, true);
		}


		private void DoConnect() {
			string info = "";
			ConnectManager.IsConn = false;
			try {
				bool isConn = setConnect();
				if (isConn) {
					info = "连接成功！";
					ConnectManager.IsConn = true;
				}
			}
			catch (Failure f) {
				info = f.Message;
			}
			catch (Exception ex) {
				info = ex.Message;
			}

			ConnFinished(info);
		}


		private bool setConnect() {
			bool isMaster = false;
			if (masterTextBox.Visible && !masterTextBox.Text.Equals(ipBox1.Value)) {
				isMaster = true;
			}
			hostName = masterTextBox.Visible ? masterTextBox.Text : ipBox1.Value;
			userName = usernameTextBox.Text;
			password = passwordTextBox.Text;
			try {
				ConnectManager.MasterHostName = "";
				ConnectManager.Connect(hostName, userName, password, isMaster);
				ConnectManager.TargetHostName = ipBox1.Value;
			}
			catch (Exception) {
				if (!String.IsNullOrEmpty(ConnectManager.MasterHostName)) {
					return false;
				}
				else {
					throw;
				}
			}
			return true;
		}

		private delegate void changeText(string info);

		public void ConnFinished(string info) {
			if (this.InvokeRequired) {
				this.BeginInvoke(new changeText(ConnFinished), info);
			}
			else {
				cmd.HideOpaqueLayer();
				if (Wizard != null) {
					Wizard.SetWizardButtons(WizardButton.Back | WizardButton.Next);
					if (!string.IsNullOrEmpty(info)) {
						if (info.IndexOf("authenticate") > 0) {
							testConnectLabel.Text = "用户名或密码错误";
						}
						else {
							testConnectLabel.Text = info;
						}
					}
					
					testConnectbutton.Enabled = true;
					masterLabel.Visible = masterTextBox.Visible = false;
					if(ConnectManager.IsConn) {
						if (!IsTestConn) {
							foreach (WizardPage page in Wizard.m_pages) {
								if ("HostPage" == page.Name) {
									Wizard.ActivatePage(Wizard.m_pages.IndexOf(page) + 1);
									break;
								}
							}
						}
					} else {
						string masterHostName = ConnectManager.MasterHostName;
						if (!String.IsNullOrEmpty(masterHostName)) {
							masterLabel.Visible = masterTextBox.Visible = true;
							this.ipBox1.BindTextBox = masterTextBox;
							masterTextBox.Text = masterHostName;
							passwordTextBox.Text = "";
							testConnectLabel.Text = "请输入管理节点用户密码！";
						}
					}
				}
			}
		}


		private void usernameTextBox_TextChanged(Object sender, EventArgs e) {
			if (String.IsNullOrEmpty(usernameTextBox.Text)) {
				userNameInfoLabel.Text = "请输入用户名！";
				return;
			}
			else {
				userNameInfoLabel.Text = "*";
				IsUserName = true;
				if (!this.ipBox1.IsValidate && IsPassword) {
					//testConnectbutton.Enabled = true;
					//Wizard.SetWizardButtons(WizardButton.Back | WizardButton.Next);
				}
				else {
					//testConnectbutton.Enabled = false;
					//Wizard.SetWizardButtons(WizardButton.Back);
				}
			}
		}

		private void passwordTextBox_TextChanged(Object sender, EventArgs e) {
			if (String.IsNullOrEmpty(passwordTextBox.Text)) {
				passInfoLabel.Text = "请输入密码！";
				return;
			}
			else {
				passInfoLabel.Text = "*";
				IsPassword = true;
				if (!this.ipBox1.IsValidate && IsUserName) {
					//testConnectbutton.Enabled = true;
					//Wizard.SetWizardButtons(WizardButton.Back | WizardButton.Next);
				}
				else {
					//testConnectbutton.Enabled = false;
					//Wizard.SetWizardButtons(WizardButton.Back);
				}
			}
		}

	}
}
