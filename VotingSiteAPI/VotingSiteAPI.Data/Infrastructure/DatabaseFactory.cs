
namespace VotingSiteAPI.Data.Infrastructure
{
	public class DatabaseFactory : Disposable, IDatabaseFactory
	{
		private VotingSiteAPIDbCtx _dataContext;

		public VotingSiteAPIDbCtx Get()
		{
			return _dataContext ?? (_dataContext = new VotingSiteAPIDbCtx());
		}

		protected override void DisposeCore()
        {
            _dataContext?.Dispose();
        }
	}

}
