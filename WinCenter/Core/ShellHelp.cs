using System.Text;
using System.IO;
using Tamir.SharpSsh.jsch;

namespace Tamir.SharpSsh {
	public class ShellHelp {

		private MemoryStream outputstream = new MemoryStream();
		private SshStream inputstream = null;

		/// <summary> 
		/// 命令等待标识，用来标识命令是否执行完成的，执行完成就会在后面输出这个字符，有时也有可能是"]$"
		/// </summary> 
		private string waitMark = "]#";
		/// <summary> 
		/// 打开连接 
		/// </summary> 
		/// <param name="host"></param> 
		/// <param name="username"></param> 
		/// <param name="pwd"></param> 
		/// <param name="privateKeyPath"></param> 
		/// <returns></returns> 
		public bool OpenShell(string host, string username, string pwd, string privateKeyPath) {
			try {
				////Redirect standard I/O to the SSH channel 
				inputstream = new SshStream(host, username, pwd, privateKeyPath);
				///我手动加进去的方法。。为了读取输出信息 
				inputstream.set_OutputStream(outputstream);
				return inputstream != null;
			}
			catch { throw; }
		}
		/// <summary> 
		/// 执行命令 
		/// </summary> 
		/// <param name="cmd"></param> 
		public bool Shell(string cmd) {
			if (inputstream == null) return false;
			string initinfo = GetAllString();
			inputstream.Write(cmd);
			inputstream.Flush();
			string currentinfo = GetAllString();
			while (currentinfo == initinfo) {
				System.Threading.Thread.Sleep(100);
				currentinfo = GetAllString();
			}
			return true;
		}
		/// <summary> 
		/// 获取输出信息 
		/// </summary> 
		/// <returns></returns> 
		public string GetAllString() {
			string outinfo = Encoding.UTF8.GetString(outputstream.ToArray());
			//等待命令结束字符 
			while (!outinfo.Trim().EndsWith(waitMark)) {
				System.Threading.Thread.Sleep(200);
				outinfo = Encoding.UTF8.GetString(outputstream.ToArray());
			}
			outputstream.Flush();
			return outinfo.ToString();
		}
		/// <summary> 
		/// 关闭连接 
		/// </summary> 
		public void Close() {
			if (inputstream != null) inputstream.Close();
		}
	
	}
}
