using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class BatchStep
{
    public int Id { get; set; }

    public int BatchId { get; set; }

    public int TechStepId { get; set; }

    public int StatusId { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public int? OperatorId { get; set; }

    public string? Comment { get; set; }

    [ValidateNever, BindNever]
    public virtual ProductBatch Batch { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<BatchStepParam> BatchStepParams { get; set; } = new List<BatchStepParam>();

    [ValidateNever, BindNever]
    public virtual User? Operator { get; set; }

    [ValidateNever, BindNever]
    public virtual Status Status { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual TechStep TechStep { get; set; } = null!;
}
