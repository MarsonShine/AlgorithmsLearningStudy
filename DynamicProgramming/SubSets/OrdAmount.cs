namespace DynamicProgramming {
    public class OrdAmount {
        public int OrdID { get; set; }
        public decimal Amount { get; set; }
        public long VirAmount => (long) (Amount * 100);
    }
}