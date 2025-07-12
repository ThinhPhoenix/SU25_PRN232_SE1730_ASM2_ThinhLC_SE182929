using System;
using System.Collections.Generic;

namespace DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Models;

public partial class SampleThinhLc
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

    public virtual AppointmentsTienDm AppointmentsTienDm { get; set; } = null!;

    public virtual ProfileThinhLc ProfileThinhLc { get; set; } = null!;

    public virtual SampleTypeThinhLc SampleTypeThinhLc { get; set; } = null!;
}

public partial class SampleThinhLcGraphQLResponse
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

    public virtual AppointmentsTienDm? AppointmentsTienDm { get; set; }
    public virtual ProfileThinhLc? ProfileThinhLc { get; set; }
    public virtual SampleTypeThinhLc? SampleTypeThinhLc { get; set; }

    // For root query response
    public List<SampleThinhLcGraphQLResponse>? sampleThinhLCs { get; set; }
}