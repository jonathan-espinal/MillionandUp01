namespace MillionandUpBackend01.Exceptions {
    public class AsyncValidatorException : Exception {
        public AsyncValidatorException(string message, string? code = null) : base(message) { }
    }
}
