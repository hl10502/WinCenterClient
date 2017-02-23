using WizardLib;
using System.IO;
using System;
using System.Windows.Forms;
using System.Resources;
using Wizard.Properties;
using System.Diagnostics;

namespace Wizard {
	public partial class LicenseAgreement : ExteriorWizardPage {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public LicenseAgreement() {
			InitializeComponent();
		}

		protected override bool OnSetActive() {
			if (!base.OnSetActive())
				return false;

			log.InfoFormat("进入【许可协议】页面");

			readRtfToRichTextBox();

			if (radioButton1.Checked) {
				Wizard.SetWizardButtons(WizardButton.Back | WizardButton.Next);
			}
			else {
				Wizard.SetWizardButtons(WizardButton.Back);
			}
			return true;
		}

		private void readRtfToRichTextBox() {
			try {
				string startupPath = System.Windows.Forms.Application.StartupPath;
				string filePath = startupPath + Path.DirectorySeparatorChar + "LicenseAgreement.rtf";
				this.richTextBox1.LoadFile(filePath, RichTextBoxStreamType.RichText); //加载rtf文件
				//this.richTextBox1.LoadFile(filePath, RichTextBoxStreamType.PlainText);//加载txt文件
			}
			catch (Exception e) {
				log.ErrorFormat("打开许可协议文件错误: {0}", e.Message);
			}
		}

		//打开rtf文件中的email超链接
		private void richTextBox1_LinkedClick(object sender, LinkClickedEventArgs e) {
			try {
				Process.Start(e.LinkText);
			}
			catch (Exception ex) {
				log.ErrorFormat("未找到邮箱应用程序，打开邮箱地址错误：{0}", ex.Message);
			}
		}

		private void radioButton1_CheckedChanged(object sender, System.EventArgs e) {
			Wizard.SetWizardButtons(WizardButton.Back | WizardButton.Next);
		}

		private void radioButton2_CheckedChanged(object sender, System.EventArgs e) {
			Wizard.SetWizardButtons(WizardButton.Back);
		}

	}
}
