using MillionandUpBackend01.Exceptions;

namespace MillionandUpBackend01.Lib.Validation.Validators {
    public class CustomAsyncValidator<T> : AbstractValidator<T> {
        readonly T _toValidate;
        readonly Func<T, object>? _func = null;
        readonly string? _key = null;
        readonly Func<T, Task<bool>> _validation;

        public override string? Message { get; set; }

        public CustomAsyncValidator(Func<T, object> func, T toValidate, Func<T, Task<bool>> validation, string? message = "Custom validation error") {
            _toValidate = toValidate;
            _func = func;
            _validation = validation;

            if (message != null) Message = message;
        }

        public CustomAsyncValidator(string key, T toValidate, Func<T, Task<bool>> validation, string? message = "Custom validation error") {
            _toValidate = toValidate;
            _key = key;
            _validation = validation;

            if (message != null) Message = message;
        }

        public override bool Validate() {
            throw new AsyncValidatorException("There are async validators.");
        }

        public override async Task<bool> ValidateAsync() {
            return await _validation(_toValidate);
        }
    }
}
