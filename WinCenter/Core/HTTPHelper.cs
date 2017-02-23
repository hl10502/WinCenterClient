using System;
using System.Collections.Generic;
using WinAPI;
using System.Net;

namespace Wizard.Core {
	class HTTPHelper {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		
		public static int progressPercent = 0;
		private static int changePercent = 0;
		public static string progressInfo;
		public static string errorInfo = "";

		public static String Put(int timeout, string path, string hostname, Delegate f, params object[] p) {
			XenRef<Task> taskRef = Task.create(ConnectManager.session, "INSTALL WinCenter VM", hostname);
			log.InfoFormat("创建Task:{0}", taskRef.opaque_ref);
			try {
				HTTP.UpdateProgressDelegate progressDelegate = delegate(int percent) {
					Tick(percent);
				};
				return Put(progressDelegate, timeout, taskRef, ref ConnectManager.session, path, hostname, f, p);
			}
			finally {
				Task.destroy(ConnectManager.session, taskRef);
				log.InfoFormat("销毁Task:{0}", taskRef.opaque_ref);
			}
		}


		public static String Put(HTTP.UpdateProgressDelegate progressDelegate, int timeout,
			XenRef<Task> taskRef, ref Session session, string path, string hostname, Delegate f, params object[] p) {

			HTTP.FuncBool cancellingDelegate = (HTTP.FuncBool)delegate() {
				return false;
			};

			log.InfoFormat("HTTP导入文件从[{0}]到主机[{1}]", path, hostname);
			try {
				List<object> args = new List<object>();
				args.Add(progressDelegate);
				args.Add(cancellingDelegate);
				args.Add(timeout);
				args.Add(hostname);
				args.Add(null); //IWebProxy
				args.Add(path);
				args.Add(taskRef.opaque_ref);  // task_id
				args.AddRange(p);
				f.DynamicInvoke(args.ToArray());
			} catch(Failure failure) {
				log.InfoFormat("HTTP导入文件失败:{0}", failure.ErrorDescription.ToString());
			}
			catch (Exception ex) {
				log.InfoFormat("HTTP导入文件失败:{0}", ex.Message);
			}

			return PollTaskForResult(ref ConnectManager.session, taskRef);
		}

		private static void Tick(int percent) {
			if (percent < 0)
				percent = 0;
			if (percent > 100)
				percent = 100;
			progressPercent = percent * 90 / 100;
			if (changePercent != percent) {
				changePercent = percent;
				log.InfoFormat("文件导入进度：{0}%，安装进度：{1}%", changePercent, progressPercent);
				if (changePercent == 100) {
					HTTPHelper.progressInfo = "导入完成";
				}
			}
		}

		private static String PollTaskForResult(ref Session session, XenRef<Task> taskRef) {
			task_status_type status;
			do {
				System.Threading.Thread.Sleep(500);
				status = Task.get_status(session, taskRef);
			}
			while (status == task_status_type.pending || status == task_status_type.cancelling);

			if (status == task_status_type.failure) {
				throw new Failure(Task.get_error_info(session, taskRef));
			}
			else {
				return Task.get_result(session, taskRef);
			}
		}
	}
}
