using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WizardLib;
using WizardLib.Core;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using WizardLib.Archive;
using Wizard.Core;

namespace Wizard {
	public partial class ImportSourcePage : InteriorWizardPage {

		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private readonly string supportedXvaTypes = ".xva";
		private int VifCount = 0;
		private int VcpuCount = 0;
		private ulong memory = 0;
		private string MaxVIFsAllowedValue = null;
		private bool isFirst = true;
		private string nameLabel;

		public ImportSourcePage() {
			InitializeComponent();
		}

		protected override bool OnSetActive() {
			if (!base.OnSetActive())
				return false;

			log.InfoFormat("进入【安装来源】页面");
			if (isFirst) {
				isFirst = false;
				string startupPath = System.Windows.Forms.Application.StartupPath;
				string tmpPath = startupPath + Path.DirectorySeparatorChar + Constants.DEFAULT_TMP_FILE_NAME + supportedXvaTypes;
				if (File.Exists(tmpPath)) {
					textBoxFile.Text = tmpPath;
					textBoxFile.Select(0, 0);
					Wizard.SetWizardButtons(WizardButton.Back | WizardButton.Next);
				}
				else {
					Wizard.SetWizardButtons(WizardButton.Back);
				}
			}
			else {
				Wizard.SetWizardButtons(WizardButton.Back | WizardButton.Next);
			}
			return true;
		}

		protected override string OnWizardNext() {
			log.InfoFormat("开始校验文件");
			string error = null;
			if (!CheckIsSupportedType(out error)) {
				ctrlErrorLabel.Text = error;
				ctrlErrorLabel.Visible = true;
				return null;
			}
			if (!CheckPathExists(out error)) {
				ctrlErrorLabel.Text = error;
				ctrlErrorLabel.Visible = true;
				return null;
			}
			if (!GetDiskCapacityXva(out error)) {
				ctrlErrorLabel.Text = error;
				ctrlErrorLabel.Visible = true;
				return null;
			}

			log.InfoFormat("文件校验完成");
			ctrlErrorLabel.Text = "";
			ctrlErrorLabel.Visible = false;
			ConnectManager.FilePath = textBoxFile.Text;
			return base.OnWizardNext();
		}

		private void buttonBrowse_Click(object sender, EventArgs e) {
			using (FileDialog ofd = new OpenFileDialog {
				CheckFileExists = true,
				CheckPathExists = true,
				DereferenceLinks = true,
				Filter = "XVA files (*.xva)|*.xva",
				RestoreDirectory = true,
				Multiselect = false,
			}) {
				if (ofd.ShowDialog() == DialogResult.OK)
					textBoxFile.Text = ofd.FileName;
			}
		}

		private void textBoxFile_TextChanged(object sender, EventArgs e) {
			string error = null;
			bool flag = CheckPathValid(out error);
			if (!flag) {
				ctrlErrorLabel.Text = error;
				ctrlErrorLabel.Visible = true;
				Wizard.SetWizardButtons(WizardButton.Back);
				return;
			}

			flag = CheckIsSupportedType(out error);
			if (!flag) {
				ctrlErrorLabel.Text = error;
				ctrlErrorLabel.Visible = true;
				Wizard.SetWizardButtons(WizardButton.Back);
				return;
			}

			flag = CheckPathExists(out error);
			if (!flag) {
				ctrlErrorLabel.Text = error;
				ctrlErrorLabel.Visible = true;
				Wizard.SetWizardButtons(WizardButton.Back);
				return;
			}

			ctrlErrorLabel.Text = "";
			ctrlErrorLabel.Visible = true;
			Wizard.SetWizardButtons(WizardButton.Back | WizardButton.Next);
		}

		private bool CheckPathValid(out string error) {
			error = String.Empty;
			string filepath = textBoxFile.Text;
			if (String.IsNullOrEmpty(filepath.TrimEnd())) {
				error = "模板路径不能为空";
				return false;
			}

			//if it's URI ignore
			if (IsUri())
				return true;

			if (!PathValidator.IsPathValid(filepath)) {
				error = "模板路径中包含无效的字符";
				return false;
			}

			return true;
		}

		private bool IsUri() {
			string uriRegex = "^(http|https|file|ftp)://*";
			Regex regex = new Regex(uriRegex, RegexOptions.IgnoreCase);
			return regex.Match(textBoxFile.Text).Success;
		}


		private bool CheckIsSupportedType(out string error) {
			error = string.Empty;

			string filepath = textBoxFile.Text;
			if (filepath.ToLower().EndsWith(supportedXvaTypes)) {
				return true;
			} else {
				error = "文件格式不正确，应为*.xva格式！";
				return false;
			}
			
		}

		
		private bool CheckPathExists(out string error) {
			error = string.Empty;
			string filepath = textBoxFile.Text;
			if (File.Exists(filepath))
				return true;

			error = "模板路径不存在！";
			return false;
		}


		private bool GetDiskCapacityXva(out string error) {
			error = string.Empty;
			ulong ImageLength = 0;
			try {
				FileInfo info = new FileInfo(textBoxFile.Text);
				ImageLength = info.Length > 0 ? (ulong)info.Length : 0;

				SROper sROper = new SROper();
				sROper.DiskCapacity = GetTotalSizeFromXmlXva(GetXmlStringFromTarXVA()); //xva style
				sROper.VifCount = this.VifCount;
				sROper.VcpuCount = this.VcpuCount;
				sROper.Memory = this.memory;
				sROper.vmNameLabel = this.nameLabel;
				int MaxVIFsAllowed = 0;
				try {
					MaxVIFsAllowed = Convert.ToInt32(MaxVIFsAllowedValue);
				}
				catch {
					MaxVIFsAllowed = 0;
				}
				sROper.MaxVIFsAllowed = MaxVIFsAllowed;
			}
			catch (Exception ex ) {
				//error = ex.Message;
				error = "模板文件内容不正确！";
				log.ErrorFormat(error+ "：{0}", ex.Message);
				return false;
			}
			return true;
		}

		private string GetXmlStringFromTarXVA() {
			using (Stream stream = new FileStream(textBoxFile.Text, FileMode.Open, FileAccess.Read)) {
				ArchiveIterator iterator = ArchiveFactory.Reader(ArchiveFactory.Type.Tar, stream);
				if (iterator.HasNext()) {
					Stream ofs = new MemoryStream();
					iterator.ExtractCurrentFile(ofs);
					ofs.Position = 0;
					return new StreamReader(ofs).ReadToEnd();
				}
				return String.Empty;
			}
		}

		private ulong GetTotalSizeFromXmlXva(string xmlString) {
			log.InfoFormat("开始解析配置文件");
			ulong totalSize = 0;
			XmlDocument xmlMetadata = new XmlDocument();
			xmlMetadata.LoadXml(xmlString);
			XPathNavigator nav = xmlMetadata.CreateNavigator();

			XPathNodeIterator nodeIteratorIsaTemplate = nav.Select(".//name[. = \"is_a_template\"]");
			while (nodeIteratorIsaTemplate.MoveNext()) {
				XPathNavigator vdiNavigator = nodeIteratorIsaTemplate.Current;
				if (vdiNavigator.MoveToNext()) {
					string is_a_template = vdiNavigator.Value;
					if (is_a_template == "1") {
						string error = "模板路径xva文件是虚拟机模板文件，请重新选择！";
						log.InfoFormat("{0}", error);
						throw new Exception(error);
					}
				}
			}

			XPathNodeIterator snapshotNodeIterator = nav.Select(".//name[. = \"snapshot\"]");
			while (snapshotNodeIterator.MoveNext()) {
				XPathNavigator snapshotNavigator = snapshotNodeIterator.Current;
				if (snapshotNavigator.MoveToNext()) {
					XPathNodeIterator powerStateIterator = snapshotNavigator.Select(".//name[. = \"power_state\"]");
					bool flag = false;
					while(powerStateIterator.MoveNext()) {
						flag = true;
						break;
					}
					if (flag) {
						XPathNodeIterator nameLabelIterator = snapshotNavigator.Select(".//name[. = \"name_label\"]");
						while (nameLabelIterator.MoveNext()) {
							XPathNavigator vmNameNavigator = nameLabelIterator.Current;
							if (vmNameNavigator.MoveToNext())
								nameLabel = vmNameNavigator.Value;
							break;
						}
						break;
					}
				}
			}
			log.InfoFormat("xva文件中的虚拟机名称为：{0}", nameLabel);

			XPathNodeIterator nodeIterator = nav.Select(".//name[. = \"virtual_size\"]");
			while (nodeIterator.MoveNext()) {
				XPathNavigator vdiNavigator = nodeIterator.Current;
				if (vdiNavigator.MoveToNext())
					totalSize += UInt64.Parse(vdiNavigator.Value);
			}
			log.InfoFormat("xva文件VDI总大小: {0:0.00}GB", (totalSize / Constants.UINT));
			
			//string vifs = "";
			//XPathNodeIterator nodeIteratorVIFs = nav.Select(".//name[. = \"VIFs\"]");
			//while (nodeIteratorVIFs.MoveNext()) {
			//    XPathNavigator vdiNavigator = nodeIteratorVIFs.Current;
			//    if (vdiNavigator.MoveToNext())
			//        if (vifs.IndexOf(vdiNavigator.Value) < 0) {
			//            vifs += vdiNavigator.Value;
			//            this.VifCount++;
			//        }
			//}
			//log.InfoFormat("xva文件中的VIF数量为：{0}", this.VifCount);


			XPathNodeIterator nodeIteratorVCPUs = nav.Select(".//name[. = \"VCPUs_at_startup\"]");
			while (nodeIteratorVCPUs.MoveNext()) {
				XPathNavigator vdiNavigator = nodeIteratorVCPUs.Current;
				if (vdiNavigator.MoveToNext()) {
					this.VcpuCount = Convert.ToInt32(vdiNavigator.Value);
					break;
				}
			}
			log.InfoFormat("xva文件中的VCPU数量为：{0}", this.VcpuCount);


			XPathNodeIterator nodeIteratorMem = nav.Select(".//name[. = \"memory_dynamic_max\"]"); //内存四个值设置同样大小，这里取动态最大值
			while (nodeIteratorMem.MoveNext()) {
				XPathNavigator vdiNavigator = nodeIteratorMem.Current;
				if (vdiNavigator.MoveToNext()) {
					this.memory = UInt64.Parse(vdiNavigator.Value);
					break;
				}
			}
			log.InfoFormat("xva文件中的memory为：{0:0.00}GB", this.memory / Constants.UINT);


			//XmlNode xn = xmlMetadata.SelectSingleNode(@"restrictions/restriction[@property='number-of-vifs']");
			//this.MaxVIFsAllowedValue = xn.Attributes["max"].Value;
			//System.Console.WriteLine("xva文件中的最大允许的VIF数量为：{0}", this.MaxVIFsAllowedValue);

			log.InfoFormat("配置文件解析完成");
			return totalSize;
		}
	}
}
