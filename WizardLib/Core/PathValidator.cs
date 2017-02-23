using System;
using System.IO;

namespace WizardLib.Core {
	public class PathValidator {
		private static readonly char[] m_invalidFileCharList = Path.GetInvalidFileNameChars();
		private static readonly string[] m_deviceNames = {
		                                                 	"CON", "PRN", "AUX", "NUL",
		                                                 	"COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
		                                                 	"LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
		                                                 };

		public static bool IsFileNameValid(string filename) {
			if (filename.IndexOfAny(m_invalidFileCharList) > -1)
				return false;

			foreach (var name in m_deviceNames) {
				if (name == filename.ToUpper())
					return false;
			}

			return true;
		}

		public static bool IsPathValid(string path) {
			if (string.IsNullOrEmpty(path))
				return false;

			try {
				if (Path.IsPathRooted(path)) {
					path = path[0] == '\\' && path.Length == 1
							? path.Substring(1)
							: path.Substring(2);
				}
			}
			catch (ArgumentException) {
				//path contains a character from Path.GetInvalidPathChars()
				return false;
			}

			string[] parts = path.Split(new[] { '\\' });

			if (parts.Length > 0) {
				foreach (var part in parts) {
					if (part.IndexOfAny(m_invalidFileCharList) > -1)
						return false;

					foreach (var name in m_deviceNames) {
						if (name == part.ToUpper())
							return false;
					}
				}
			}

			return true;
		}
	}
}

