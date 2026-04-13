using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class BatchStepParam
{
    public int Id { get; set; }

    public int BatchStepId { get; set; }

    public int TechParamId { get; set; }

    public int Value { get; set; }

    public int UnitId { get; set; }

    public bool IsOutOfRange { get; set; }

    [ValidateNever, BindNever]
    public virtual BatchStep BatchStep { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<Deviation> Deviations { get; set; } = new List<Deviation>();

    [ValidateNever, BindNever]
    public virtual TechStepParam TechParam { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual Unit Unit { get; set; } = null!;
}
