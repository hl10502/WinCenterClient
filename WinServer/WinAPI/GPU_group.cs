namespace WinAPI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class GPU_group : XenObject<GPU_group>
    {
        private string[] _GPU_types;
        private string _name_description;
        private string _name_label;
        private Dictionary<string, string> _other_config;
        private List<XenRef<PGPU>> _PGPUs;
        private string _uuid;
        private List<XenRef<VGPU>> _VGPUs;

        public GPU_group()
        {
        }

        public GPU_group(Hashtable table)
        {
            this.uuid = Marshalling.ParseString(table, "uuid");
            this.name_label = Marshalling.ParseString(table, "name_label");
            this.name_description = Marshalling.ParseString(table, "name_description");
            this.PGPUs = Marshalling.ParseSetRef<PGPU>(table, "PGPUs");
            this.VGPUs = Marshalling.ParseSetRef<VGPU>(table, "VGPUs");
            this.GPU_types = Marshalling.ParseStringArray(table, "GPU_types");
            this.other_config = Maps.convert_from_proxy_string_string(Marshalling.ParseHashTable(table, "other_config"));
        }

        public GPU_group(Proxy_GPU_group proxy)
        {
            this.UpdateFromProxy(proxy);
        }

        public GPU_group(string uuid, string name_label, string name_description, List<XenRef<PGPU>> PGPUs, List<XenRef<VGPU>> VGPUs, string[] GPU_types, Dictionary<string, string> other_config)
        {
            this.uuid = uuid;
            this.name_label = name_label;
            this.name_description = name_description;
            this.PGPUs = PGPUs;
            this.VGPUs = VGPUs;
            this.GPU_types = GPU_types;
            this.other_config = other_config;
        }

        public static void add_to_other_config(Session session, string _gpu_group, string _key, string _value)
        {
            session.proxy.gpu_group_add_to_other_config(session.uuid, (_gpu_group != null) ? _gpu_group : "", (_key != null) ? _key : "", (_value != null) ? _value : "").parse();
        }

        public bool DeepEquals(GPU_group other)
        {
            if (object.ReferenceEquals(null, other))
            {
                return false;
            }
            return (object.ReferenceEquals(this, other) || ((((Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<string>(this._name_label, other._name_label)) && (Helper.AreEqual2<string>(this._name_description, other._name_description) && Helper.AreEqual2<List<XenRef<PGPU>>>(this._PGPUs, other._PGPUs))) && (Helper.AreEqual2<List<XenRef<VGPU>>>(this._VGPUs, other._VGPUs) && Helper.AreEqual2<string[]>(this._GPU_types, other._GPU_types))) && Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config)));
        }

        public static List<XenRef<GPU_group>> get_all(Session session)
        {
            return XenRef<GPU_group>.Create(session.proxy.gpu_group_get_all(session.uuid).parse());
        }

        public static Dictionary<XenRef<GPU_group>, GPU_group> get_all_records(Session session)
        {
            return XenRef<GPU_group>.Create<Proxy_GPU_group>(session.proxy.gpu_group_get_all_records(session.uuid).parse());
        }

        public static List<XenRef<GPU_group>> get_by_name_label(Session session, string _label)
        {
            return XenRef<GPU_group>.Create(session.proxy.gpu_group_get_by_name_label(session.uuid, (_label != null) ? _label : "").parse());
        }

        public static XenRef<GPU_group> get_by_uuid(Session session, string _uuid)
        {
            return XenRef<GPU_group>.Create(session.proxy.gpu_group_get_by_uuid(session.uuid, (_uuid != null) ? _uuid : "").parse());
        }

        public static string[] get_GPU_types(Session session, string _gpu_group)
        {
            return session.proxy.gpu_group_get_gpu_types(session.uuid, (_gpu_group != null) ? _gpu_group : "").parse();
        }

        public static string get_name_description(Session session, string _gpu_group)
        {
            return session.proxy.gpu_group_get_name_description(session.uuid, (_gpu_group != null) ? _gpu_group : "").parse();
        }

        public static string get_name_label(Session session, string _gpu_group)
        {
            return session.proxy.gpu_group_get_name_label(session.uuid, (_gpu_group != null) ? _gpu_group : "").parse();
        }

        public static Dictionary<string, string> get_other_config(Session session, string _gpu_group)
        {
            return Maps.convert_from_proxy_string_string(session.proxy.gpu_group_get_other_config(session.uuid, (_gpu_group != null) ? _gpu_group : "").parse());
        }

        public static List<XenRef<PGPU>> get_PGPUs(Session session, string _gpu_group)
        {
            return XenRef<PGPU>.Create(session.proxy.gpu_group_get_pgpus(session.uuid, (_gpu_group != null) ? _gpu_group : "").parse());
        }

        public static GPU_group get_record(Session session, string _gpu_group)
        {
            return new GPU_group(session.proxy.gpu_group_get_record(session.uuid, (_gpu_group != null) ? _gpu_group : "").parse());
        }

        public static string get_uuid(Session session, string _gpu_group)
        {
            return session.proxy.gpu_group_get_uuid(session.uuid, (_gpu_group != null) ? _gpu_group : "").parse();
        }

        public static List<XenRef<VGPU>> get_VGPUs(Session session, string _gpu_group)
        {
            return XenRef<VGPU>.Create(session.proxy.gpu_group_get_vgpus(session.uuid, (_gpu_group != null) ? _gpu_group : "").parse());
        }

        public static void remove_from_other_config(Session session, string _gpu_group, string _key)
        {
            session.proxy.gpu_group_remove_from_other_config(session.uuid, (_gpu_group != null) ? _gpu_group : "", (_key != null) ? _key : "").parse();
        }

        public override string SaveChanges(Session session, string opaqueRef, GPU_group server)
        {
            if (opaqueRef == null)
            {
                return "";
            }
            if (!Helper.AreEqual2<string>(this._name_label, server._name_label))
            {
                set_name_label(session, opaqueRef, this._name_label);
            }
            if (!Helper.AreEqual2<string>(this._name_description, server._name_description))
            {
                set_name_description(session, opaqueRef, this._name_description);
            }
            if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
            {
                set_other_config(session, opaqueRef, this._other_config);
            }
            return null;
        }

        public static void set_name_description(Session session, string _gpu_group, string _description)
        {
            session.proxy.gpu_group_set_name_description(session.uuid, (_gpu_group != null) ? _gpu_group : "", (_description != null) ? _description : "").parse();
        }

        public static void set_name_label(Session session, string _gpu_group, string _label)
        {
            session.proxy.gpu_group_set_name_label(session.uuid, (_gpu_group != null) ? _gpu_group : "", (_label != null) ? _label : "").parse();
        }

        public static void set_other_config(Session session, string _gpu_group, Dictionary<string, string> _other_config)
        {
            session.proxy.gpu_group_set_other_config(session.uuid, (_gpu_group != null) ? _gpu_group : "", Maps.convert_to_proxy_string_string(_other_config)).parse();
        }

        public Proxy_GPU_group ToProxy()
        {
            return new Proxy_GPU_group { uuid = (this.uuid != null) ? this.uuid : "", name_label = (this.name_label != null) ? this.name_label : "", name_description = (this.name_description != null) ? this.name_description : "", PGPUs = (this.PGPUs != null) ? Helper.RefListToStringArray<PGPU>(this.PGPUs) : new string[0], VGPUs = (this.VGPUs != null) ? Helper.RefListToStringArray<VGPU>(this.VGPUs) : new string[0], GPU_types = this.GPU_types, other_config = Maps.convert_to_proxy_string_string(this.other_config) };
        }

        public override void UpdateFrom(GPU_group update)
        {
            this.uuid = update.uuid;
            this.name_label = update.name_label;
            this.name_description = update.name_description;
            this.PGPUs = update.PGPUs;
            this.VGPUs = update.VGPUs;
            this.GPU_types = update.GPU_types;
            this.other_config = update.other_config;
        }

        internal void UpdateFromProxy(Proxy_GPU_group proxy)
        {
            this.uuid = (proxy.uuid == null) ? null : proxy.uuid;
            this.name_label = (proxy.name_label == null) ? null : proxy.name_label;
            this.name_description = (proxy.name_description == null) ? null : proxy.name_description;
            this.PGPUs = (proxy.PGPUs == null) ? null : XenRef<PGPU>.Create(proxy.PGPUs);
            this.VGPUs = (proxy.VGPUs == null) ? null : XenRef<VGPU>.Create(proxy.VGPUs);
            this.GPU_types = (proxy.GPU_types == null) ? new string[0] : proxy.GPU_types;
            this.other_config = (proxy.other_config == null) ? null : Maps.convert_from_proxy_string_string(proxy.other_config);
        }

        public virtual string[] GPU_types
        {
            get
            {
                return this._GPU_types;
            }
            set
            {
                if (!Helper.AreEqual(value, this._GPU_types))
                {
                    this._GPU_types = value;
                    base.Changed = true;
                    base.NotifyPropertyChanged("GPU_types");
                }
            }
        }

        public virtual string name_description
        {
            get
            {
                return this._name_description;
            }
            set
            {
                if (!Helper.AreEqual(value, this._name_description))
                {
                    this._name_description = value;
                    base.Changed = true;
                    base.NotifyPropertyChanged("name_description");
                }
            }
        }

        public virtual string name_label
        {
            get
            {
                return this._name_label;
            }
            set
            {
                if (!Helper.AreEqual(value, this._name_label))
                {
                    this._name_label = value;
                    base.Changed = true;
                    base.NotifyPropertyChanged("name_label");
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

        public virtual List<XenRef<PGPU>> PGPUs
        {
            get
            {
                return this._PGPUs;
            }
            set
            {
                if (!Helper.AreEqual(value, this._PGPUs))
                {
                    this._PGPUs = value;
                    base.Changed = true;
                    base.NotifyPropertyChanged("PGPUs");
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

        public virtual List<XenRef<VGPU>> VGPUs
        {
            get
            {
                return this._VGPUs;
            }
            set
            {
                if (!Helper.AreEqual(value, this._VGPUs))
                {
                    this._VGPUs = value;
                    base.Changed = true;
                    base.NotifyPropertyChanged("VGPUs");
                }
            }
        }
    }
}

