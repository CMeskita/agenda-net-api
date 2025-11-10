namespace AgendaNet_Application.Core
{
    public interface IHandler<TRequest, TResponse>
    {
        Task<TResponse> ExecuteAsync(TRequest request);
    }
}
