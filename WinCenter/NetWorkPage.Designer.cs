namespace Wizard {
	partial class NetWorkPage {
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

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetWorkPage));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.pingButton = new System.Windows.Forms.Button();
			this.ipBox3 = new WizardLib.IPBox();
			this.ipBox2 = new WizardLib.IPBox();
			this.ipBox1 = new WizardLib.IPBox();
			this.IPInfoLabel = new System.Windows.Forms.Label();
			this.gatewayLabel = new System.Windows.Forms.Label();
			this.maskLabel = new System.Windows.Forms.Label();
			this.IPLabel = new System.Windows.Forms.Label();
			this.macCheckLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.InfoLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox4
			// 
			this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.pingButton);
			this.groupBox1.Controls.Add(this.ipBox3);
			this.groupBox1.Controls.Add(this.ipBox2);
			this.groupBox1.Controls.Add(this.ipBox1);
			this.groupBox1.Controls.Add(this.IPInfoLabel);
			this.groupBox1.Controls.Add(this.gatewayLabel);
			this.groupBox1.Controls.Add(this.maskLabel);
			this.groupBox1.Controls.Add(this.IPLabel);
			this.groupBox1.Location = new System.Drawing.Point(212, 140);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(416, 136);
			this.groupBox1.TabIndex = 16;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "管理网络信息";
			// 
			// pingButton
			// 
			this.pingButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pingButton.BackgroundImage")));
			this.pingButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.pingButton.ForeColor = System.Drawing.Color.Transparent;
			this.pingButton.Location = new System.Drawing.Point(246, 29);
			this.pingButton.Name = "pingButton";
			this.pingButton.Size = new System.Drawing.Size(75, 23);
			this.pingButton.TabIndex = 12;
			this.pingButton.Text = "Ping测试";
			this.pingButton.UseVisualStyleBackColor = true;
			this.pingButton.Click += new System.EventHandler(this.pingButton_Click);
			// 
			// ipBox3
			// 
			this.ipBox3.Location = new System.Drawing.Point(78, 98);
			this.ipBox3.Name = "ipBox3";
			this.ipBox3.Padding = new System.Windows.Forms.Padding(1);
			this.ipBox3.Size = new System.Drawing.Size(140, 21);
			this.ipBox3.TabIndex = 11;
			// 
			// ipBox2
			// 
			this.ipBox2.Location = new System.Drawing.Point(78, 62);
			this.ipBox2.Name = "ipBox2";
			this.ipBox2.Padding = new System.Windows.Forms.Padding(1);
			this.ipBox2.Size = new System.Drawing.Size(140, 21);
			this.ipBox2.TabIndex = 10;
			// 
			// ipBox1
			// 
			this.ipBox1.Location = new System.Drawing.Point(78, 29);
			this.ipBox1.Name = "ipBox1";
			this.ipBox1.Padding = new System.Windows.Forms.Padding(1);
			this.ipBox1.ShowErrorFlag = true;
			this.ipBox1.Size = new System.Drawing.Size(140, 21);
			this.ipBox1.TabIndex = 9;
			// 
			// IPInfoLabel
			// 
			this.IPInfoLabel.ForeColor = System.Drawing.Color.Red;
			this.IPInfoLabel.Location = new System.Drawing.Point(222, 29);
			this.IPInfoLabel.Name = "IPInfoLabel";
			this.IPInfoLabel.Size = new System.Drawing.Size(18, 23);
			this.IPInfoLabel.TabIndex = 7;
			this.IPInfoLabel.Text = "*";
			this.IPInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// gatewayLabel
			// 
			this.gatewayLabel.Location = new System.Drawing.Point(24, 98);
			this.gatewayLabel.Name = "gatewayLabel";
			this.gatewayLabel.Size = new System.Drawing.Size(48, 23);
			this.gatewayLabel.TabIndex = 4;
			this.gatewayLabel.Text = "网关";
			this.gatewayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// maskLabel
			// 
			this.maskLabel.Location = new System.Drawing.Point(24, 62);
			this.maskLabel.Name = "maskLabel";
			this.maskLabel.Size = new System.Drawing.Size(48, 23);
			this.maskLabel.TabIndex = 2;
			this.maskLabel.Text = "掩码";
			this.maskLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// IPLabel
			// 
			this.IPLabel.Location = new System.Drawing.Point(24, 27);
			this.IPLabel.Name = "IPLabel";
			this.IPLabel.Size = new System.Drawing.Size(48, 23);
			this.IPLabel.TabIndex = 0;
			this.IPLabel.Text = "IP地址";
			this.IPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// macCheckLabel
			// 
			this.macCheckLabel.ForeColor = System.Drawing.Color.Red;
			this.macCheckLabel.Location = new System.Drawing.Point(273, 277);
			this.macCheckLabel.Name = "macCheckLabel";
			this.macCheckLabel.Size = new System.Drawing.Size(242, 23);
			this.macCheckLabel.TabIndex = 17;
			this.macCheckLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(217, 93);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(416, 23);
			this.label1.TabIndex = 18;
			this.label1.Text = "掩码、网关与目标主机相同，IP地址请修改最后一位";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// InfoLabel
			// 
			this.InfoLabel.ForeColor = System.Drawing.Color.Red;
			this.InfoLabel.Location = new System.Drawing.Point(210, 290);
			this.InfoLabel.Name = "InfoLabel";
			this.InfoLabel.Size = new System.Drawing.Size(418, 23);
			this.InfoLabel.TabIndex = 19;
			this.InfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// NetWorkPage
			// 
			this.Controls.Add(this.InfoLabel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.macCheckLabel);
			this.Controls.Add(this.groupBox1);
			this.Name = "NetWorkPage";
			this.Controls.SetChildIndex(this.pictureBox1, 0);
			this.Controls.SetChildIndex(this.pictureBox2, 0);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.pictureBox4, 0);
			this.Controls.SetChildIndex(this.pictureBox5, 0);
			this.Controls.SetChildIndex(this.pictureBox6, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.Controls.SetChildIndex(this.macCheckLabel, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.InfoLabel, 0);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label IPLabel;
		private System.Windows.Forms.Label gatewayLabel;
		private System.Windows.Forms.Label maskLabel;
		private System.Windows.Forms.Label IPInfoLabel;
		private System.Windows.Forms.Label macCheckLabel;
		private WizardLib.IPBox ipBox1;
		private WizardLib.IPBox ipBox3;
		private WizardLib.IPBox ipBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label InfoLabel;
		private System.Windows.Forms.Button pingButton;
	}
}
