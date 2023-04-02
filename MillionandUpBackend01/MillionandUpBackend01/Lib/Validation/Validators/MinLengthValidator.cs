using MillionandUpBackend01.Lib.Validation.Tools;


namespace MillionandUpBackend01.Lib.Validation.Validators {
    public class MinLengthValidator<T> : AbstractValidator<T> {
        T _toValidate;
        Func<T, object> _func;
        int _value;

        public override string? Message { get; set; }

        public MinLengthValidator(Func<T, object> func, T toValidate, int value, string? message = "Value length must to be lower than {value}") {
            _toValidate = toValidate;
            _func = func;
            _value = value;

            if (message != null) Message = message;
            Message = Message.Replace("{value}", _value.ToString());
        }

        public override bool Validate() {
            return ValidatorCompare.Compare(_func(_toValidate).ToString().Length, _value, ValidatorCompare.OpEnum.Ge);
        }
    }
}
