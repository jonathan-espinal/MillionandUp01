using MillionandUpBackend01.Lib.Validation.Tools;

namespace MillionandUpBackend01.Lib.Validation.Validators {
    public class RangeValidator<T> : AbstractValidator<T> {
        T _toValidate;
        Func<T, object> _func;
        int _valueMin;
        int _valueMax;
        bool _include = false;

        public override string? Message { get; set; }

        public RangeValidator(Func<T, object> func, T toValidate, int valueMin, int valueMax, bool incluide = false, string? message = "Value length must to be between {value}") {
            _toValidate = toValidate;
            _func = func;
            _valueMin = valueMin;
            _valueMax = valueMax;
            _include = incluide;

            if (message != null) Message = message;
            Message = Message.Replace("{valueMin}", _valueMin.ToString());
            Message = Message.Replace("{valueMax}", _valueMax.ToString());
        }

        public override bool Validate() {
            bool a = ValidatorCompare.Compare(_func(_toValidate), _valueMin, ValidatorCompare.OpEnum.Ge);
            bool b = false;
            if (_include) {
                b = ValidatorCompare.Compare(_func(_toValidate), _valueMax, ValidatorCompare.OpEnum.Le);
            } else {
                b = ValidatorCompare.Compare(_func(_toValidate), _valueMax, ValidatorCompare.OpEnum.Le);
            }
            return a && b;
        }
    }
}
