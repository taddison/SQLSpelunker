using System;
using System.Collections.Generic;
using System.Text;

namespace SQLSpelunker.Core
{
    public class ProcedureCall
    {
        public ProcedureCall(List<StoredProcedure> parents, StoredProcedure storedProcedure, int depth)
        {
            Children = new List<ProcedureCall>();
            AllParents = parents ?? new List<StoredProcedure>();
            StoredProcedure = storedProcedure;
            Depth = depth;
        }

        public StoredProcedure StoredProcedure { get; private set;}
        public int Depth { get; private set; }
        public IList<ProcedureCall> Children { get; private set; }
        public IList<StoredProcedure> AllParents { get; private set; }

        public bool IsInfiniteLoop { get; set; }
        public bool IsRoot => Depth == 0;

        public string GetCallHierarchy()
        {
            var infinite = IsInfiniteLoop ? "[*]" : "";
            var hierarchy = IsRoot ? string.Empty : $"{StoredProcedure.Database}.{StoredProcedure.Schema}.{StoredProcedure.Name} {infinite}";
            foreach (var child in Children)
            {
                hierarchy += Environment.NewLine;
                for (var i = 0; i < Depth; i++)
                {
                    hierarchy += "-";
                }
                hierarchy += $"{child.GetCallHierarchy()}";
            }
            return hierarchy;
        }
    }
}
