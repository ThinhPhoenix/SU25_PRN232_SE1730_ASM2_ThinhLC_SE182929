using System.ComponentModel.DataAnnotations;

namespace DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Models.DTOs
{
    public class SampleThinhLcInputDto
    {
        public int? SampleThinhLcid { get; set; }

        [Required(ErrorMessage = "Profile is required")]
        public int? ProfileThinhLcid { get; set; }

        [Required(ErrorMessage = "Sample Type is required")]
        public int? SampleTypeThinhLcid { get; set; }

        public int? AppointmentsTienDmid { get; set; }

        [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }

        public bool IsProcessed { get; set; } = false;

        [Range(1, int.MaxValue, ErrorMessage = "Count must be at least 1")]
        public int Count { get; set; } = 1;

        public DateTime? CollectedAt { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
