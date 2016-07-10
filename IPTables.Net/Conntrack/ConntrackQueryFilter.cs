﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IPTables.Net.Conntrack
{
    [StructLayout(LayoutKind.Sequential, Pack=4, Size=20)]
    public struct ConntrackQueryFilter
    {
        public Int32 Key;
        public Int32 Max;
        public Int32 CompareLength;
        public IntPtr Compare;
    }
}
