namespace Wizard {
	partial class HostPage {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HostPage));
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ipBox1 = new WizardLib.IPBox();
			this.hostnameLabel = new System.Windows.Forms.Label();
			this.targetIPInfoLabel = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.passInfoLabel = new System.Windows.Forms.Label();
			this.userNameInfoLabel = new System.Windows.Forms.Label();
			this.masterTextBox = new System.Windows.Forms.TextBox();
			this.masterLabel = new System.Windows.Forms.Label();
			this.passwordTextBox = new System.Windows.Forms.TextBox();
			this.passwordLabel = new System.Windows.Forms.Label();
			this.usernameTextBox = new System.Windows.Forms.TextBox();
			this.usernameLabel = new System.Windows.Forms.Label();
			this.testConnectbutton = new System.Windows.Forms.Button();
			this.testConnectLabel = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(289, 67);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(249, 23);
			this.label1.TabIndex = 6;
			this.label1.Text = "配置物理主机信息(待创建虚拟机所在主机)";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.ipBox1);
			this.groupBox1.Controls.Add(this.hostnameLabel);
			this.groupBox1.Controls.Add(this.targetIPInfoLabel);
			this.groupBox1.Location = new System.Drawing.Point(7, 14);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(406, 67);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "网络信息";
			// 
			// ipBox1
			// 
			this.ipBox1.Location = new System.Drawing.Point(139, 31);
			this.ipBox1.Name = "ipBox1";
			this.ipBox1.Padding = new System.Windows.Forms.Padding(1);
			this.ipBox1.ShowErrorFlag = true;
			this.ipBox1.Size = new System.Drawing.Size(140, 21);
			this.ipBox1.TabIndex = 11;
			// 
			// hostnameLabel
			// 
			this.hostnameLabel.Location = new System.Drawing.Point(23, 32);
			this.hostnameLabel.Name = "hostnameLabel";
			this.hostnameLabel.Size = new System.Drawing.Size(110, 20);
			this.hostnameLabel.TabIndex = 0;
			this.hostnameLabel.Text = "目标主机IP地址：";
			this.hostnameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// targetIPInfoLabel
			// 
			this.targetIPInfoLabel.ForeColor = System.Drawing.Color.Red;
			this.targetIPInfoLabel.Location = new System.Drawing.Point(285, 31);
			this.targetIPInfoLabel.Name = "targetIPInfoLabel";
			this.targetIPInfoLabel.Size = new System.Drawing.Size(104, 23);
			this.targetIPInfoLabel.TabIndex = 10;
			this.targetIPInfoLabel.Text = "*";
			this.targetIPInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.passInfoLabel);
			this.groupBox2.Controls.Add(this.userNameInfoLabel);
			this.groupBox2.Controls.Add(this.masterTextBox);
			this.groupBox2.Controls.Add(this.masterLabel);
			this.groupBox2.Controls.Add(this.passwordTextBox);
			this.groupBox2.Controls.Add(this.passwordLabel);
			this.groupBox2.Controls.Add(this.usernameTextBox);
			this.groupBox2.Controls.Add(this.usernameLabel);
			this.groupBox2.Location = new System.Drawing.Point(7, 87);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(406, 109);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "用户信息";
			// 
			// passInfoLabel
			// 
			this.passInfoLabel.ForeColor = System.Drawing.Color.Red;
			this.passInfoLabel.Location = new System.Drawing.Point(287, 74);
			this.passInfoLabel.Name = "passInfoLabel";
			this.passInfoLabel.Size = new System.Drawing.Size(102, 23);
			this.passInfoLabel.TabIndex = 13;
			this.passInfoLabel.Text = "*";
			this.passInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// userNameInfoLabel
			// 
			this.userNameInfoLabel.ForeColor = System.Drawing.Color.Red;
			this.userNameInfoLabel.Location = new System.Drawing.Point(287, 51);
			this.userNameInfoLabel.Name = "userNameInfoLabel";
			this.userNameInfoLabel.Size = new System.Drawing.Size(102, 23);
			this.userNameInfoLabel.TabIndex = 12;
			this.userNameInfoLabel.Text = "*";
			this.userNameInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// masterTextBox
			// 
			this.masterTextBox.Enabled = false;
			this.masterTextBox.Location = new System.Drawing.Point(139, 26);
			this.masterTextBox.Name = "masterTextBox";
			this.masterTextBox.Size = new System.Drawing.Size(140, 21);
			this.masterTextBox.TabIndex = 7;
			this.masterTextBox.Visible = false;
			// 
			// masterLabel
			// 
			this.masterLabel.Location = new System.Drawing.Point(23, 26);
			this.masterLabel.Name = "masterLabel";
			this.masterLabel.Size = new System.Drawing.Size(110, 21);
			this.masterLabel.TabIndex = 6;
			this.masterLabel.Text = "管理节点IP地址：";
			this.masterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.masterLabel.Visible = false;
			// 
			// passwordTextBox
			// 
			this.passwordTextBox.Location = new System.Drawing.Point(139, 77);
			this.passwordTextBox.Name = "passwordTextBox";
			this.passwordTextBox.PasswordChar = '*';
			this.passwordTextBox.Size = new System.Drawing.Size(140, 21);
			this.passwordTextBox.TabIndex = 5;
			this.passwordTextBox.TextChanged += new System.EventHandler(this.passwordTextBox_TextChanged);
			// 
			// passwordLabel
			// 
			this.passwordLabel.Location = new System.Drawing.Point(23, 80);
			this.passwordLabel.Name = "passwordLabel";
			this.passwordLabel.Size = new System.Drawing.Size(110, 18);
			this.passwordLabel.TabIndex = 4;
			this.passwordLabel.Text = "密码：";
			this.passwordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// usernameTextBox
			// 
			this.usernameTextBox.Location = new System.Drawing.Point(139, 50);
			this.usernameTextBox.Name = "usernameTextBox";
			this.usernameTextBox.Size = new System.Drawing.Size(140, 21);
			this.usernameTextBox.TabIndex = 3;
			this.usernameTextBox.Text = "root";
			this.usernameTextBox.TextChanged += new System.EventHandler(this.usernameTextBox_TextChanged);
			// 
			// usernameLabel
			// 
			this.usernameLabel.Location = new System.Drawing.Point(23, 53);
			this.usernameLabel.Name = "usernameLabel";
			this.usernameLabel.Size = new System.Drawing.Size(110, 18);
			this.usernameLabel.TabIndex = 2;
			this.usernameLabel.Text = "用户名：";
			this.usernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// testConnectbutton
			// 
			this.testConnectbutton.BackColor = System.Drawing.Color.Transparent;
			this.testConnectbutton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("testConnectbutton.BackgroundImage")));
			this.testConnectbutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.testConnectbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.testConnectbutton.ForeColor = System.Drawing.Color.Transparent;
			this.testConnectbutton.Location = new System.Drawing.Point(544, 300);
			this.testConnectbutton.Name = "testConnectbutton";
			this.testConnectbutton.Size = new System.Drawing.Size(75, 23);
			this.testConnectbutton.TabIndex = 9;
			this.testConnectbutton.Text = "测试连接";
			this.testConnectbutton.UseVisualStyleBackColor = false;
			this.testConnectbutton.Click += new System.EventHandler(this.testConnectbutton_Click);
			// 
			// testConnectLabel
			// 
			this.testConnectLabel.ForeColor = System.Drawing.Color.Red;
			this.testConnectLabel.Location = new System.Drawing.Point(211, 297);
			this.testConnectLabel.Name = "testConnectLabel";
			this.testConnectLabel.Size = new System.Drawing.Size(327, 23);
			this.testConnectLabel.TabIndex = 14;
			this.testConnectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Location = new System.Drawing.Point(206, 93);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(420, 201);
			this.panel1.TabIndex = 15;
			// 
			// HostPage
			// 
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.testConnectLabel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.testConnectbutton);
			this.Name = "HostPage";
			this.Controls.SetChildIndex(this.pictureBox1, 0);
			this.Controls.SetChildIndex(this.pictureBox2, 0);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.pictureBox4, 0);
			this.Controls.SetChildIndex(this.pictureBox5, 0);
			this.Controls.SetChildIndex(this.pictureBox6, 0);
			this.Controls.SetChildIndex(this.testConnectbutton, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.testConnectLabel, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label hostnameLabel;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox passwordTextBox;
		private System.Windows.Forms.Label passwordLabel;
		private System.Windows.Forms.TextBox usernameTextBox;
		private System.Windows.Forms.Label usernameLabel;
		private System.Windows.Forms.Button testConnectbutton;
		private System.Windows.Forms.Label targetIPInfoLabel;
		private System.Windows.Forms.TextBox masterTextBox;
		private System.Windows.Forms.Label masterLabel;
		private System.Windows.Forms.Label passInfoLabel;
		private System.Windows.Forms.Label userNameInfoLabel;
		private System.Windows.Forms.Label testConnectLabel;
		private WizardLib.IPBox ipBox1;
		private System.Windows.Forms.Panel panel1;
	}
}
