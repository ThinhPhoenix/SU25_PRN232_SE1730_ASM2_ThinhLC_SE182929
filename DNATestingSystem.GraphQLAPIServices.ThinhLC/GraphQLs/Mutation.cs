using DNATestingSystem.Repository.ThinhLC.Models;
using DNATestingSystem.Services.ThinhLC;
using DNATestingSystem.GraphQLAPIServices.ThinhLC.GraphQLs.InputTypes;

namespace DNATestingSystem.GraphQLAPIServices.ThinhLC.GraphQLs
{
    public class Mutation
    {
        private readonly IServiceProviders _serviceProvider;
        public Mutation(IServiceProviders serviceProviders) => _serviceProvider = serviceProviders; public async Task<int> CreateSampleThinhLCs(SampleThinhLcInput sampleThinhLC)
        {
            try
            {
                var entity = sampleThinhLC.ToEntity();
                var result = await _serviceProvider.SampleThinhLCService.CreateAsync(entity);
                return (int)result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<int> UpdateSampleThinhLCs(SampleThinhLcInput sampleThinhLC)
        {
            try
            {
                var entity = sampleThinhLC.ToEntity();
                var result = await _serviceProvider.SampleThinhLCService.UpdateAsync(entity);
                return (int)result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<bool> DeleteSampleThinhLCs(int id)
        {
            try
            {
                var result = await _serviceProvider.SampleThinhLCService.RemoveAsync(id);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
