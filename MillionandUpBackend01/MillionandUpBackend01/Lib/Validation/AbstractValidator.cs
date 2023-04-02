namespace MillionandUpBackend01.Lib.Validation {
    public abstract class AbstractValidator<T> {
        public virtual string? Message { get; set; } = null;
        public abstract bool Validate();
        public virtual async Task<bool> ValidateAsync() {
            return await Task.Run(() => Validate());
        }
    }
}
