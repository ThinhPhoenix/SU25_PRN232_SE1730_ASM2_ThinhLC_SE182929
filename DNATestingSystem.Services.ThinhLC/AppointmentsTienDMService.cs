using DNATestingSystem.Repository.ThinhLC;
using DNATestingSystem.Repository.ThinhLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestingSystem.Services.ThinhLC
{
    public class AppointmentsTienDMService : IAppointmentsTienDMService
    {
        private readonly AppointmentsTienDMRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceProviders _serviceProviders;

        public AppointmentsTienDMService(AppointmentsTienDMRepository repository, IUnitOfWork unitOfWork, IServiceProviders serviceProviders)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _serviceProviders = serviceProviders;
        }

        public async Task<List<AppointmentsTienDm>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
