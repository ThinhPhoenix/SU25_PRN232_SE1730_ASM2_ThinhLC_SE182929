using DNATestingSystem.Repository.ThinhLC.Models;
using DNATestingSystem.Services.ThinhLC;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DNATestingSystem.GraphQLAPIServices.ThinhLC.GraphQLs
{
    public class Query
    {
        private readonly IServiceProviders _serviceProvider;
        public Query(IServiceProviders serviceProviders) => _serviceProvider = serviceProviders;
        public async Task<IEnumerable<SampleThinhLc>> GetSampleThinhLCs()
        {
            try
            {
                var result = await _serviceProvider.SampleThinhLCService.GetAllAsync();
                return result ?? new List<SampleThinhLc>();
            }
            catch (Exception ex)
            {
                throw new GraphQLException($"Error retrieving samples: {ex.Message}");
            }
        }
        public async Task<SampleThinhLc> GetSampleThinhLCById(int id)
        {
            try
            {
                var result = await _serviceProvider.SampleThinhLCService.GetByIdAsync(id);
                return result ?? new SampleThinhLc();
            }
            catch (Exception ex)
            {
                throw new GraphQLException($"Error retrieving samples: {ex.Message}");
            }
        }

        // Query for dropdown lists
        public async Task<IEnumerable<ProfileThinhLc>> GetProfileThinhLCs()
        {
            try
            {
                var result = await _serviceProvider.ProfileThinhLCService.GetAllAsync();
                return result ?? new List<ProfileThinhLc>();
            }
            catch (Exception ex)
            {
                throw new GraphQLException($"Error retrieving profiles: {ex.Message}");
            }
        }

        public async Task<IEnumerable<SampleTypeThinhLc>> GetSampleTypeThinhLCs()
        {
            try
            {
                var result = await _serviceProvider.SampleTypeThinhLCService.GetAllAsync();
                return result ?? new List<SampleTypeThinhLc>();
            }
            catch (Exception ex)
            {
                throw new GraphQLException($"Error retrieving sample types: {ex.Message}");
            }
        }

        public async Task<SampleTypeThinhLc?> GetSampleTypeThinhLCById(int id)
        {
            try
            {
                var result = await _serviceProvider.SampleTypeThinhLCService.GetByIdAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new GraphQLException($"Error retrieving sample type: {ex.Message}");
            }
        }

        public async Task<IEnumerable<AppointmentsTienDm>> GetAppointmentsTienDMs()
        {
            try
            {
                var result = await _serviceProvider.AppointmentsTienDMService.GetAllAsync();
                return result ?? new List<AppointmentsTienDm>();
            }
            catch (Exception ex)
            {
                throw new GraphQLException($"Error retrieving appointments: {ex.Message}");
            }
        }
    }
}
