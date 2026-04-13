using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class LabTest
{
    public int Id { get; set; }

    public int? MaterialBatchId { get; set; }

    public int? ProductBatchId { get; set; }

    public string Type { get; set; } = null!;

    public int Priority { get; set; }

    public int StatusId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? Comment { get; set; }

    [ValidateNever, BindNever]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<LabDecision> LabDecisions { get; set; } = new List<LabDecision>();

    [ValidateNever, BindNever]
    public virtual ICollection<LabTestParam> LabTestParams { get; set; } = new List<LabTestParam>();

    [ValidateNever, BindNever]
    public virtual MaterialBatch? MaterialBatch { get; set; }

    [ValidateNever, BindNever]
    public virtual ProductBatch? ProductBatch { get; set; }

    [ValidateNever, BindNever]
    public virtual Status Status { get; set; } = null!;
}
