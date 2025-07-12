using DNATestingSystem.Repository.ThinhLC.Models;

namespace DNATestingSystem.GraphQLAPIServices.ThinhLC.GraphQLs.InputTypes
{
    public class SampleTypeThinhLcInput
    {
        public int? SampleTypeThinhLcid { get; set; }
        public string? TypeName { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public int? Count { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public SampleTypeThinhLc ToEntity()
        {
            return new SampleTypeThinhLc
            {
                SampleTypeThinhLcid = this.SampleTypeThinhLcid,
                TypeName = this.TypeName,
                Description = this.Description,
                IsActive = this.IsActive,
                Count = this.Count,
                CreatedAt = this.CreatedAt ?? DateTime.Now,
                UpdatedAt = this.UpdatedAt ?? DateTime.Now,
                DeletedAt = this.DeletedAt
            };
        }
    }
}
