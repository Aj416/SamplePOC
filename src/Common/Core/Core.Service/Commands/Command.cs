using MediatR;

namespace Core.Service.Commands
{
    public abstract class Command : CommandBase, IRequest
    {
    }
}
