namespace Wizard {
	partial class StoragePage {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StoragePage));
			this.label1 = new System.Windows.Forms.Label();
			this.storageDataGridView = new System.Windows.Forms.DataGridView();
			this.uuidColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.checkBoxColumn = new WizardLib.DataGridViewDisableCheckBoxColumn();
			this.nameLabelColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.AvailableCapacityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.physicalSizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.infoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SelectSRInfoLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.storageDataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox3
			// 
			this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(212, 69);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(416, 23);
			this.label1.TabIndex = 6;
			this.label1.Text = "请选择存储";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// storageDataGridView
			// 
			this.storageDataGridView.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
			this.storageDataGridView.AllowUserToAddRows = false;
			this.storageDataGridView.AllowUserToDeleteRows = false;
			this.storageDataGridView.AllowUserToResizeRows = false;
			this.storageDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
			this.storageDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.storageDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.uuidColumn,
            this.checkBoxColumn,
            this.nameLabelColumn,
            this.AvailableCapacityColumn,
            this.physicalSizeColumn,
            this.TypeColumn,
            this.infoColumn});
			this.storageDataGridView.Location = new System.Drawing.Point(212, 106);
			this.storageDataGridView.MultiSelect = false;
			this.storageDataGridView.Name = "storageDataGridView";
			this.storageDataGridView.ReadOnly = true;
			this.storageDataGridView.RowHeadersVisible = false;
			this.storageDataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.storageDataGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			this.storageDataGridView.RowTemplate.Height = 23;
			this.storageDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.storageDataGridView.Size = new System.Drawing.Size(416, 214);
			this.storageDataGridView.TabIndex = 14;
			this.storageDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.storageDataGridView_CellClick);
			// 
			// uuidColumn
			// 
			this.uuidColumn.DataPropertyName = "Uuid";
			this.uuidColumn.HeaderText = "uuid";
			this.uuidColumn.MinimumWidth = 2;
			this.uuidColumn.Name = "uuidColumn";
			this.uuidColumn.ReadOnly = true;
			this.uuidColumn.Visible = false;
			this.uuidColumn.Width = 2;
			// 
			// checkBoxColumn
			// 
			this.checkBoxColumn.DataPropertyName = "Selected";
			this.checkBoxColumn.FalseValue = "NoSelected";
			this.checkBoxColumn.HeaderText = "";
			this.checkBoxColumn.IndeterminateValue = "Indeterminate";
			this.checkBoxColumn.Name = "checkBoxColumn";
			this.checkBoxColumn.ReadOnly = true;
			this.checkBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.checkBoxColumn.TrueValue = "Selected";
			this.checkBoxColumn.Width = 20;
			// 
			// nameLabelColumn
			// 
			this.nameLabelColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.nameLabelColumn.DataPropertyName = "Name";
			this.nameLabelColumn.HeaderText = "名称";
			this.nameLabelColumn.Name = "nameLabelColumn";
			this.nameLabelColumn.ReadOnly = true;
			this.nameLabelColumn.ToolTipText = "Name";
			// 
			// AvailableCapacityColumn
			// 
			this.AvailableCapacityColumn.DataPropertyName = "AvailableCapacity";
			this.AvailableCapacityColumn.HeaderText = "可用容量";
			this.AvailableCapacityColumn.Name = "AvailableCapacityColumn";
			this.AvailableCapacityColumn.ReadOnly = true;
			this.AvailableCapacityColumn.Width = 80;
			// 
			// physicalSizeColumn
			// 
			this.physicalSizeColumn.DataPropertyName = "PhysicalSize";
			this.physicalSizeColumn.HeaderText = "总容量";
			this.physicalSizeColumn.Name = "physicalSizeColumn";
			this.physicalSizeColumn.ReadOnly = true;
			this.physicalSizeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.physicalSizeColumn.Width = 80;
			// 
			// TypeColumn
			// 
			this.TypeColumn.DataPropertyName = "Type";
			this.TypeColumn.HeaderText = "类型";
			this.TypeColumn.Name = "TypeColumn";
			this.TypeColumn.ReadOnly = true;
			this.TypeColumn.Width = 70;
			// 
			// infoColumn
			// 
			this.infoColumn.DataPropertyName = "Info";
			this.infoColumn.HeaderText = "Info";
			this.infoColumn.MinimumWidth = 2;
			this.infoColumn.Name = "infoColumn";
			this.infoColumn.ReadOnly = true;
			this.infoColumn.Visible = false;
			this.infoColumn.Width = 2;
			// 
			// SelectSRInfoLabel
			// 
			this.SelectSRInfoLabel.ForeColor = System.Drawing.Color.Red;
			this.SelectSRInfoLabel.Location = new System.Drawing.Point(212, 333);
			this.SelectSRInfoLabel.Name = "SelectSRInfoLabel";
			this.SelectSRInfoLabel.Size = new System.Drawing.Size(416, 23);
			this.SelectSRInfoLabel.TabIndex = 15;
			this.SelectSRInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// StoragePage
			// 
			this.Controls.Add(this.SelectSRInfoLabel);
			this.Controls.Add(this.storageDataGridView);
			this.Controls.Add(this.label1);
			this.Name = "StoragePage";
			this.Controls.SetChildIndex(this.pictureBox1, 0);
			this.Controls.SetChildIndex(this.pictureBox2, 0);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.pictureBox4, 0);
			this.Controls.SetChildIndex(this.pictureBox5, 0);
			this.Controls.SetChildIndex(this.pictureBox6, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.storageDataGridView, 0);
			this.Controls.SetChildIndex(this.SelectSRInfoLabel, 0);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.storageDataGridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView storageDataGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn uuidColumn;
		private WizardLib.DataGridViewDisableCheckBoxColumn checkBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameLabelColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn AvailableCapacityColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn physicalSizeColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn TypeColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn infoColumn;
		private System.Windows.Forms.Label SelectSRInfoLabel;
	}
}
