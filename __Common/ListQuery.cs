using MediatR;

namespace TaskManagement.Common;

public class ListQuery : IRequest<IResult>
{
    public int? PageSize { get; set; } = null;
    public int? PageNumber { get; set; } = null;
}
