using MediatR;

namespace Hospital.Integration.Application.CommandsHandlers;

public class RequestCommand : IRequest<string>
{
    public string? ModelBase64 { get; init; }
}
