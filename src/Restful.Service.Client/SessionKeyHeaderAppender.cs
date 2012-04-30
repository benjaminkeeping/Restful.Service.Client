using System.Net;
using Restful.Web.Client.Headers;

namespace Restful.Service.Client
{
    public class SessionKeyHeaderAppender : IHeaderAppender
    {
        readonly ISessionProvider _sessionProvider;

        public SessionKeyHeaderAppender(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        public void AppendTo(WebRequest request)
        {
            request.Headers.Add("session-key", _sessionProvider.GetSessionKey());
        }
    }
}