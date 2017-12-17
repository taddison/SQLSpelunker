using System;
using System.Collections.Generic;
using System.Text;

namespace SQLSpelunker.Core
{
    public class ProcedureCall
    {
        public ProcedureCall(List<StoredProcedure> parents, StoredProcedure storedProcedure)
        {
            Children = new List<ProcedureCall>();
            AllParents = parents ?? new List<StoredProcedure>();
            StoredProcedure = storedProcedure;
        }

        public StoredProcedure StoredProcedure { get; private set;}
        public IList<ProcedureCall> Children { get; private set; }
        public IList<StoredProcedure> AllParents { get; private set; }

        public bool IsInfiniteLoop { get; set; }
        public bool IsRoot => StoredProcedure == null;
    }
}
