using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Nerosoft.Insights.Framework;

namespace Nerosoft.Insights.Storage.Domain;

public class LogCommandHandler : IRequestHandler<LogCreateCommand>
{
    private readonly IServiceProvider _provider;

    public LogCommandHandler(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task Handle(LogCreateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var logType = request.Content.GetType();

            var repositoryType = typeof(IRepository<,>).MakeGenericType(logType, typeof(Guid));
            var service = _provider.GetRequiredService(repositoryType);
            var method = service.GetType().GetMethod("InsertAsync", new[] { logType, typeof(CancellationToken) });
            if (method == null)
            {
                throw new MissingMethodException(repositoryType.Name, "InsertAsync");
            }

            await ((Task)method.Invoke(service, new[] { request.Content, cancellationToken }))!;
        }
        catch (System.Exception exception)
        {
            Debug.WriteLine(exception);
        }
    }
}