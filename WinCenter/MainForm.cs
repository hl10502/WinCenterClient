using System.Windows.Forms;
using WizardLib;

namespace Wizard {
	public partial class MainForm : WizardForm {

		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


		///**
		// 禁止窗体关闭按钮
		// */
		//protected override void WndProc(ref System.Windows.Forms.Message m) {
		//    const int WM_SYSCOMMAND = 0x0112;
		//    const int SC_CLOSE = 0xF060;
		//    if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE) {
		//        return;
		//    }
		//    base.WndProc(ref m);
		//}


		/**
		 窗体关闭提示
		 */
		protected override void WndProc(ref System.Windows.Forms.Message m) {
			const int WM_SYSCOMMAND = 0x0112;
			const int SC_CLOSE = 0xF060;
			if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE) {
				WizardPage page = (WizardPage)this.m_pages[this.m_selectedIndex];
				if ("ImportVMPage" == page.Name && !this.m_backButton.Enabled && !this.m_nextButton.Enabled) {
					return;
				} else if (MessageBox.Show("是否确定取消安装?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
					base.WndProc(ref m);
					return;
				} else {
					return;
				}
			}
			base.WndProc(ref m);
		}


		public MainForm() {
			InitializeComponent();
			log.InfoFormat("WinCenter安装向导开始初始化....");
			Controls.AddRange(new Control[] {
		        new WelComePage(),
				new LicenseAgreement(),
				new ImportSourcePage(),
				new HostPage(),
				new StoragePage(),
				new NetWorkPage(),
				new ImportVMPage(),
				new FinishPage()
		        });
			log.InfoFormat("WinCenter安装向导初始化完成....");
		}
	}
}
