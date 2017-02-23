using System;
using WinAPI;
using System.Collections.Generic;


namespace Wizard.Core {
	class ConnectManager {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public const int DEFAULT_XEN_PORT = 80;//默认80
		public const int DEFAULT_NUM_VIFS_ALLOWED = 7;

		public static Session session;//连接session

		private static string filePath;//文件路径
		private static Host targetHost;//目标主机
		private static string targetHostName;//目标主机IP
		private static string targetHostUserName;//目标主机用户名
		private static string targetHostPassword;//目标主机密码
		private static string masterHostName;//master主机IP
		private static ulong diskCapacity;//ova.xml文件中的VDI总大小
		private static int vifCount;//ova.xml文件中的VIF数量
		private static int maxVIFsAllowed;//ova.xml文件中的最大允许的VIF数量
		private static int vcpuCount;//ova.xml文件中的VCPU数量
		private static ulong memory;//ova.xml文件中的内存大小
		private static SR sr;
		private static VIF vif;
		private static string networkName;
		private static Dictionary<string, string> iPMaskGatewayDict;
		private static string vmname;
		private static string WINSERVER_API_VERSION_2_0 = "WS-2.0";
        private static int connectPort; //连接的端口

		public static bool IsConn;

        public static void Connect(String hostName, String userName, String password, bool isMaster)
        {
            log.InfoFormat("开始连接主机[{0}], 用户名{1}", hostName, userName);
			try {

                session = SessionFactory.CreateSession(hostName, DEFAULT_XEN_PORT);
				//session.login_with_password(userName, password, API_Version.LATEST);
				session.login_with_password(userName, password, WINSERVER_API_VERSION_2_0); //修改XAPI连接版本号
				if (!isMaster) {
					targetHostUserName = userName;
					targetHostPassword = password;
				}
                ConnectPort = DEFAULT_XEN_PORT; //使用默认的80端口
                log.InfoFormat("连接主机[{0}]成功", hostName);
			}
			catch (Failure f) {
				if (f.ErrorDescription.Count > 0) {
					switch (f.ErrorDescription[0])
                        {
							case "HOST_IS_SLAVE":
                                // we know it is a slave so there there is no need to try and connect again, we need to connect to the master
								masterHostName = f.ErrorDescription[1];
								log.InfoFormat("master主机是[{0}]", masterHostName);
								//已连上目标主机，保存目标主机的用户名和密码（目标主机是从节点）
								targetHostUserName = userName;
							    targetHostPassword = password;
								//需要去连接master
								Connect(masterHostName, userName, password, true);
								break;
							case "SESSION_AUTHENTICATION_FAILED": 
								//用户名或密码错误Count=3
							case "RBAC_PERMISSION_DENIED":
                                // No point retrying this, the user needs the read only role at least to log in
                            case "HOST_UNKNOWN_TO_MASTER":
                                // Will never succeed, CA-74718
                                throw;
                        }
				}
			}
			catch (Exception ex) {
                log.InfoFormat("连接主机[{0}]失败", hostName);
				log.Error(ex.Message);
				throw;
			}
		}


        public static bool TryParseHostname(string s, int defaultPort, out string hostname, out int port)
        {
            try
            {
                int i = s.IndexOf(':');
                if (i != -1)
                {
                    hostname = s.Substring(0, i).Trim();
                    port = int.Parse(s.Substring(i + 1).Trim());
                }
                else
                {
                    hostname = s;
                    port = defaultPort; // DEFAULT_XEN_PORT;
                }
                return true;
            }
            catch (Exception)
            {
                hostname = null;
                port = 0;
                return false;
            }
        }

		public static void closeConnect() {
			if (session != null) {
				session.logout();
				session = null;
			}
		}

		
		public static Host TargetHost {
			set {
				targetHost = value;
			}
			get {
				return targetHost;
			}
		}

		public static string TargetHostName {
			set {
				targetHostName = value;
			}
			get {
				return targetHostName;
			}
		}

		public static string MasterHostName {
			set {
			    masterHostName = value;
			}
			get {
				return masterHostName;
			}
		}

		public static ulong DiskCapacity {
			set {
				diskCapacity = value;
			}
			get {
				return diskCapacity;
			}
		}

		public static int VifCount {
			set {
				vifCount = value;
			}
			get {
				return vifCount;
			}
		}


		public static int VcpuCount {
			set {
				vcpuCount = value;
			}
			get {
				return vcpuCount;
			}
		}

		public static ulong Memory {
			set {
				memory = value;
			}
			get {
				return memory;
			}
		}


		public static int MaxVIFsAllowed {
			set {
				maxVIFsAllowed = value;
			}
			get {
				if (maxVIFsAllowed == 0) {
					return DEFAULT_NUM_VIFS_ALLOWED;
				}
				return maxVIFsAllowed;
			}
		}

		public static SR SelectedSR {
			set {
				sr = value;
			}
			get {
				return sr;
			}
		}

		public static VIF VIF {
			set {
				vif = value;
			}
			get {
				return vif;
			}
		}

		public static string NetworkName {
			set {
				networkName = value;
			}
			get {
				return networkName;
			}
		}

		public static string FilePath {
			set {
				filePath = value;
			}
			get {
				return filePath;
			}
		}

		public static Dictionary<string, string> IPMaskGatewayDict {
			set {
				iPMaskGatewayDict = value;
			}
			get {
				return iPMaskGatewayDict;
			}
		}

		public static string VMName {
			set {
				vmname = value;
			}
			get {
				return vmname;
			}
		}


		public static string TargetHostUserName {
			set {
				targetHostUserName = value;
			}
			get {
				return targetHostUserName;
			}
		}

		public static string TargetHostPassword {
			set {
				targetHostPassword = value;
			}
			get {
				return targetHostPassword;
			}
		}

        public static int ConnectPort
        {
            set
            {
                connectPort = value;
            }
            get
            {
                return connectPort;
            }
        }

	}
}
