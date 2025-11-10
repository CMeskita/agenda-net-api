using Microsoft.Extensions.DependencyInjection;

namespace AgendaNet_Application.Core
{
    public class MediatorService
    {
        private readonly IServiceProvider _provider;

        public MediatorService(IServiceProvider provider)
        {
            _provider = provider;
        }
        /// <summary>
        /// Envia qualquer comando ou consulta e executa o manipulador (handler) correspondente.
        /// </summary>
        public async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Localiza automaticamente o handler baseado nos tipos
            using var scope = _provider.CreateScope();
            var handlerType = typeof(IHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = scope.ServiceProvider.GetService(handlerType);

            if (handler == null)
                throw new InvalidOperationException($"Nenhum manipulador encontrado para o tipo {request.GetType().Name}");

            // Invoca o método ExecuteAsync do handler resolvido (invocação dinâmica evita reflection manual)
            dynamic dynHandler = handler;
            TResponse result = await dynHandler.ExecuteAsync((dynamic)request);
            return result;
        }
    }
}
