using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class TechStepParam
{
    public int Id { get; set; }

    public int StepId { get; set; }

    public string Name { get; set; } = null!;

    public int MinValue { get; set; }

    public int MaxValue { get; set; }

    public int UnitId { get; set; }

    [ValidateNever, BindNever]
    public virtual ICollection<BatchStepParam> BatchStepParams { get; set; } = new List<BatchStepParam>();

    [ValidateNever, BindNever]
    public virtual TechStep Step { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual Unit Unit { get; set; } = null!;
}
