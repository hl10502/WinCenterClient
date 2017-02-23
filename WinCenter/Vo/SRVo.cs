
using System;
using System.Collections.Generic;
namespace Wizard {
	class SRVo {
		
		public SRVo() {
		
		}

		private string _uuid;
		private string _name;
		private long _availableCapacity;
		private long _physicalSize;
		private string _type;
		private string _info;

		public string Uuid {
			set {
				_uuid = value;
			}
			get {
				return _uuid;
			}
		}

		public string Name {
			set {
				_name = value;
			}
			get {
				return _name;
			}
		}

		public long AvailableCapacity {
			set {
				_availableCapacity = value;
			}
			get {
				return _availableCapacity;
			}
		}

		public long PhysicalSize {
			set {
				_physicalSize = value;
			}
			get {
				return _physicalSize;
			}
		}

		public string Type {
			set {
				_type = value;
			}
			get {
				return _type;
			}
		}

		public string Info {
			set {
				_info = value;
			}
			get {
				return _info;
			}
		}

	}
}
