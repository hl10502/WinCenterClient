namespace WizardLib.Controls.Common {
	partial class CheckFailure {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckFailure));
			this.errorPictureBox = new System.Windows.Forms.PictureBox();
			this.errorLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.errorPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// errorPictureBox
			// 
			this.errorPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("errorPictureBox.Image")));
			this.errorPictureBox.Location = new System.Drawing.Point(0, 0);
			this.errorPictureBox.Name = "errorPictureBox";
			this.errorPictureBox.Size = new System.Drawing.Size(100, 50);
			this.errorPictureBox.TabIndex = 1;
			this.errorPictureBox.TabStop = false;
			// 
			// errorLabel
			// 
			this.errorLabel.ForeColor = System.Drawing.Color.Red;
			this.errorLabel.Location = new System.Drawing.Point(0, 0);
			this.errorLabel.Name = "errorLabel";
			this.errorLabel.Size = new System.Drawing.Size(100, 23);
			this.errorLabel.TabIndex = 0;
			// 
			// CheckFailure
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.errorLabel);
			this.Controls.Add(this.errorPictureBox);
			this.Name = "CheckFailure";
			((System.ComponentModel.ISupportInitialize)(this.errorPictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox errorPictureBox;
		private System.Windows.Forms.Label errorLabel;
	}
}
