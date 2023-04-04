

using FluentValidation.Results;

namespace Core.Service.Commands
{
    public abstract class CommandBase
    {
        public DateTime TimeStamp { get; private set; }
        public string MessageType { get; private set; }
        public ValidationResult ValidationResult { get; set; }
        public abstract bool IsValid();
        protected CommandBase(string messageType = null)
        {
            TimeStamp = DateTime.UtcNow;
            MessageType = messageType ?? GetType().Name;
        }
    }
}
