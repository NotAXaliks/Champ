using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class TechStep
{
    public int Id { get; set; }

    public int TypeId { get; set; }

    public int CardId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool Required { get; set; }

    public int Status { get; set; }

    public int Order { get; set; }

    [ValidateNever, BindNever]
    public virtual ICollection<BatchStep> BatchSteps { get; set; } = new List<BatchStep>();

    [ValidateNever, BindNever]
    public virtual TechCard Card { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual Status StatusNavigation { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<TechStepParam> TechStepParams { get; set; } = new List<TechStepParam>();

    [ValidateNever, BindNever]
    public virtual TechStepType Type { get; set; } = null!;
}
