﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IPTables.Net.Iptables.Modules.TcpMss
{
    public class TcpMssModule : ModuleBase, IIpTablesModule, IEquatable<TcpMssModule>
    {
        private const String OptionSetMss= "--set-mss";
        private const String OptionClampMssToPmtuLong = "--clamp-mss-to-pmtu";

        public int SetMss = 0;
        public bool ClampMssToPmtu = false;

        public TcpMssModule(int version) : base(version)
        {
        }

        public bool Equals(TcpMssModule other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return SetMss == other.SetMss && ClampMssToPmtu.Equals(other.ClampMssToPmtu);
        }

        public bool NeedsLoading
        {
            get { return false; }
        }

        public int Feed(RuleParser parser, bool not)
        {
            switch (parser.GetCurrentArg())
            {
                case OptionClampMssToPmtuLong:
                    ClampMssToPmtu = true;
                    return 1;
                case OptionSetMss:
                    SetMss = int.Parse(parser.GetNextArg());
                    return 1;
            }

            return 0;
        }

        public String GetRuleString()
        {
            var sb = new StringBuilder();

            if (ClampMssToPmtu)
            {
                if (sb.Length != 0)
                    sb.Append(" ");
                sb.Append(OptionClampMssToPmtuLong);
            }

            if (SetMss != 0)
            {
                if (sb.Length != 0)
                    sb.Append(" ");
                sb.Append(OptionClampMssToPmtuLong);
                sb.Append(" ");
                sb.Append(SetMss);
            }

            return sb.ToString();
        }

        public static HashSet<String> GetOptions()
        {
            var options = new HashSet<string>
            {
                OptionClampMssToPmtuLong,
                OptionSetMss
            };
            return options;
        }

        public static ModuleEntry GetModuleEntry()
        {
            return GetTargetModuleEntryInternal("TCPMSS", typeof (TcpMssModule), GetOptions);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TcpMssModule) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (SetMss*397) ^ ClampMssToPmtu.GetHashCode();
            }
        }
    }
}