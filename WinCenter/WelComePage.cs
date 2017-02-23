using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WizardLib;

namespace Wizard {
	public partial class WelComePage : ExteriorWizardPage {

		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public WelComePage() {
			InitializeComponent();
		}

		protected override bool OnSetActive() {
			if (!base.OnSetActive())
				return false;

			log.InfoFormat("WinCenter安装向导开始，进入【欢迎】页面");
			Wizard.SetWizardButtons(WizardButton.Next);
			return true;
		}

	}
}
