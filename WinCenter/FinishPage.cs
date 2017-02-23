using WizardLib;
using Wizard.Core;
using System.Windows.Forms;
using System.Diagnostics;

namespace Wizard {
	public partial class FinishPage : WizardLib.InteriorWizardPage {

		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private string winCenterLinkAddr;
		public FinishPage() {
			InitializeComponent();
		}

		protected override bool OnSetActive() {
			if (!base.OnSetActive())
				return false;

			string ip = "";
			ConnectManager.IPMaskGatewayDict.TryGetValue("ip", out ip);
			WinCenterLinkLabel.Text = winCenterLinkAddr = string.Format("https://{0}:8090/pc/index.jsp", ip);
			
			log.InfoFormat("进入【完成】页面");
			
			Wizard.SetWizardButtons(WizardButton.Finish);
			return true;
		}


		private void WinCenterLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)  
        {
			Process.Start(winCenterLinkAddr);  
        }  

		
	}
}
