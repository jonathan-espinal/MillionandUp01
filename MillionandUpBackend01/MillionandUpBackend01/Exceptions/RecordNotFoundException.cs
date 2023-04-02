namespace MillionandUpBackend01.Exceptions {
    public class RecordNotFoundException : Exception {
        public RecordNotFoundException(string message, string? code = null) : base(message) { }
    }
}
