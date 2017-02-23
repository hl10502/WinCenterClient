namespace WinAPI
{
    using CookComputing.XmlRpc;
    using System;

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public class Proxy_PGPU
    {
        public string GPU_group;
        public string host;
        public object other_config;
        public string PCI;
        public string uuid;
    }
}

