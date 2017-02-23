using System.Collections.Generic;
using WinAPI;

namespace Wizard.Core {
	class NetworkOper {
		
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		
		public NetworkOper() { 
		
		}

		public List<Network> getNetworkList() {
			log.InfoFormat("=======开始获取网络======");
			List<Network> networkList = new List<Network>();
			List<XenRef<Network>> nwRefs = Network.get_all(ConnectManager.session);
			foreach (XenRef<Network> nwRef in nwRefs) {
				Network network = Network.get_record(ConnectManager.session, nwRef);
				log.InfoFormat("uuid: {0}", network.uuid);
				log.InfoFormat("name: {0}", network.name_label);
				log.InfoFormat("name_description: {0}", network.name_description);
				log.InfoFormat("bridge: {0}", network.bridge);
				log.InfoFormat("MTU: {0}", network.MTU);
				log.InfoFormat("PIFs Count: {0}", network.PIFs.Count);
				//network.PIFs.Count > 0
				if (!"xenapi".Equals(network.bridge)) {
					networkList.Add(network);
				}
			}
			log.InfoFormat("=======获取网络完成======");
			return networkList;
		}

		public Network getNetworkByVif(VIF vif) {
			XenRef<Network> nwRef = vif.network;
			Network network = Network.get_record(ConnectManager.session, nwRef);
			return network;
		}

		public XenRef<Network> getNetworkRef(Network network) {
			XenRef<Network> nwRef = Network.get_by_uuid(ConnectManager.session, network.uuid);
			return nwRef;
		}

		public void setVIF(VIF vif, Network network) {
			vif.network = getNetworkRef(network);
			ConnectManager.VIF = vif;
			ConnectManager.NetworkName = network.name_label;
		}

	}
}
