using System.Collections.Generic;
using WinAPI;

namespace Wizard.Core {
	class PIFOper {

		private Network network;

		public Dictionary<string, string> getNetworkInfo(string hostUuid) {
			Dictionary<string, string> dict = new Dictionary<string, string>();
			List<XenRef<PIF>> vifRefs = PIF.get_all(ConnectManager.session);
			foreach (XenRef<PIF> vifRef in vifRefs) {
				PIF vif = PIF.get_record(ConnectManager.session, vifRef);
				if (vif.management) {
					XenRef<Host> hostRef = vif.host;
					Host host = Host.get_record(ConnectManager.session, hostRef);
					if (hostUuid.Equals(host.uuid)) {
						string ip = vif.IP;
						dict.Add("ip", ip);
						string netmask = vif.netmask;
						dict.Add("netmask", netmask);
						string gateway = vif.gateway;
						dict.Add("gateway", gateway);
						XenRef<Network> networkRef = vif.network;
						network = Network.get_record(ConnectManager.session, networkRef);
					}
				}
			}
			return dict;
		}

		public Network ManageNetwork {
			get {
				return network;
			}
		}

	}
}
