using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Models.DTOs;

namespace DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Models.Extensions
{
    public static class SampleThinhLcExtensions
    {
        public static SampleThinhLcInputDto ToInputDto(this SampleThinhLcGraphQLResponse response)
        {
            return new SampleThinhLcInputDto
            {
                SampleThinhLcid = response.SampleThinhLcid,
                ProfileThinhLcid = response.ProfileThinhLcid,
                SampleTypeThinhLcid = response.SampleTypeThinhLcid,
                AppointmentsTienDmid = response.AppointmentsTienDmid,
                Notes = response.Notes,
                IsProcessed = response.IsProcessed ?? false,
                Count = response.Count ?? 1,
                CollectedAt = response.CollectedAt,
                CreatedAt = response.CreatedAt,
                UpdatedAt = response.UpdatedAt
            };
        }

        public static SampleThinhLcGraphQLResponse ToGraphQLResponse(this SampleThinhLcInputDto dto)
        {
            return new SampleThinhLcGraphQLResponse
            {
                SampleThinhLcid = dto.SampleThinhLcid,
                ProfileThinhLcid = dto.ProfileThinhLcid,
                SampleTypeThinhLcid = dto.SampleTypeThinhLcid,
                AppointmentsTienDmid = dto.AppointmentsTienDmid,
                Notes = dto.Notes,
                IsProcessed = dto.IsProcessed,
                Count = dto.Count,
                CollectedAt = dto.CollectedAt,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            };
        }
    }
}
