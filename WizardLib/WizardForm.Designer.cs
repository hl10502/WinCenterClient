namespace WizardLib {
	partial class WizardForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.m_backButton = new MyButton();
			this.m_nextButton = new MyButton();
			this.m_cancelButton = new MyButton();
			this.m_finishButton = new MyButton();
			this.SuspendLayout();
			// 
			// m_backButton
			// 
			this.m_backButton.BackColor = System.Drawing.SystemColors.Control;
			this.m_backButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.m_backButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.m_backButton.Location = new System.Drawing.Point(313, 363);
			this.m_backButton.Name = "m_backButton";
			this.m_backButton.Size = new System.Drawing.Size(90, 25);
			this.m_backButton.TabIndex = 8;
			this.m_backButton.Text = "< 上一步";
			this.m_backButton.UseVisualStyleBackColor = false;
			this.m_backButton.Click += new System.EventHandler(this.OnClickBack);
			// 
			// m_nextButton
			// 
			this.m_nextButton.BackColor = System.Drawing.SystemColors.Control;
			this.m_nextButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.m_nextButton.Location = new System.Drawing.Point(409, 363);
			this.m_nextButton.Name = "m_nextButton";
			this.m_nextButton.Size = new System.Drawing.Size(90, 25);
			this.m_nextButton.TabIndex = 9;
			this.m_nextButton.Text = "下一步 >";
			this.m_nextButton.UseVisualStyleBackColor = false;
			this.m_nextButton.Click += new System.EventHandler(this.OnClickNext);
			// 
			// m_cancelButton
			// 
			this.m_cancelButton.BackColor = System.Drawing.SystemColors.Control;
			this.m_cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.m_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_cancelButton.Location = new System.Drawing.Point(505, 363);
			this.m_cancelButton.Name = "m_cancelButton";
			this.m_cancelButton.Size = new System.Drawing.Size(90, 25);
			this.m_cancelButton.TabIndex = 11;
			this.m_cancelButton.Text = "取消";
			this.m_cancelButton.UseVisualStyleBackColor = false;
			this.m_cancelButton.Click += new System.EventHandler(this.OnClickCancel);
			// 
			// m_finishButton
			// 
			this.m_finishButton.BackColor = System.Drawing.SystemColors.Control;
			this.m_finishButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.m_finishButton.Location = new System.Drawing.Point(409, 363);
			this.m_finishButton.Name = "m_finishButton";
			this.m_finishButton.Size = new System.Drawing.Size(90, 25);
			this.m_finishButton.TabIndex = 10;
			this.m_finishButton.Text = "完成";
			this.m_finishButton.UseVisualStyleBackColor = false;
			this.m_finishButton.Visible = false;
			this.m_finishButton.Click += new System.EventHandler(this.OnClickFinish);
			// 
			// WizardForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(664, 433);
			this.Controls.Add(this.m_backButton);
			this.Controls.Add(this.m_nextButton);
			this.Controls.Add(this.m_cancelButton);
			this.Controls.Add(this.m_finishButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "WizardForm";
			this.ShowInTaskbar = false;
			this.ResumeLayout(false);

		}
		#endregion
	}
}