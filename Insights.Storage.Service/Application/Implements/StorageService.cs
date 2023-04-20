using MediatR;
using Nerosoft.Insights.Storage.Domain;

namespace Nerosoft.Insights.Storage.Application;

public class StorageService : IStorageService
{
    private readonly IMediator _mediator;

    public StorageService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task InsertAsync<T>(T entity, CancellationToken cancellationToken = default) 
        where T : class
    {
        var command = new LogCreateCommand(entity);
        await _mediator.Send(command, cancellationToken);
    }
}