using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class TechStepType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<TechStep> TechSteps { get; set; } = new List<TechStep>();
}
