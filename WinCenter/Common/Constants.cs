using System.Text.RegularExpressions;

namespace Wizard {
	class Constants {

		public const string DEFAULT_TMP_FILE_NAME = "CNware虚拟化管理系统镜像-CentOS";

		public static readonly Regex MacRegex = new Regex(@"(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])");

		public const double UINT = 1073741824D; //1024D * 1024D * 1024D，转换成GB

		public const string AE_ISO_REPO_NAME = "WinCenter_Local_ISO";

		public const string IDENTITY_FILENAME = "id_rsa";

		public const string AE_ISO_REPO_LOCATION = "/var/opt/xen/Wincenter_ISO_Store";
		
		// wce的AE库KEY
		public const string AE_ISO_REPO_OTHER_KEY= "author";

		// wce的AE库VALUE
		public const string AE_ISO_REPO_OTHER_VALUE = "Wincenter";

		// 主机UUID
		public const string AE_ISO_REPO_HOST_KEY = "HOST-UUID";

		public const string AE_XML_TMP_FILE = "/tmp/ovf-env.xml";

		public const string WinServer = "WinServer";

		public const string XenServer = "XenServer";

		public const int WinServerPort = 4430;

		public const int SSHPort = 22;
	}
}
