namespace WinAPI
{
    using CookComputing.XmlRpc;
    using System;

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public class Proxy_GPU_group
    {
        public string[] GPU_types;
        public string name_description;
        public string name_label;
        public object other_config;
        public string[] PGPUs;
        public string uuid;
        public string[] VGPUs;
    }
}

