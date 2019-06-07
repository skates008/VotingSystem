

namespace VotingSiteAPI.Logging
{
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }
}
