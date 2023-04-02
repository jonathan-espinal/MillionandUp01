namespace MillionandUpBackend01.Lib.Validation.Validators {

    public class RequiredValidator<T> : AbstractValidator<T> {
        T _toValidate;
        Func<T, object> _func;
        bool _acceptEmpty = false;

        public override string? Message { get; set; }

        public RequiredValidator(Func<T, object> func, T toValidate, bool acceptEmpty = false, string? message = "Value is required") {
            _toValidate = toValidate;
            _func = func;
            _acceptEmpty = acceptEmpty;

            if (message != null) Message = message;
        }

        public override bool Validate() {
            string? value = (string)_func(_toValidate);
            if (_acceptEmpty) return (value != null);
            return (value != null && value != string.Empty);
        }
    }
}