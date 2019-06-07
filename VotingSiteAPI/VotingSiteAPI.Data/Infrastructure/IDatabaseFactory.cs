
using System;

using VotingSiteAPI.Data;


namespace VotingSiteAPI.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        VotingSiteAPIDbCtx Get();
    }
}
