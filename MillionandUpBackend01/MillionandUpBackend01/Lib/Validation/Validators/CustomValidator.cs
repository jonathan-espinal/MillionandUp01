namespace MillionandUpBackend01.Lib.Validation.Validators {
    public class CustomValidator<T> : AbstractValidator<T> {
        readonly T _toValidate;
        readonly Func<T, object>? _func = null;
        readonly string? _key = null;
        readonly Func<T, bool> _validation;

        public override string? Message { get; set; }

        public CustomValidator(Func<T, object> func, T toValidate, Func<T, bool> validation, string? message = "Custom validation error") {
            _toValidate = toValidate;
            _func = func;
            _validation = validation;

            if (message != null) Message = message;
        }

        public CustomValidator(string key, T toValidate, Func<T, bool> validation, string? message = "Custom validation error") {
            _toValidate = toValidate;
            _key = key;
            _validation = validation;

            if (message != null) Message = message;
        }

        public override bool Validate() {
            return _validation(_toValidate);
        }
    }
}
