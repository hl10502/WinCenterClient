namespace WinAPI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class PGPU : XenObject<PGPU>
    {
        private XenRef<WinAPI.GPU_group> _GPU_group;
        private XenRef<Host> _host;
        private Dictionary<string, string> _other_config;
        private XenRef<WinAPI.PCI> _PCI;
        private string _uuid;

        public PGPU()
        {
        }

        public PGPU(Hashtable table)
        {
            this.uuid = Marshalling.ParseString(table, "uuid");
            this.PCI = Marshalling.ParseRef<WinAPI.PCI>(table, "PCI");
            this.GPU_group = Marshalling.ParseRef<WinAPI.GPU_group>(table, "GPU_group");
            this.host = Marshalling.ParseRef<Host>(table, "host");
            this.other_config = Maps.convert_from_proxy_string_string(Marshalling.ParseHashTable(table, "other_config"));
        }

        public PGPU(Proxy_PGPU proxy)
        {
            this.UpdateFromProxy(proxy);
        }

        public PGPU(string uuid, XenRef<WinAPI.PCI> PCI, XenRef<WinAPI.GPU_group> GPU_group, XenRef<Host> host, Dictionary<string, string> other_config)
        {
            this.uuid = uuid;
            this.PCI = PCI;
            this.GPU_group = GPU_group;
            this.host = host;
            this.other_config = other_config;
        }

        public static void add_to_other_config(Session session, string _pgpu, string _key, string _value)
        {
            session.proxy.pgpu_add_to_other_config(session.uuid, (_pgpu != null) ? _pgpu : "", (_key != null) ? _key : "", (_value != null) ? _value : "").parse();
        }

        public bool DeepEquals(PGPU other)
        {
            if (object.ReferenceEquals(null, other))
            {
                return false;
            }
            return (object.ReferenceEquals(this, other) || (((Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<XenRef<WinAPI.PCI>>(this._PCI, other._PCI)) && (Helper.AreEqual2<XenRef<WinAPI.GPU_group>>(this._GPU_group, other._GPU_group) && Helper.AreEqual2<XenRef<Host>>(this._host, other._host))) && Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config)));
        }

        public static List<XenRef<PGPU>> get_all(Session session)
        {
            return XenRef<PGPU>.Create(session.proxy.pgpu_get_all(session.uuid).parse());
        }

        public static Dictionary<XenRef<PGPU>, PGPU> get_all_records(Session session)
        {
            return XenRef<PGPU>.Create<Proxy_PGPU>(session.proxy.pgpu_get_all_records(session.uuid).parse());
        }

        public static XenRef<PGPU> get_by_uuid(Session session, string _uuid)
        {
            return XenRef<PGPU>.Create(session.proxy.pgpu_get_by_uuid(session.uuid, (_uuid != null) ? _uuid : "").parse());
        }

        public static XenRef<WinAPI.GPU_group> get_GPU_group(Session session, string _pgpu)
        {
            return XenRef<WinAPI.GPU_group>.Create(session.proxy.pgpu_get_gpu_group(session.uuid, (_pgpu != null) ? _pgpu : "").parse());
        }

        public static XenRef<Host> get_host(Session session, string _pgpu)
        {
            return XenRef<Host>.Create(session.proxy.pgpu_get_host(session.uuid, (_pgpu != null) ? _pgpu : "").parse());
        }

        public static Dictionary<string, string> get_other_config(Session session, string _pgpu)
        {
            return Maps.convert_from_proxy_string_string(session.proxy.pgpu_get_other_config(session.uuid, (_pgpu != null) ? _pgpu : "").parse());
        }

        public static XenRef<WinAPI.PCI> get_PCI(Session session, string _pgpu)
        {
            return XenRef<WinAPI.PCI>.Create(session.proxy.pgpu_get_pci(session.uuid, (_pgpu != null) ? _pgpu : "").parse());
        }

        public static PGPU get_record(Session session, string _pgpu)
        {
            return new PGPU(session.proxy.pgpu_get_record(session.uuid, (_pgpu != null) ? _pgpu : "").parse());
        }

        public static string get_uuid(Session session, string _pgpu)
        {
            return session.proxy.pgpu_get_uuid(session.uuid, (_pgpu != null) ? _pgpu : "").parse();
        }

        public static void remove_from_other_config(Session session, string _pgpu, string _key)
        {
            session.proxy.pgpu_remove_from_other_config(session.uuid, (_pgpu != null) ? _pgpu : "", (_key != null) ? _key : "").parse();
        }

        public override string SaveChanges(Session session, string opaqueRef, PGPU server)
        {
            if (opaqueRef == null)
            {
                return "";
            }
            if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
            {
                set_other_config(session, opaqueRef, this._other_config);
            }
            return null;
        }

        public static void set_other_config(Session session, string _pgpu, Dictionary<string, string> _other_config)
        {
            session.proxy.pgpu_set_other_config(session.uuid, (_pgpu != null) ? _pgpu : "", Maps.convert_to_proxy_string_string(_other_config)).parse();
        }

        public Proxy_PGPU ToProxy()
        {
            return new Proxy_PGPU { uuid = (this.uuid != null) ? this.uuid : "", PCI = (this.PCI != null) ? ((string) this.PCI) : "", GPU_group = (this.GPU_group != null) ? ((string) this.GPU_group) : "", host = (this.host != null) ? ((string) this.host) : "", other_config = Maps.convert_to_proxy_string_string(this.other_config) };
        }

        public override void UpdateFrom(PGPU update)
        {
            this.uuid = update.uuid;
            this.PCI = update.PCI;
            this.GPU_group = update.GPU_group;
            this.host = update.host;
            this.other_config = update.other_config;
        }

        internal void UpdateFromProxy(Proxy_PGPU proxy)
        {
            this.uuid = (proxy.uuid == null) ? null : proxy.uuid;
            this.PCI = (proxy.PCI == null) ? null : XenRef<WinAPI.PCI>.Create(proxy.PCI);
            this.GPU_group = (proxy.GPU_group == null) ? null : XenRef<WinAPI.GPU_group>.Create(proxy.GPU_group);
            this.host = (proxy.host == null) ? null : XenRef<Host>.Create(proxy.host);
            this.other_config = (proxy.other_config == null) ? null : Maps.convert_from_proxy_string_string(proxy.other_config);
        }

        public virtual XenRef<WinAPI.GPU_group> GPU_group
        {
            get
            {
                return this._GPU_group;
            }
            set
            {
                if (!Helper.AreEqual(value, this._GPU_group))
                {
                    this._GPU_group = value;
                    base.Changed = true;
                    base.NotifyPropertyChanged("GPU_group");
                }
            }
        }

        public virtual XenRef<Host> host
        {
            get
            {
                return this._host;
            }
            set
            {
                if (!Helper.AreEqual(value, this._host))
                {
                    this._host = value;
                    base.Changed = true;
                    base.NotifyPropertyChanged("host");
                }
            }
        }

        public virtual Dictionary<string, string> other_config
        {
            get
            {
                return this._other_config;
            }
            set
            {
                if (!Helper.AreEqual(value, this._other_config))
                {
                    this._other_config = value;
                    base.Changed = true;
                    base.NotifyPropertyChanged("other_config");
                }
            }
        }

        public virtual XenRef<WinAPI.PCI> PCI
        {
            get
            {
                return this._PCI;
            }
            set
            {
                if (!Helper.AreEqual(value, this._PCI))
                {
                    this._PCI = value;
                    base.Changed = true;
                    base.NotifyPropertyChanged("PCI");
                }
            }
        }

        public virtual string uuid
        {
            get
            {
                return this._uuid;
            }
            set
            {
                if (!Helper.AreEqual(value, this._uuid))
                {
                    this._uuid = value;
                    base.Changed = true;
                    base.NotifyPropertyChanged("uuid");
                }
            }
        }
    }
}

