using System.Collections.Generic;
using WizardLib;
using Wizard.Core;
using System;
using WinAPI;
using System.Windows.Forms;
using System.ComponentModel;
using Wizard.Common;
using System.Threading;

namespace Wizard {
	public partial class StoragePage : InteriorWizardPage {

		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private SROper sROper;
		
		public StoragePage() {
			InitializeComponent();
			
		}

		protected override bool OnSetActive() {
			if (!base.OnSetActive())
				return false;

			log.InfoFormat("进入【存储配置】页面");
			SelectSRInfoLabel.Text = "";

			//ThreadStart threadStart = new ThreadStart(setVmName);
			//Thread thread = new Thread(threadStart);
			//thread.Start();
			
			string vdiSize = string.Format("{0:0.00}GB", ConnectManager.DiskCapacity / Constants.UINT);
			label1.Text = string.Format("所需磁盘容量为{0}, 请选择可用容量大于{1}的存储池", vdiSize, vdiSize);
			sROper = new SROper();
			List<SRVo> srList = sROper.getSRByTargetHost();
			////排序：降序
			//Reverser<SRVo> reverser = new Reverser<SRVo>(new SRVo().GetType(), "AvailableCapacity", ReverserInfo.Direction.DESC);
			//srList.Sort(reverser);
			////排序
			//srList.Sort(delegate(SRVo x, SRVo y) { return x.AvailableCapacity.CompareTo(y.AvailableCapacity);});
			////排序，使用Lambda表达式，升序
			//srList.Sort((x, y) => x.AvailableCapacity.CompareTo(y.AvailableCapacity));
			//排序，使用Lambda表达式，降序
			srList.Sort((x, y) => y.AvailableCapacity.CompareTo(x.AvailableCapacity));
			if (srList.Count > 0) {
				storageDataGridView.RowCount = srList.Count;
				storageDataGridView.Rows[0].Selected = true;//每次默认选中第一行
				for (int i = 0; i < srList.Count; i++) {
					storageDataGridView.Rows[i].Cells["uuidColumn"].Value = srList[i].Uuid;
					//int index = storageDataGridView.CurrentRow == null ? 0 : storageDataGridView.CurrentRow.Index;
					int index = 0;
					if (i == index) {
						storageDataGridView.Rows[i].Cells["checkBoxColumn"].Value = SelectedStatus.Selected;
					}
					else {
						storageDataGridView.Rows[i].Cells["checkBoxColumn"].Value = SelectedStatus.NoSelected;
					}

					storageDataGridView.Rows[i].Cells["nameLabelColumn"].Value = srList[i].Name;
					storageDataGridView.Rows[i].Cells["AvailableCapacityColumn"].Value = string.Format("{0:0.00}GB", srList[i].AvailableCapacity / Constants.UINT);
					storageDataGridView.Rows[i].Cells["physicalSizeColumn"].Value = string.Format("{0:0.00}GB", srList[i].PhysicalSize / Constants.UINT);
					storageDataGridView.Rows[i].Cells["TypeColumn"].Value = srList[i].Type;
					storageDataGridView.Rows[i].Cells["infoColumn"].Value = srList[i].Info;
				}
				//storageDataGridView.Sort(storageDataGridView.Columns["AvailableCapacityColumn"], ListSortDirection.Descending);
				Wizard.SetWizardButtons(WizardButton.Back | WizardButton.Next);
			} else {
				Wizard.SetWizardButtons(WizardButton.Back);
			}
			
			return true;
		}

		protected override string OnWizardNext() {
			SelectSRInfoLabel.Text = "";
			Object o = this.storageDataGridView.CurrentRow.Cells["infoColumn"].Value;
			if (o != null) {
				SelectSRInfoLabel.Text = o.ToString();
				return null;
			}
			string uuid = this.storageDataGridView.CurrentRow.Cells["uuidColumn"].Value.ToString();
			string name = this.storageDataGridView.CurrentRow.Cells["nameLabelColumn"].Value.ToString();
			ConnectManager.SelectedSR = sROper.getSrByUuid(uuid);
			log.InfoFormat("已选择存储池,sr=[uuid:{0}, name:{1}]", uuid, name);
			return base.OnWizardNext();
		}


		private void storageDataGridView_CellClick(object sender, DataGridViewCellEventArgs e) {
			if (e.RowIndex >= 0) {
				//Cells[1]为checkBoxColumn列
				DataGridViewDisableCheckBoxCell cell = storageDataGridView.Rows[e.RowIndex].Cells["checkBoxColumn"] as DataGridViewDisableCheckBoxCell;
				if (!cell.Enabled) {
					return;
				}
				string isSelected = cell.Value == null ? "NoSelected" : cell.Value.ToString();
				if (SelectedStatus.NoSelected.ToString().Equals(isSelected)) {
					cell.Value = SelectedStatus.Selected;
					SetRadioButtonValue(cell);
				}
			}
		}

		private void SetRadioButtonValue(DataGridViewDisableCheckBoxCell cell) {
			SelectedStatus status = (SelectedStatus)cell.Value;
			if (status == SelectedStatus.Selected) {
				status = SelectedStatus.NoSelected;
			}
			else {
				status = SelectedStatus.Selected;
			}
			for (int i = 0; i < storageDataGridView.Rows.Count; i++) {
				DataGridViewDisableCheckBoxCell cel = storageDataGridView.Rows[i].Cells["checkBoxColumn"] as DataGridViewDisableCheckBoxCell;
				if (!cel.Equals(cell)) {
					cel.Value = status;
				}
			}
		}

		private void setVmName() {
			VMOper vmOper = new VMOper();
			vmOper.DefaultVMName(ConnectManager.VMName);
		}

	}
}
