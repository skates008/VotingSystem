
using System;

using VotingSiteAPI.Data.Repositories;


namespace VotingSiteAPI.Services
{
    public class VotersServices : IVotersServices
    {
        private readonly IVotersRepository _votersRepository;

        public VotersServices(
            IVotersRepository votersRepository)
        {
            _votersRepository = votersRepository ?? throw new ArgumentNullException(nameof(votersRepository));
        }


    }
}
