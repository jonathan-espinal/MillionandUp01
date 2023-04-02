using MillionandUpBackend01.Lib.Validation.Tools;
using System.Linq.Expressions;


namespace MillionandUpBackend01.Lib.Validation {

    public class ValidatorServiceManager {
        public ErrorResponse _data { get; set; } = new ErrorResponse();
        public int Count { get => _data.Count; }
        public Dictionary<string, List<object>> Errors { get => _data.Data; }
        public ValidatorServiceManager AddError(object value) { _data.Add("_global", value); return this; }
        public ValidatorServiceManager AddError(object[] value) { _data.Add("_global", value); return this; }
        public ValidatorServiceManager AddError(List<object> value) { _data.Add("_global", value); return this; }
        public ValidatorServiceManager AddError(string key, object value) { _data.Add(key, value); return this; }
        public ValidatorServiceManager AddError(string key, object[] value) { _data.Add(key, value); return this; }
        public ValidatorServiceManager AddError(string key, List<object> value) { _data.Add(key, value); return this; }

        public Dictionary<string, object> Store { get; set; } = new Dictionary<string, object>();
    }

    public class ValidatorService : ValidatorServiceManager {
    }

    public class ValidatorService<T> : ValidatorServiceManager where T : class {
        #pragma warning disable CS8618
        protected T _toValidate;
        #pragma warning restore CS8618

        List<ValidatorChainList<T>> _validatorChains = new List<ValidatorChainList<T>>();

        public void AddObject(T toValidate) {
            _toValidate = toValidate;
        }

        public ValidatorChain<T> Add(Expression<Func<T, object>> expr) {
            #pragma warning disable CS8600
            MemberExpression expression = expr.Body as MemberExpression ?? ((UnaryExpression)expr.Body).Operand as MemberExpression;
            #pragma warning restore CS8600

            #pragma warning disable CS8602
            string propertyName = expression.Member.Name;
            #pragma warning restore CS8602
            ValidatorChain<T> validatorChain = new ValidatorChain<T>(expr.Compile(), _toValidate);
            _validatorChains.Add(new ValidatorChainList<T>() {
                // Name = propertyName.ToLowerTitle(),
                Name = propertyName,
                Value = validatorChain,
            });
            return validatorChain;
        }

        public ValidatorChain<T> Add(string key) {
            ValidatorChain<T> validatorChain = new ValidatorChain<T>(key, _toValidate);
            _validatorChains.Add(new ValidatorChainList<T>() {
                Name = key,
                Value = validatorChain,
            });
            return validatorChain;
        }

        public bool Validate() {
            foreach (ValidatorChainList<T> validatorChain in _validatorChains) {
                if (!validatorChain.Value.Validate()) {
                    AddError(validatorChain.Name, validatorChain.Value.Errors);
                }
            }
            return (Count == 0);
        }

        public async Task<bool> ValidateAsync() {
            foreach (ValidatorChainList<T> validatorChain in _validatorChains) {
                if (!await validatorChain.Value.ValidateAsync()) {
                    AddError(validatorChain.Name, validatorChain.Value.Errors);
                }
            }
            return (Count == 0);
        }
    }
}
