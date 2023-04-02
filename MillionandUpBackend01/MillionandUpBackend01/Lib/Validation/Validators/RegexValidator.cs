using System.Text.RegularExpressions;

namespace MillionandUpBackend01.Lib.Validation.Validators {
    public class RegexValidator<T> : AbstractValidator<T> {
        T _toValidate;
        Func<T, object> _func;
        string _regex;
        public override string? Message { get; set; }

        public RegexValidator(Func<T, object> func, T toValidate, string regex, string? message = "Not valid data") {
            _toValidate = toValidate;
            _func = func;
            _regex = regex;

            if (message != null) Message = message;
        }

        public override bool Validate() {
            var match = Regex.Match((string)_func(_toValidate), _regex, RegexOptions.IgnoreCase);
            return match.Success;
        }
    }
}
