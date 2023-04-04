using Core.Service.Commands;
using MediatR;

namespace Core.Service.Queries
{
    public abstract class Query<T> : CommandBase, IRequest<T>
    {
    }
}
