using System.Windows.Forms;
namespace Wizard {
	partial class ImportVMPage {
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportVMPage));
			this.importButton = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.VMNameLabel = new System.Windows.Forms.Label();
			this.gatewayLabel = new System.Windows.Forms.Label();
			this.maskLabel = new System.Windows.Forms.Label();
			this.ipLabel = new System.Windows.Forms.Label();
			this.networkLabel = new System.Windows.Forms.Label();
			this.storageLabel = new System.Windows.Forms.Label();
			this.targetHostLabel = new System.Windows.Forms.Label();
			this.filePathLabel = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox5
			// 
			this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
			// 
			// importButton
			// 
			this.importButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("importButton.BackgroundImage")));
			this.importButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.importButton.FlatAppearance.BorderSize = 0;
			this.importButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.importButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.importButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.importButton.Location = new System.Drawing.Point(533, 308);
			this.importButton.Name = "importButton";
			this.importButton.Size = new System.Drawing.Size(75, 23);
			this.importButton.TabIndex = 14;
			this.importButton.Text = "安装";
			this.importButton.UseVisualStyleBackColor = true;
			this.importButton.Click += new System.EventHandler(this.importButton_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.BackColor = System.Drawing.Color.White;
			this.progressBar1.Location = new System.Drawing.Point(212, 289);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(409, 13);
			this.progressBar1.TabIndex = 15;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(212, 308);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(315, 23);
			this.label1.TabIndex = 16;
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.VMNameLabel);
			this.groupBox1.Controls.Add(this.gatewayLabel);
			this.groupBox1.Controls.Add(this.maskLabel);
			this.groupBox1.Controls.Add(this.ipLabel);
			this.groupBox1.Controls.Add(this.networkLabel);
			this.groupBox1.Controls.Add(this.storageLabel);
			this.groupBox1.Controls.Add(this.targetHostLabel);
			this.groupBox1.Controls.Add(this.filePathLabel);
			this.groupBox1.Location = new System.Drawing.Point(212, 78);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(409, 205);
			this.groupBox1.TabIndex = 17;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "汇总信息";
			// 
			// VMNameLabel
			// 
			this.VMNameLabel.Location = new System.Drawing.Point(45, 86);
			this.VMNameLabel.Name = "VMNameLabel";
			this.VMNameLabel.Size = new System.Drawing.Size(320, 23);
			this.VMNameLabel.TabIndex = 7;
			this.VMNameLabel.Text = "虚拟机名称";
			this.VMNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// gatewayLabel
			// 
			this.gatewayLabel.Location = new System.Drawing.Point(44, 178);
			this.gatewayLabel.Name = "gatewayLabel";
			this.gatewayLabel.Size = new System.Drawing.Size(320, 23);
			this.gatewayLabel.TabIndex = 6;
			this.gatewayLabel.Text = "网关";
			this.gatewayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// maskLabel
			// 
			this.maskLabel.Location = new System.Drawing.Point(45, 155);
			this.maskLabel.Name = "maskLabel";
			this.maskLabel.Size = new System.Drawing.Size(320, 23);
			this.maskLabel.TabIndex = 5;
			this.maskLabel.Text = "掩码";
			this.maskLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ipLabel
			// 
			this.ipLabel.Location = new System.Drawing.Point(45, 132);
			this.ipLabel.Name = "ipLabel";
			this.ipLabel.Size = new System.Drawing.Size(320, 23);
			this.ipLabel.TabIndex = 4;
			this.ipLabel.Text = "IP地址";
			this.ipLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// networkLabel
			// 
			this.networkLabel.Location = new System.Drawing.Point(44, 109);
			this.networkLabel.Name = "networkLabel";
			this.networkLabel.Size = new System.Drawing.Size(320, 23);
			this.networkLabel.TabIndex = 3;
			this.networkLabel.Text = "虚拟机配置";
			this.networkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// storageLabel
			// 
			this.storageLabel.Location = new System.Drawing.Point(45, 63);
			this.storageLabel.Name = "storageLabel";
			this.storageLabel.Size = new System.Drawing.Size(320, 23);
			this.storageLabel.TabIndex = 2;
			this.storageLabel.Text = "存储";
			this.storageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// targetHostLabel
			// 
			this.targetHostLabel.Location = new System.Drawing.Point(45, 40);
			this.targetHostLabel.Name = "targetHostLabel";
			this.targetHostLabel.Size = new System.Drawing.Size(320, 23);
			this.targetHostLabel.TabIndex = 1;
			this.targetHostLabel.Text = "目标主机";
			this.targetHostLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// filePathLabel
			// 
			this.filePathLabel.Location = new System.Drawing.Point(45, 17);
			this.filePathLabel.Name = "filePathLabel";
			this.filePathLabel.Size = new System.Drawing.Size(320, 23);
			this.filePathLabel.TabIndex = 0;
			this.filePathLabel.Text = "文件路径";
			this.filePathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ImportVMPage
			// 
			this.Controls.Add(this.label1);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.importButton);
			this.Name = "ImportVMPage";
			this.Controls.SetChildIndex(this.importButton, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.Controls.SetChildIndex(this.progressBar1, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.pictureBox1, 0);
			this.Controls.SetChildIndex(this.pictureBox2, 0);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.pictureBox4, 0);
			this.Controls.SetChildIndex(this.pictureBox5, 0);
			this.Controls.SetChildIndex(this.pictureBox6, 0);
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

		private System.Windows.Forms.Button importButton;
		private System.Windows.Forms.ProgressBar progressBar1;
		private Label label1;
		private GroupBox groupBox1;
		private Label storageLabel;
		private Label targetHostLabel;
		private Label filePathLabel;
		private Label maskLabel;
		private Label ipLabel;
		private Label networkLabel;
		private Label gatewayLabel;
		private Label VMNameLabel;
		private ToolTip toolTip1;
	}
}
