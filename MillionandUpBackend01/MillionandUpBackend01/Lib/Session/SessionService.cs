namespace MillionandUpBackend01.Lib.Session {
    public class SessionService {
        public Guid Id { get; set; }
        public UserSession User { get; set; } = new UserSession();

        public class UserSession {
            public Guid Id { get; set; }
            public bool IsAuthenticated { get; set; } = false;
            public string Username { get; set; } = null!;
        }
    }
}
