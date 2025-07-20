using DNATestingSystem.Repository.ThinhLC;
using DNATestingSystem.Repository.ThinhLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestingSystem.Services.ThinhLC
{
    public class ProfileThinhLCService : IProfileThinhLCService
    {
        private readonly ProfileThinhLCRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceProviders _serviceProviders;

        public ProfileThinhLCService(ProfileThinhLCRepository repository, IUnitOfWork unitOfWork, IServiceProviders serviceProviders)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _serviceProviders = serviceProviders;
        }

        public async Task<List<ProfileThinhLc>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
