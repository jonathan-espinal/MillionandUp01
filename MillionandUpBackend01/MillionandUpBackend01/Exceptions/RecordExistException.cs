namespace MillionandUpBackend01.Exceptions {
    public class RecordExistException : Exception {
        public RecordExistException(string message, string? code = null) : base(message) { }
    }
}
