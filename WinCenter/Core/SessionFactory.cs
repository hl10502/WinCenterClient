using WinAPI;

namespace Wizard.Core {
	public static class SessionFactory {
		public static Session CreateSession(string hostname, int port) {
			return new Session(Session.STANDARD_TIMEOUT, hostname, port);
		}

		public static Session CreateSession(Session session, int timeout) {
			return new Session(session, timeout);
		}
	}
}
