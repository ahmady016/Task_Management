using MediatR;

namespace TaskManagement.Common;

public class IdInput : IRequest<IResult>
{
    public Guid Id { get; set; }
}
