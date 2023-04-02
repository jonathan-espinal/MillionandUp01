using MillionandUpBackend01.Lib.Validation.Tools;

namespace MillionandUpBackend01.Lib.Validation.Validators {
    public class GreaterEqualsValidator<T> : AbstractValidator<T> {
        T _toValidate;
        Func<T, object> _func;
        object _value;

        public override string? Message { get; set; }

        public GreaterEqualsValidator(Func<T, object> func, T toValidate, object value, string? message = "Value must to be greater or equals than {value}") {
            _toValidate = toValidate;
            _func = func;
            _value = value;

            if (message != null) Message = message;
            Message = Message.Replace("{value}", _value.ToString());
        }

        public override bool Validate() {
            return ValidatorCompare.Compare(_func(_toValidate), _value, ValidatorCompare.OpEnum.Ge);
        }
    }
}
