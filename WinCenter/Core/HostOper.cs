using System;
using WinAPI;
using System.Collections.Generic;

namespace Wizard.Core {
	class HostOper {

		public HostOper() { 
			
		}

		public Host getTargetHost() {
			Host targetHost = null;
			List<XenRef<Host>> hostRefs = Host.get_all(ConnectManager.session);
			if (hostRefs != null && hostRefs.Count > 0) {
				foreach (XenRef<Host> hostRef in hostRefs) {
					Host host = Host.get_record(ConnectManager.session, hostRef);
					if (host.address.Equals(ConnectManager.TargetHostName)) {
						targetHost = ConnectManager.TargetHost = host;
						break;	
					}
				}
			}
			return targetHost;
		}

		public Boolean IsConnect() {
			List<XenRef<Host>> hostList = Host.get_all(ConnectManager.session);
			if (hostList != null && hostList.Count > 0) {
				return true;
			}
			return false;
		}

	}
}
