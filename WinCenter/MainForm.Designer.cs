using System.Windows.Forms;
using System.Configuration;
namespace Wizard {
	partial class MainForm {
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.SuspendLayout();
			// 
			// m_backButton
			// 
			this.m_backButton.BackColor = System.Drawing.Color.Transparent;
			this.m_backButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_backButton.BackgroundImage")));
			this.m_backButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.m_backButton.FlatAppearance.BorderSize = 0;
			this.m_backButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.m_backButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.m_backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_backButton.Location = new System.Drawing.Point(314, 396);
			// 
			// m_nextButton
			// 
			this.m_nextButton.BackColor = System.Drawing.Color.Transparent;
			this.m_nextButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_nextButton.BackgroundImage")));
			this.m_nextButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.m_nextButton.FlatAppearance.BorderSize = 0;
			this.m_nextButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.m_nextButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.m_nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_nextButton.Location = new System.Drawing.Point(410, 396);
			// 
			// m_cancelButton
			// 
			this.m_cancelButton.BackColor = System.Drawing.Color.Transparent;
			this.m_cancelButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_cancelButton.BackgroundImage")));
			this.m_cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.m_cancelButton.FlatAppearance.BorderSize = 0;
			this.m_cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.m_cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.m_cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_cancelButton.Location = new System.Drawing.Point(506, 396);
			// 
			// m_finishButton
			// 
			this.m_finishButton.BackColor = System.Drawing.Color.Transparent;
			this.m_finishButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_finishButton.BackgroundImage")));
			this.m_finishButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.m_finishButton.FlatAppearance.BorderSize = 0;
			this.m_finishButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.m_finishButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.m_finishButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_finishButton.Location = new System.Drawing.Point(410, 396);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(664, 433);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.ShowInTaskbar = true;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CNware-WinCenter-ApplianceV" + ConfigurationManager.AppSettings["CNware-WinCenter-Client-Version"] + "安装程序";
			this.ResumeLayout(false);

		}
		#endregion





	}
}

