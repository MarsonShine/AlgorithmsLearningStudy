using System;

namespace DynamicProgramming {
    public class OrdAmount {
        public int OrdID { get; set; }
        public decimal Amount { get; set; }
        public long VirAmount => (long) (Amount * 100);
        public int CeilingAmount => (int) Math.Ceiling(Amount);
        public int DecimalsAmount => (int) ((Amount - CeilingAmount) * 100);
    }
}