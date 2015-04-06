﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace IPTables.Net.Iptables.NativeLibrary
{
    /* TODO: for the future */
    class IptcInterface
    {
        private IntPtr _handle;
        public const String Library = "libiptc.so";
        public const String Helper = "libipthelper.so";
        public const int StringLabelLength = 32;

        public const String IPTC_LABEL_ACCEPT = "ACCEPT";
        public const String IPTC_LABEL_DROP = "DROP";
        public const String IPTC_LABEL_QUEUE = "QUEUE";
        public const String IPTC_LABEL_RETURN = "RETURN";

        /* Does this chain exist? */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_is_chain(String chain, IntPtr handle);

        /* Take a snapshot of the rules.  Returns NULL on error. */
        [DllImport(Library, SetLastError = true)]
        static extern IntPtr iptc_init(String tablename);

        /* Cleanup after iptc_init(). */
        [DllImport(Library, SetLastError = true)]
        static extern void iptc_free(IntPtr h);

        /* Iterator functions to run through the chains.  Returns NULL at end. */
        [DllImport(Library, SetLastError = true)]
        static extern String iptc_first_chain(IntPtr handle);
        [DllImport(Library, SetLastError = true)]
        static extern String iptc_next_chain(IntPtr handle);

        /* Get first rule in the given chain: NULL for empty chain. */
        [DllImport(Library, SetLastError = true)]
        static extern IntPtr iptc_first_rule(String chain,
                            IntPtr handle);

        /* Returns NULL when rules run out. */
        [DllImport(Library, SetLastError = true)]
        static extern IntPtr iptc_next_rule(IntPtr prev,
                               IntPtr handle);

        /* Returns a pointer to the target name of this entry. */
        [DllImport(Library, SetLastError = true)]
        static extern String iptc_get_target(IntPtr e,
                        IntPtr handle);

        /* Is this a built-in chain? */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_builtin(String chain, IntPtr handle);

        /* Get the policy of a given built-in chain */
        [DllImport(Library, SetLastError = true)]
        static extern String iptc_get_policy(String chain,
                        IntPtr counter,
                        IntPtr handle);

        /* These functions return TRUE for OK or 0 and set errno.  If errno ==
           0, it means there was a version error (ie. upgrade libiptc). */
        /* Rule numbers start at 1 for the first rule. */

        /* Insert the entry `e' in chain `chain' into position `rulenum'. */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_insert_entry(
            [MarshalAs(UnmanagedType.LPStr, SizeConst = StringLabelLength)] String chain,
                      IntPtr e,
                      uint rulenum,
                      IntPtr handle);

        /* Atomically replace rule `rulenum' in `chain' with `e'. */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_replace_entry([MarshalAs(UnmanagedType.LPStr, SizeConst = StringLabelLength)]
                String chain,
                       IntPtr e,
                       uint rulenum,
                       IntPtr handle);

        /* Append entry `e' to chain `chain'.  Equivalent to insert with
           rulenum = length of chain. */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_append_entry(
            [MarshalAs(UnmanagedType.LPStr, SizeConst = StringLabelLength)]
                String chain,
                      IntPtr e,
                      IntPtr handle);

        /* Delete the first rule in `chain' which matches `e', subject to
           matchmask (array of length == origfw) */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_delete_entry(
            [MarshalAs(UnmanagedType.LPStr, SizeConst = StringLabelLength)]
                String chain,
                      IntPtr origfw,
                      String matchmask,
                      IntPtr handle);

        /* Delete the rule in position `rulenum' in `chain'. */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_delete_num_entry(
            [MarshalAs(UnmanagedType.LPStr, SizeConst = StringLabelLength)]
                String chain,
                      uint rulenum,
                      IntPtr handle);

        /* Check the packet `e' on chain `chain'.  Returns the verdict, or
           NULL and sets errno. */
        [DllImport(Library, SetLastError = true)]
        static extern String iptc_check_packet(
            [MarshalAs(UnmanagedType.LPStr, SizeConst = StringLabelLength)]
                String chain,
                          IntPtr entry,
                          IntPtr handle);

        /* Flushes the entries in the given chain (ie. empties chain). */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_flush_entries(
            [MarshalAs(UnmanagedType.LPStr, SizeConst = StringLabelLength)]
                String chain,
                       IntPtr handle);

        /* Zeroes the counters in a chain. */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_zero_entries(
            [MarshalAs(UnmanagedType.LPStr, SizeConst = StringLabelLength)]
                String chain,
                      IntPtr handle);

        /* Creates a new chain. */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_create_chain(
            [MarshalAs(UnmanagedType.LPStr, SizeConst = StringLabelLength)]
                String chain,
                      IntPtr handle);

        /* Deletes a chain. */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_delete_chain(
            [MarshalAs(UnmanagedType.LPStr, SizeConst = StringLabelLength)]
                String chain,
                      IntPtr handle);

        /* Renames a chain. */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_rename_chain(
            [MarshalAs(UnmanagedType.LPStr, SizeConst = StringLabelLength)]
                String chain,
                      IntPtr handle);

        /* Sets the policy on a built-in chain. */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_set_policy(
            [MarshalAs(UnmanagedType.LPStr, SizeConst = StringLabelLength)]
                String chain,
            [MarshalAs(UnmanagedType.LPStr, SizeConst = StringLabelLength)]
                String chainPolicy,
                    IntPtr counters,
                    IntPtr handle);

        /* Get the number of references to this chain */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_get_references(IntPtr references,
                [MarshalAs(UnmanagedType.LPStr, SizeConst = StringLabelLength)]
                String chain,
                    IntPtr handle);

        /* read packet and byte counters for a specific rule */
        [DllImport(Library, SetLastError = true)]
        static extern IntPtr iptc_read_counter(
            [MarshalAs(UnmanagedType.LPStr, SizeConst = StringLabelLength)]
                String chain,
                               uint rulenum,
                               IntPtr handle);

        /* zero packet and byte counters for a specific rule */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_zero_counter(
            [MarshalAs(UnmanagedType.LPStr, SizeConst = StringLabelLength)]
                String chain,
                      uint rulenum,
                      IntPtr handle);

        /* set packet and byte counters for a specific rule */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_set_counter(
            [MarshalAs(UnmanagedType.LPStr, SizeConst = StringLabelLength)]
                String chain,
                     uint rulenum,
                     IntPtr counters,
                     IntPtr handle);

        /* Makes the actual changes. */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_commit(IntPtr handle);

        /* Get raw socket. */
        [DllImport(Library, SetLastError = true)]
        static extern int iptc_get_raw_socket();

        /* Translates errno numbers into more human-readable form than strerror. */
        [DllImport(Library, SetLastError = true)]
        static extern String iptc_strerror(int err);

        [DllImport(Helper, SetLastError = true)]
        static extern String output_rule4(IntPtr e, IntPtr h, String chain, int counters);

        public IptcInterface(String table)
        {
            _handle = iptc_init(table);
        }

        public List<IntPtr> GetRules(String chain)
        {
            List<IntPtr> ret = new List<IntPtr>();
            var rule = iptc_first_rule(chain, _handle);
            do
            {
                ret.Add(rule);
                rule = iptc_next_rule(rule, _handle);
            } while (rule != IntPtr.Zero);
            return ret;
        }


        public String GetRuleString(String chain, IntPtr rule, bool counters = false)
        {
            return output_rule4(rule, _handle, chain, counters ? 1 : 0);
        }

        public void Insert(String chain, IntPtr entry, uint at)
        {
            iptc_insert_entry(chain, entry, at, _handle);
        }


        ~IptcInterface()
        {
            if (_handle != IntPtr.Zero)
            {
                iptc_free(_handle);
                _handle = IntPtr.Zero;
            }
        }
    }
}