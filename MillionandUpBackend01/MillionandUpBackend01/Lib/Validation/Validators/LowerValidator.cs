using MillionandUpBackend01.Lib.Validation.Tools;

namespace MillionandUpBackend01.Lib.Validation.Validators {
    public class LowerValidator<T> : AbstractValidator<T> {
        T _toValidate;
        Func<T, object> _func;
        object _value;

        public override string? Message { get; set; }

        public LowerValidator(Func<T, object> func, T toValidate, object value, string? message = "Value must to be lower than {value}") {
            _toValidate = toValidate;
            _func = func;
            _value = value;

            if (message != null) Message = message;
            Message = Message.Replace("{value}", _value.ToString());
        }

        public override bool Validate() {
            return ValidatorCompare.Compare(_func(_toValidate), _value, ValidatorCompare.OpEnum.Lt);
        }
    }
}
