using DNATestingSystem.Repository.ThinhLC.Models;

namespace DNATestingSystem.GraphQLAPIServices.ThinhLC.GraphQLs.InputTypes
{
    public class SampleThinhLcInput
    {
        public int? SampleThinhLcid { get; set; }
        public int? ProfileThinhLcid { get; set; }
        public int? SampleTypeThinhLcid { get; set; }
        public int? AppointmentsTienDmid { get; set; }
        public string? Notes { get; set; }
        public bool? IsProcessed { get; set; }
        public int? Count { get; set; }
        public DateTime? CollectedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public SampleThinhLc ToEntity()
        {
            return new SampleThinhLc
            {
                SampleThinhLcid = this.SampleThinhLcid,
                ProfileThinhLcid = this.ProfileThinhLcid,
                SampleTypeThinhLcid = this.SampleTypeThinhLcid,
                AppointmentsTienDmid = this.AppointmentsTienDmid,
                Notes = this.Notes,
                IsProcessed = this.IsProcessed,
                Count = this.Count,
                CollectedAt = this.CollectedAt,
                CreatedAt = this.CreatedAt ?? DateTime.Now,
                UpdatedAt = this.UpdatedAt ?? DateTime.Now,
                DeletedAt = this.DeletedAt
            };
        }
    }
}
