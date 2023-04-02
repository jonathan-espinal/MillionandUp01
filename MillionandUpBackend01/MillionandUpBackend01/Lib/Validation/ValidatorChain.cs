using MillionandUpBackend01.Lib.Validation.Conditionals;
using MillionandUpBackend01.Lib.Validation.Tools;
using MillionandUpBackend01.Lib.Validation.Validators;

namespace MillionandUpBackend01.Lib.Validation {

    public class ValidatorChain<T> {
        readonly T _toValidate;
        readonly Func<T, object>? _func = null;
        readonly string? _key = null;

        ValidatorList<AbstractValidator<T>> _validators = new ValidatorList<AbstractValidator<T>>();
        List<object> _errors = new List<object>();

        public ValidatorChain(Func<T, object> func, T toValidate) {
            _toValidate = toValidate;
            _func = func;
        }

        public ValidatorChain(string key, T toValidate) {
            _key = key;
            _toValidate = toValidate;
        }

        public List<object> Errors {
            get => _errors;
        }

        public bool Validate() {
            bool lastIfValidator = true;
            foreach (AbstractValidator<T> validator in _validators) {
                if (validator.GetType() == typeof(IfConditional<T>)) {
                    lastIfValidator = validator.Validate();
                }
                if (!lastIfValidator) return _errors.Count == 0;
                if (!validator.Validate()) {
                    if (validator.Message != null) _errors.Add(validator.Message);
                }
            }
            return _errors.Count == 0;
        }

        public async Task<bool> ValidateAsync() {
            bool lastIfValidator = true;
            foreach (AbstractValidator<T> validator in _validators) {
                if (validator.GetType() == typeof(IfConditional<T>)) {
                    lastIfValidator = validator.Validate();
                }
                if (!lastIfValidator) return _errors.Count == 0;
                if (!await validator.ValidateAsync()) {
                    if (validator.Message != null) _errors.Add(validator.Message);
                }
            }
            return _errors.Count == 0;
        }

        public ValidatorChain<T> Requiered(bool acceptEmpty = false, string? message = null) {
            if (_func == null) return this;
            RequiredValidator<T> validator = new RequiredValidator<T>(_func, _toValidate, acceptEmpty, message);
            _validators.Add(validator);
            return this;
        }

        public ValidatorChain<T> Greater(object value, string? message = null) {
            if (_func == null) return this;
            GreaterValidator<T> validator = new GreaterValidator<T>(_func, _toValidate, value, message);
            _validators.Add(validator);
            return this;
        }

        public ValidatorChain<T> Lower(object value, string? message = null) {
            if (_func == null) return this;
            LowerValidator<T> validator = new LowerValidator<T>(_func, _toValidate, value, message);
            _validators.Add(validator);
            return this;
        }

        public ValidatorChain<T> GreaterEquals(object value, string? message = null) {
            if (_func == null) return this;
            GreaterEqualsValidator<T> validator = new GreaterEqualsValidator<T>(_func, _toValidate, value, message);
            _validators.Add(validator);
            return this;
        }

        public ValidatorChain<T> LowerEquals(object value, string? message = null) {
            if (_func == null) return this;
            LowerEqualsValidator<T> validator = new LowerEqualsValidator<T>(_func, _toValidate, value, message);
            _validators.Add(validator);
            return this;
        }

        public ValidatorChain<T> Custom(Func<T, bool> validate, string? message = null) {
            CustomValidator<T> validator;
            if (_func != null) {
                validator = new CustomValidator<T>(_func, _toValidate, validate, message);
                _validators.Add(validator);
                return this;
            }
            if (_key != null) {
                validator = new CustomValidator<T>(_key, _toValidate, validate, message);
                _validators.Add(validator);
                return this;
            }
            return this;
        }

        public ValidatorChain<T> CustomAsync(Func<T, Task<bool>> validate, string? message = null) {
            CustomAsyncValidator<T> validator;
            if (_func != null) {
                validator = new CustomAsyncValidator<T>(_func, _toValidate, validate, message);
                _validators.Add(validator);
                return this;
            }
            if (_key != null) {
                validator = new CustomAsyncValidator<T>(_key, _toValidate, validate, message);
                _validators.Add(validator);
                return this;
            }
            return this;
        }

        public ValidatorChain<T> MaxLength(int value, string? message = null) {
            if (_func == null) return this;
            MaxLengthValidator<T> validator = new MaxLengthValidator<T>(_func, _toValidate, value, message);
            _validators.Add(validator);
            return this;
        }

        public ValidatorChain<T> MinLength(int value, string? message = null) {
            if (_func == null) return this;
            MinLengthValidator<T> validator = new MinLengthValidator<T>(_func, _toValidate, value, message);
            _validators.Add(validator);
            return this;
        }

        public ValidatorChain<T> If(Func<T, bool> condition) {
            if (_func == null) return this;
            IfConditional<T> validator = new IfConditional<T>(_toValidate, condition);
            _validators.Add(validator);
            return this;
        }

        public ValidatorChain<T> IfNot(Func<T, bool> condition) {
            if (_func == null) return this;
            IfNotConditional<T> validator = new IfNotConditional<T>(_toValidate, condition);
            _validators.Add(validator);
            return this;
        }
    }
}
