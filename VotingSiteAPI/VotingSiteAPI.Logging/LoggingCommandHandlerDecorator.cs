using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


// https://blogs.cuttingedge.it/steven/posts/2011/meanwhile-on-the-command-side-of-my-architecture/

namespace VotingSiteAPI.Logging
{

    public class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _decoratee;
        //private readonly ILogger logger;

        public LoggingCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratee /*, 
            ILogger logger */)
        {
            _decoratee = decoratee;
            //this.logger = logger;
        }

        public void Handle(TCommand command)
        {
            // serialize command to json and log
            //this.logger.Log(serializedcommandData);

            _decoratee.Handle(command);
        }
    }


    // public class MoveCustomerCommand -- A 'Command' in this context is just a DTO who's info is used to do the job.
    // {
    //  public int CustomerId { get; set; }
    //  public Address NewAddress { get; set; }
    // }

    //public class TransactionCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    //{
    //    private readonly ICommandHandler<TCommand> _decorated;
    //
    //    public TransactionCommandHandlerDecorator(
    //        ICommandHandler<TCommand> decorated)
    //    {
    //        _decorated = decorated;
    //    }
    //
    //    public void Handle(TCommand command)
    //    {
    //        using (var scope = new TransactionScope())
    //        {
    //            _decorated.Handle(command);
    //            scope.Complete();
    //        }
    //    }
    //}
}

