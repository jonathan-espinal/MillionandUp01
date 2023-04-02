namespace MillionandUpBackend01.Lib.Validation.Tools {
    public class ValidatorChainList<T> {
        public string Name { get; set; } = null!;
        public ValidatorChain<T> Value { get; set; } = null!;
    }
}
