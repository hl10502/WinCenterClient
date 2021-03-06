﻿using System.Net;

namespace Wizard
{
    public partial class HTTP_actions
    {

		private static void Get(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, string remotePath, IWebProxy proxy, string localPath, params object[] args) {
			HTTP.Get(dataCopiedDelegate, cancellingDelegate, HTTP.BuildUri(hostname, remotePath, args), proxy, localPath, timeout_ms);
		}

		private static void Put(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, string remotePath, IWebProxy proxy, string localPath, params object[] args) {
			HTTP.Put(progressDelegate, cancellingDelegate, HTTP.BuildUri(hostname, remotePath, args), proxy, localPath, timeout_ms);
		}

		public static void get_services(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id) {
			Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/services", proxy, path,
				"task_id", task_id, "session_id", session_id);
		}

		public static void put_import(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id, bool restore, bool force, string sr_id) {
			Put(progressDelegate, cancellingDelegate, timeout_ms, hostname, "/import", proxy, path,
				"task_id", task_id, "session_id", session_id, "restore", restore, "force", force, "sr_id", sr_id);
		}

		public static void put_import_metadata(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id, bool restore, bool force) {
			Put(progressDelegate, cancellingDelegate, timeout_ms, hostname, "/import_metadata", proxy, path,
				"task_id", task_id, "session_id", session_id, "restore", restore, "force", force);
		}

		public static void put_import_raw_vdi(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id, string vdi) {
			Put(progressDelegate, cancellingDelegate, timeout_ms, hostname, "/import_raw_vdi", proxy, path,
				"task_id", task_id, "session_id", session_id, "vdi", vdi);
		}

		public static void get_export(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id, string uuid) {
			Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/export", proxy, path,
				"task_id", task_id, "session_id", session_id, "uuid", uuid);
		}

		public static void get_export_metadata(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id, string uuid) {
			Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/export_metadata", proxy, path,
				"task_id", task_id, "session_id", session_id, "uuid", uuid);
		}

		public static void get_export_raw_vdi(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id, string vdi) {
			Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/export_raw_vdi", proxy, path,
				"task_id", task_id, "session_id", session_id, "vdi", vdi);
		}

		public static void get_host_backup(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id) {
			Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/host_backup", proxy, path,
				"task_id", task_id, "session_id", session_id);
		}

		public static void put_host_restore(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id) {
			Put(progressDelegate, cancellingDelegate, timeout_ms, hostname, "/host_restore", proxy, path,
				"task_id", task_id, "session_id", session_id);
		}

		public static void get_host_logs_download(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id) {
			Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/host_logs_download", proxy, path,
				"task_id", task_id, "session_id", session_id);
		}

		public static void put_pool_patch_upload(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id) {
			Put(progressDelegate, cancellingDelegate, timeout_ms, hostname, "/pool_patch_upload", proxy, path,
				"task_id", task_id, "session_id", session_id);
		}

		public static void get_pool_patch_download(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id, string uuid) {
			Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/pool_patch_download", proxy, path,
				"task_id", task_id, "session_id", session_id, "uuid", uuid);
		}

		public static void put_oem_patch_stream(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id) {
			Put(progressDelegate, cancellingDelegate, timeout_ms, hostname, "/oem_patch_stream", proxy, path,
				"task_id", task_id, "session_id", session_id);
		}

		public static void get_vncsnapshot(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id, string uuid) {
			Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/vncsnapshot", proxy, path,
				"task_id", task_id, "session_id", session_id, "uuid", uuid);
		}

		public static void get_pool_xml_db_sync(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id) {
			Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/pool/xmldbdump", proxy, path,
				"task_id", task_id, "session_id", session_id);
		}

		public static void get_system_status(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id, string entries, string output) {
			Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/system-status", proxy, path,
				"task_id", task_id, "session_id", session_id, "entries", entries, "output", output);
		}

		public static void vm_rrd(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id, string uuid) {
			Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/vm_rrd", proxy, path,
				"task_id", task_id, "session_id", session_id, "uuid", uuid);
		}

		public static void host_rrd(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id, bool json) {
			Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/host_rrd", proxy, path,
				"task_id", task_id, "session_id", session_id, "json", json);
		}

		public static void rrd_updates(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id, long start, string cf, long interval, bool host, string uuid, bool json) {
			Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/rrd_updates", proxy, path,
				"task_id", task_id, "session_id", session_id, "start", start, "cf", cf, "interval", interval, "host", host, "uuid", uuid, "json", json);
		}

		public static void put_blob(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id, string reff) {
			Put(progressDelegate, cancellingDelegate, timeout_ms, hostname, "/blob", proxy, path,
				"task_id", task_id, "session_id", session_id, "ref", reff);
		}

		public static void get_wlb_report(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id, string report, params string[] args /* alternate names & values */) {
			Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/wlb_report", proxy, path,
				"task_id", task_id, "session_id", session_id, "report", report, args);
		}

		public static void get_wlb_diagnostics(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id) {
			Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/wlb_diagnostics", proxy, path,
				"task_id", task_id, "session_id", session_id);
		}

		public static void get_audit_log(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
			string hostname, IWebProxy proxy, string path, string task_id, string session_id) {
			Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/audit_log", proxy, path,
				"task_id", task_id, "session_id", session_id);
		}
	
		public delegate void get_ss(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
            string hostname, IWebProxy proxy, string path, string arg1, string arg2);
        public delegate void get_sss(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
            string hostname, IWebProxy proxy, string path, string arg1, string arg2, string arg3);
        public delegate void get_ssss(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
            string hostname, IWebProxy proxy, string path, string arg1, string arg2, string arg3, string arg4);

        public delegate void put_ss(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
            string hostname, IWebProxy proxy, string path, string arg1, string arg2);
        public delegate void put_ssbbs(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms,
            string hostname, IWebProxy proxy, string path, string arg1, string arg2, bool arg3, bool arg4, string arg5);
    }
}
