﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using IPTables.Net.Iptables.NativeLibrary;
using NUnit.Framework;

namespace IPTables.Net.Tests
{
    [TestFixture]
    class IptablesLibraryTest
    {
        public static bool IsLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }

        [TestFixtureSetUp]
        public void TestStartup()
        {
            if (IsLinux)
            {
                Process.Start("/sbin/iptables", "-A INPUT -j ACCEPT");
            }
        }

        [TestFixtureTearDown]
        public void TestDestroy()
        {
            if (IsLinux)
            {
                Process.Start("/sbin/iptables", "-D INPUT -j ACCEPT");
            }
        }

        [Test]
        public void TestRuleOutput()
        {
            if (IsLinux)
            {
                IptcInterface iptc = new IptcInterface("filter");
                var rules = iptc.GetRules("INPUT");
                Assert.AreEqual(1,rules.Count);
                Assert.AreEqual("-A INPUT -j ACCEPT", rules[0]);
            }   
        }
    }
}