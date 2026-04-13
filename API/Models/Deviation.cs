using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class Deviation
{
    public int Id { get; set; }

    public int? BatchStepParamId { get; set; }

    public int? BatchId { get; set; }

    public int? ActualValue { get; set; }

    public string? Severity { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    [ValidateNever, BindNever]
    public virtual ProductBatch? Batch { get; set; }

    [ValidateNever, BindNever]
    public virtual BatchStepParam? BatchStepParam { get; set; }

    [ValidateNever, BindNever]
    public virtual User CreatedByNavigation { get; set; } = null!;
}
