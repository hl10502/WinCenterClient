using System.Collections.Generic;
using WinAPI;

namespace Wizard.Core {
	class VIFOper {

		public List<VIF> getVIFList() {
			List<VIF> vifList = new List<VIF>();
			List<XenRef<VIF>> vifRefs = VIF.get_all(ConnectManager.session);
			foreach (XenRef<VIF> vifRef in vifRefs) {
				VIF vif = VIF.get_record(ConnectManager.session, vifRef);
				vifList.Add(vif);
			}
			return vifList;
		}

	}
}
