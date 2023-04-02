using MillionandUpBackend01.Lib.Validation.Validators;

namespace MillionandUpBackend01.Lib.Validation.Conditionals;

public class IfConditional<T> : AbstractConditional<T> {
    T _toValidate;
    Func<T, bool> _condition;

    public IfConditional(T toValidate, Func<T, bool> condition) {
        _toValidate = toValidate;
        _condition = condition;
    }

    public override bool Validate() {
        return _condition(_toValidate);
    }
}
