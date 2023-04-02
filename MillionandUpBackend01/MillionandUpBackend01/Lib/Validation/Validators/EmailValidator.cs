using System.Text.RegularExpressions;

namespace MillionandUpBackend01.Lib.Validation.Validators {
    public class EmailValidator<T> : AbstractValidator<T> {
        T _toValidate;
        Func<T, object> _func;

        public override string? Message { get; set; }

        public EmailValidator(Func<T, object> func, T toValidate, string? message = "Invalid email address") {
            _toValidate = toValidate;
            _func = func;

            if (message != null) Message = message;
        }

        public override bool Validate() {
            var match = Regex.Match((string)_func(_toValidate), @"^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", RegexOptions.IgnoreCase);
            return match.Success;
        }
    }
}
