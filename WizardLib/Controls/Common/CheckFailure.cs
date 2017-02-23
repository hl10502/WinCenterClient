using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WizardLib.Controls.Common {
	public delegate bool CheckDelegate(out string error);

	public partial class CheckFailure : UserControl {
		public CheckFailure() {
			InitializeComponent();
		}

		[Localizable(true)]
		public string Error {
			get {
				return errorLabel.Text;
			}
			set {
				errorLabel.Text = value;
			}
		}

		protected override void OnResize(EventArgs e) {
			base.OnResize(e);

			errorLabel.MaximumSize = new Size(Width - errorLabel.Margin.Left - errorLabel.Margin.Right -
											  errorPictureBox.Width - errorPictureBox.Margin.Left - errorPictureBox.Margin.Right, 0);
		}

		public void ShowError(string errorMsg) {
			Visible = true;
			Error = errorMsg;
		}

		public void HideError() {
			Visible = false;
		}

		/// <summary>
		/// Performs certain checks on the pages's input data and shows/hides itself accordingly
		/// </summary>
		/// <param name="checks">The checks to perform</param>
		/// <returns></returns>
		public bool PerformCheck(params CheckDelegate[] checks) {
			foreach (var check in checks) {
				string errorMsg;

				if (!check.Invoke(out errorMsg)) {
					if (string.IsNullOrEmpty(errorMsg))
						HideError();
					else
						ShowError(errorMsg);

					return false;
				}
			}

			HideError();
			return true;
		}
	}
}
