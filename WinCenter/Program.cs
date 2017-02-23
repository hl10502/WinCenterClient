using System;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Tamir.SharpSsh;

namespace Wizard {
	static class Program {

		private static log4net.ILog log = null;
		
		static Program() {
			log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(Assembly.GetCallingAssembly().Location + ".config"));
            log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		}

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
