using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.ThinhLC;

namespace DNATestingSystem.Services.ThinhLC
{
    public interface IServiceProviders
    {
        SystemUserAccountService UserAccountService { get; }
        ISampleThinhLCService SampleThinhLCService { get; }
        ISampleTypeThinhLCService SampleTypeThinhLCService { get; }
        IProfileThinhLCService ProfileThinhLCService { get; }
        IAppointmentsTienDMService AppointmentsTienDMService { get; }
    }

    public class ServiceProviders : IServiceProviders
    {
        private readonly IUnitOfWork _unitOfWork;
        private SystemUserAccountService _systemUserAccountService;
        private ISampleThinhLCService _sampleThinhLCService;
        private ISampleTypeThinhLCService _sampleTypeThinhLCService;
        private IProfileThinhLCService _profileThinhLCService;
        private IAppointmentsTienDMService _appointmentsTienDMService;

        public ServiceProviders()
        {
            _unitOfWork = new UnitOfWork();
        }

        public SystemUserAccountService UserAccountService
        {
            get { return _systemUserAccountService ??= new SystemUserAccountService(_unitOfWork.SystemUserAccountRepository, _unitOfWork, this); }
        }

        public ISampleThinhLCService SampleThinhLCService
        {
            get { return _sampleThinhLCService ??= new SampleThinhLCService(_unitOfWork.SampleThinhLCRepository, _unitOfWork, this); }
        }

        public ISampleTypeThinhLCService SampleTypeThinhLCService
        {
            get { return _sampleTypeThinhLCService ??= new SampleTypeThinhLCService(_unitOfWork.SampleTypeThinhLCRepository, _unitOfWork, this); }
        }

        public IProfileThinhLCService ProfileThinhLCService
        {
            get { return _profileThinhLCService ??= new ProfileThinhLCService(_unitOfWork.ProfileThinhLCRepository, _unitOfWork, this); }
        }

        public IAppointmentsTienDMService AppointmentsTienDMService
        {
            get { return _appointmentsTienDMService ??= new AppointmentsTienDMService(_unitOfWork.AppointmentsTienDMRepository, _unitOfWork, this); }
        }
    }
}