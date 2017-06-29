namespace Scanless
{
    public interface IRequest
    {
        void SetResponse(string result);
        RequestInfo GetRequestInfo();
    }
}