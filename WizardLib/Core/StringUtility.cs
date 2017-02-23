using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WizardLib.Core {
	public static class StringUtility {
		/// <summary>
		/// Parses strings of the form "hostname:port" 
		/// </summary>
		/// <param name="s"></param>
		/// <param name="hostname"></param>
		/// <param name="port"></param>
		/// <returns></returns>
		public static bool TryParseHostname(string s, int defaultPort, out string hostname, out int port) {
			try {
				int i = s.IndexOf(':');
				if (i != -1) {
					hostname = s.Substring(0, i).Trim();
					port = int.Parse(s.Substring(i + 1).Trim());
				}
				else {
					hostname = s;
					port = defaultPort; // Program.DEFAULT_XEN_PORT;
				}
				return true;
			}
			catch (Exception) {
				hostname = null;
				port = 0;
				return false;
			}
		}

		public static int NaturalCompare(string s1, string s2) {
			if (string.Compare(s1, s2, StringComparison.CurrentCultureIgnoreCase) == 0) {
				// Strings are identical
				return 0;
			}

			if (s1 == null)
				return -1;
			if (s2 == null)
				return 1;

			char[] chars1 = s1.ToCharArray();
			char[] chars2 = s2.ToCharArray();

			// Compare strings char by char
			int min = Math.Min(chars1.Length, chars2.Length);
			for (int i = 0; i < min; i++) {
				char c1 = chars1[i];
				char c2 = chars2[i];

				bool c1IsDigit = char.IsDigit(c1);
				bool c2IsDigit = char.IsDigit(c2);

				if (!c1IsDigit && !c2IsDigit) {
					// Two non-digits. Do a string (i.e. alphabetical) comparison.
					int tmp = String.Compare(s1.Substring(i, 1), s2.Substring(i, 1), StringComparison.CurrentCultureIgnoreCase);
					if (tmp == 0)
						continue;  // Identical non-digits. Move onto next character.
					else
						return tmp;
				}
				else if (c1IsDigit && c2IsDigit) {
					// See how many digits there are in a row in each string.
					int j = 1;
					while (i + j < chars1.Length && char.IsDigit(chars1[i + j]))
						j++;
					int k = 1;
					while (i + k < chars2.Length && char.IsDigit(chars2[i + k]))
						k++;

					// A number that is shorter in decimal places must be smaller.
					if (j < k) {
						return -1;
					}
					else if (k < j) {
						return 1;
					}

					// The two integers have the same number of digits. Compare them digit by digit.
					for (int m = i; m < i + j; m++) {
						if (chars1[m] != chars2[m])
							return chars1[m] - chars2[m];
					}

					// Skip the characters we've already compared, so we don't have to do them again. (CA-50738)
					// (It's only j-1, not j, because we get one more in the loop increment).
					i += j - 1;
					continue;
				}
				else {
					// We're comparing a digit to a non-digit.
					return String.Compare(s1.Substring(i, 1), s2.Substring(i, 1), StringComparison.CurrentCultureIgnoreCase);
				}
			}
			// The shorter string comes first.
			return chars1.Length - chars2.Length;
		}

		private static readonly Regex IPRegex = new Regex(@"^([0-9]{1,3})\.([0-9]{1,3})\.([0-9]{1,3})\.([0-9]{1,3})$");
		private static readonly Regex IPRegex0 = new Regex(@"^([0]{1,3})\.([0]{1,3})\.([0]{1,3})\.([0]{1,3})$");

		public static bool IsIPAddress(string s) {
			if (string.IsNullOrEmpty(s))
				return false;

			// check the general form is ok
			Match m = IPRegex.Match(s);
			if (!m.Success)
				return false;

			// check the individual numbers are in range, easier to do this as a parse than with the regex
			for (int i = 1; i < 3; i++) {
				int v;
				if (!int.TryParse(m.Groups[i].Value, out v))
					return false;

				if (v > 255)
					return false;
			}

			Match m2 = IPRegex0.Match(s);
			if (m2.Success)
				return false;

			return true;
		}
	}
}