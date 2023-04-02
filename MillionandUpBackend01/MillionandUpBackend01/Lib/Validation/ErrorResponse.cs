namespace MillionandUpBackend01.Lib.Validation {
    public class ErrorResponse {
        Dictionary<string, List<object>> _data = new Dictionary<string, List<object>>();

        public int Count { get => _data.Count; }

        public Dictionary<string, List<object>> Data { get => _data; }

        public ErrorResponse Add(string key, object value) {
            if (_data.ContainsKey(key)) _data[key].Add(value);
            _data.Add(key, new List<object> { value });
            return this;
        }

        public ErrorResponse Add(string key, object[] value) {
            if (_data.ContainsKey(key)) _data[key].AddRange(value);
            _data.Add(key, new List<object>(value));
            return this;
        }

        public ErrorResponse Add(string key, List<object> value) {
            if (_data.ContainsKey(key)) _data[key].AddRange(value);
            _data.Add(key, value);
            return this;
        }
    }
}
