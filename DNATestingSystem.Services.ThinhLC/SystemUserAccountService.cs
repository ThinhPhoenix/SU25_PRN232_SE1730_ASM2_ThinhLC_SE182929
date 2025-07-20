using DNATestingSystem.Repository.ThinhLC;
using DNATestingSystem.Repository.ThinhLC.Models;

namespace DNATestingSystem.Services.ThinhLC
{
    public class SystemUserAccountService
    {
        private readonly SystemUserAccountRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceProviders _serviceProviders;

        public SystemUserAccountService(SystemUserAccountRepository repository, IUnitOfWork unitOfWork, IServiceProviders serviceProviders)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _serviceProviders = serviceProviders;
        }

        public Task<SystemUserAccount> GetSystemUserAccountAsync(string username, string password)
        {
            return _repository.GetSystemUserAccountAsync(username, password);
        }
    }
}
