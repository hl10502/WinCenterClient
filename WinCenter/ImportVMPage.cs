using WizardLib;
using Wizard.Core;
using System.Threading;
using System;
using System.Windows.Forms;
using WinAPI;

namespace Wizard {
	public partial class ImportVMPage : InteriorWizardPage {

		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public ImportVMPage() {
			InitializeComponent();
		}

		protected override bool OnSetActive() {
			if (!base.OnSetActive())
				return false;

			log.InfoFormat("进入【安装虚拟机】页面");
            label1.Text = "";
			ShowData();
			Wizard.SetWizardButtons(WizardButton.Back);
			return true;
		}

		protected override string OnWizardNext() {
			return base.OnWizardNext();
		}

		private void ShowData() {
			string memory = string.Format("{0:0.00}GB", ConnectManager.Memory / Constants.UINT);
			log.InfoFormat("文件路径：{0}", ConnectManager.FilePath);
			log.InfoFormat("目标主机：{0}", ConnectManager.TargetHostName);
			log.InfoFormat("存储：{0}", ConnectManager.SelectedSR.name_label);
			log.InfoFormat("虚拟机名称：{0}", ConnectManager.VMName);
			log.InfoFormat("虚拟机配置：内存 {0}，VCPU {1}个", memory, ConnectManager.VcpuCount);

			this.filePathLabel.Text = string.Format("文件路径：{0}", ConnectManager.FilePath);
			this.toolTip1.SetToolTip(this.filePathLabel, ConnectManager.FilePath);

			this.targetHostLabel.Text = string.Format("目标主机：{0}", ConnectManager.TargetHostName);
			this.storageLabel.Text = string.Format("存    储：{0}", ConnectManager.SelectedSR.name_label);
			this.VMNameLabel.Text = string.Format("虚拟机名称：{0}", ConnectManager.VMName);
			this.networkLabel.Text = string.Format("虚拟机配置：内存 {0}，VCPU {1}个", memory, ConnectManager.VcpuCount);
			string ip = "";
			ConnectManager.IPMaskGatewayDict.TryGetValue("ip", out ip);
			this.ipLabel.Text = string.Format("IP 地 址：{0}", ip);
			string netmask = "";
			ConnectManager.IPMaskGatewayDict.TryGetValue("netmask", out netmask);
			this.maskLabel.Text = string.Format("掩    码：{0}", netmask);
			string gateway = "";
			ConnectManager.IPMaskGatewayDict.TryGetValue("gateway", out gateway);
			this.gatewayLabel.Text = string.Format("网    关：{0}", gateway);
		}
		
		private void importButton_Click(object sender, System.EventArgs e) {
			log.InfoFormat("开始安装WinCenter VM");
            if (!checkMemory()) {
                return;
            }
			importButton.Enabled = false;
			Wizard.SetWizardButtons(WizardButton.DisabledAll);
			//注册关闭按钮事件
			Wizard.FormClosing += new FormClosingEventHandler(FormClosing);
			HTTPHelper.errorInfo = "";

			Thread workThread = new Thread(new ThreadStart(ProcessImp));
			workThread.Start();

			Thread uiThread = new Thread(new ThreadStart(ChangeProgress));
			uiThread.Start();
		}

        private bool checkMemory() {
            XenRef<Host> hostRef = Host.get_by_uuid(ConnectManager.session, ConnectManager.TargetHost.uuid);
            ulong free_memory = (ulong)Host.compute_free_memory(ConnectManager.session, hostRef);
            string hostFreeMemory = string.Format("{0:0.00}GB", free_memory / Constants.UINT);
            string vmMemory = string.Format("{0:0.00}GB", ConnectManager.Memory / Constants.UINT);
            log.InfoFormat("物理主机可用内存为{0}，虚拟机需要的内存为{1}", hostFreeMemory, vmMemory);

            if (ConnectManager.Memory > free_memory)
            {
                log.ErrorFormat("物理主机的可用内存不够!");
                label1.Text = "物理主机的可用内存不够！";
                return false;
            }

            return true;
        }

		private void FormClosing(object sender, FormClosingEventArgs e) {
			e.Cancel = true;
			log.InfoFormat("安装过程中，禁止关闭窗口");
		}

		private void ProcessImp() {
			DateTime startTime = System.DateTime.Now;
			try {
				VMOper vmOper = new VMOper();
				vmOper.ImpXvaFile();
				DateTime endTime = System.DateTime.Now;
				TimeSpan time = endTime - startTime;
				log.InfoFormat("安装WinCenter VM总耗时: {0}小时{1}分钟{2}秒", time.Hours, time.Minutes, time.Seconds);
				log.InfoFormat("安装WinCenter VM成功");
			}
			catch (Exception ex) {
				HTTPHelper.errorInfo = ex.Message;
				log.InfoFormat("安装WinCenter VM失败Message: {0}", ex.Message);
				log.InfoFormat("安装WinCenter VM失败StackTrace: {0}", ex.StackTrace);
			}
		}


		private void ChangeProgress() {
			int i = 0;
			while (true) {
				while (!this.importButton.IsHandleCreated) {
					//解决窗体关闭时出现“访问已释放句柄“的异常
					if (this.importButton.Disposing || this.importButton.IsDisposed)
						return;
				}
				int progressPercent = HTTPHelper.progressPercent;
				this.Invoke(new Action<int>(this.UpdateProgress), progressPercent);
				if (progressPercent == 100 || !string.IsNullOrEmpty(HTTPHelper.errorInfo)) {
					if(i == 1) {
						Wizard.FormClosing -= new FormClosingEventHandler(FormClosing);
						break;
					}
					i++;
				}
			}
		}

		private void UpdateProgress(int progressPercent) {
			if (string.IsNullOrEmpty(HTTPHelper.errorInfo)) {
				progressBar1.Value = progressPercent;
				label1.Text = "正在导入，进度：" + progressPercent + "%";
				if (progressPercent >= 90) {
					label1.Text = HTTPHelper.progressInfo + "，进度：" + progressPercent + "%";
				}
				label1.Refresh();
				if (progressPercent == 100) {
					label1.Text = "安装完成：" + progressPercent + "%";
					label1.Refresh();
					Wizard.SetWizardButtons(WizardButton.Next);
					log.InfoFormat("WinCenter VM安装完成");
				}
			}
			else {
				//label1.Text = "安装失败:" + HTTPHelper.errorInfo;
				label1.Text = "安装失败，详情请查看日志！";
				label1.Refresh();
				importButton.Enabled = true;
				Wizard.SetWizardButtons(WizardButton.Back);
			}
		}

	}
}
