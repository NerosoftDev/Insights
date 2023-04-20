using MediatR;

namespace Nerosoft.Insights.Storage.Domain;

public class LogCreateCommand : IRequest
{
    public LogCreateCommand(object content)
    {
        Content = content;
    }

    public object Content { get; }
}
