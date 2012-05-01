namespace Restful.Service.Client
{
    public interface ISessionProvider
    {
        string GetSessionKey();
        long GetLoggedInUserId();
        bool IsLoggedIn();
        void InitialiseSessionWith(dynamic sessionDetails);
        void ClearSession();
        string GetLoggedInUsername();
    }
}