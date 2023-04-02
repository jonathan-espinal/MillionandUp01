namespace MillionandUpBackend01.Lib.Session {
    public class SessionMiddleware : IMiddleware {

        private readonly SessionService _session;

        public SessionMiddleware(SessionService session) {
            _session = session;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
            try {
                _session.Id = Guid.NewGuid();
                _session.User = new SessionService.UserSession();
                string id = context.User.Claims.First((claim) => claim.Type == "Id").Value;
                _session.User.Id = Guid.Parse(id);
                _session.User.Username = context.User.Claims.First((claim) => claim.Type == "Username").Value;
                _session.User.IsAuthenticated = true;
            } catch (Exception) { }

            await next(context);
        }
    }
}
