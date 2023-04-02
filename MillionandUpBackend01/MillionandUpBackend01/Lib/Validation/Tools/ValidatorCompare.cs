namespace MillionandUpBackend01.Lib.Validation.Tools {
    public class ValidatorCompare {
        public enum OpEnum {
            Eq,
            Gt,
            Lt,
            Ge,
            Le
        }

        public static bool Compare(object a, object b, OpEnum op) {
            if(op == OpEnum.Eq) {
                if (b.GetType() == typeof(string)) return (string)a == (string)b;
                if (b.GetType() == typeof(int)) return (int)a == (int)b;
                if (b.GetType() == typeof(float)) return (float)a == (float)b;
                if (b.GetType() == typeof(decimal)) return (decimal)a == (decimal)b;
                if (b.GetType() == typeof(bool)) return (bool)a == (bool)b;
                if (b.GetType() == typeof(DateTime)) return (DateTime)a == (DateTime)b;
            }
            if (op == OpEnum.Gt) {
                if (b.GetType() == typeof(int)) return (int)a > (int)b;
                if (b.GetType() == typeof(float)) return (float)a > (float)b;
                if (b.GetType() == typeof(decimal)) return (decimal)a > (decimal)b;
                if (b.GetType() == typeof(DateTime)) return (DateTime)a > (DateTime)b;
            }
            if (op == OpEnum.Lt) {
                if (b.GetType() == typeof(int)) return (int)a < (int)b;
                if (b.GetType() == typeof(float)) return (float)a < (float)b;
                if (b.GetType() == typeof(decimal)) return (decimal)a < (decimal)b;
                if (b.GetType() == typeof(DateTime)) return (DateTime)a < (DateTime)b;
            }
            if (op == OpEnum.Ge) {
                if (b.GetType() == typeof(int)) return (int)a >= (int)b;
                if (b.GetType() == typeof(float)) return (float)a >= (float)b;
                if (b.GetType() == typeof(decimal)) return (decimal)a >= (decimal)b;
                if (b.GetType() == typeof(DateTime)) return (DateTime)a >= (DateTime)b;
            }
            if (op == OpEnum.Le) {
                if (b.GetType() == typeof(int)) return (int)a <= (int)b;
                if (b.GetType() == typeof(float)) return (float)a <= (float)b;
                if (b.GetType() == typeof(decimal)) return (decimal)a <= (decimal)b;
                if (b.GetType() == typeof(DateTime)) return (DateTime)a <= (DateTime)b;
            }
            return false;
        }
    }
}
