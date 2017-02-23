namespace Wizard {
	partial class ImportSourcePage {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportSourcePage));
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxFile = new System.Windows.Forms.TextBox();
			this.buttonBrowse = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.ctrlErrorLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(278, 78);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(282, 27);
			this.label1.TabIndex = 6;
			this.label1.Text = "请点击’浏览’按钮，选择需要导入的模板(*.xva)";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxFile
			// 
			this.textBoxFile.Location = new System.Drawing.Point(292, 125);
			this.textBoxFile.Name = "textBoxFile";
			this.textBoxFile.Size = new System.Drawing.Size(239, 21);
			this.textBoxFile.TabIndex = 7;
			this.textBoxFile.TextChanged += new System.EventHandler(this.textBoxFile_TextChanged);
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonBrowse.BackgroundImage")));
			this.buttonBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.buttonBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonBrowse.ForeColor = System.Drawing.Color.Transparent;
			this.buttonBrowse.Location = new System.Drawing.Point(537, 125);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(65, 23);
			this.buttonBrowse.TabIndex = 8;
			this.buttonBrowse.Text = "浏览";
			this.buttonBrowse.UseVisualStyleBackColor = true;
			this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(216, 125);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 23);
			this.label2.TabIndex = 9;
			this.label2.Text = "模板路径：";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ctrlErrorLabel
			// 
			this.ctrlErrorLabel.ForeColor = System.Drawing.Color.Red;
			this.ctrlErrorLabel.Location = new System.Drawing.Point(216, 162);
			this.ctrlErrorLabel.Name = "ctrlErrorLabel";
			this.ctrlErrorLabel.Size = new System.Drawing.Size(386, 26);
			this.ctrlErrorLabel.TabIndex = 10;
			this.ctrlErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.ctrlErrorLabel.Visible = false;
			// 
			// ImportSourcePage
			// 
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxFile);
			this.Controls.Add(this.buttonBrowse);
			this.Controls.Add(this.ctrlErrorLabel);
			this.Name = "ImportSourcePage";
			this.Controls.SetChildIndex(this.pictureBox1, 0);
			this.Controls.SetChildIndex(this.pictureBox2, 0);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.pictureBox4, 0);
			this.Controls.SetChildIndex(this.pictureBox5, 0);
			this.Controls.SetChildIndex(this.pictureBox6, 0);
			this.Controls.SetChildIndex(this.ctrlErrorLabel, 0);
			this.Controls.SetChildIndex(this.buttonBrowse, 0);
			this.Controls.SetChildIndex(this.textBoxFile, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxFile;
		private System.Windows.Forms.Button buttonBrowse;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label ctrlErrorLabel;
	}
}
