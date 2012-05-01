using System;
using System.Web;

namespace Restful.Service.Client
{
    public class CookieBasedSessionProvider : ISessionProvider
    {
        public string GetSessionKey()
        {
            var cookie = HttpContext.Current.Request.Cookies["session-key"];
            return cookie != null ? cookie.Value : "";
        }

        public string GetLoggedInUsername()
        {
            var cookie = HttpContext.Current.Request.Cookies["user-name"];
            return cookie != null ? cookie.Value : "";
        }

        public long GetLoggedInUserId()
        {
            if (!IsLoggedIn()) return -1;
            var cookie = HttpContext.Current.Request.Cookies["user-id"];
            return cookie != null ? long.Parse(cookie.Value) : -1;            
        }

        public bool IsLoggedIn()
        {
            return !string.IsNullOrWhiteSpace(GetSessionKey());
        }

        public void InitialiseSessionWith(dynamic sessionDetails)
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("session-key", sessionDetails.SessionKey));
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("user-id", sessionDetails.UserId));
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("user-name", sessionDetails.UserName));
        }

        public void ClearSession()
        {
            Expire(HttpContext.Current.Request.Cookies["user-id"]);
            Expire(HttpContext.Current.Request.Cookies["session-key"]);
            Expire(HttpContext.Current.Request.Cookies["user-name"]);
        }

        void Expire(HttpCookie c)
        {
            if (c == null) return;
            c.Expires = DateTime.Now.AddDays(-1D);
            HttpContext.Current.Response.Cookies.Add(c);
        }
    }
}