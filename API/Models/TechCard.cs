using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class TechCard
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public int StatusId { get; set; }

    public int Version { get; set; }

    public string Description { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<ProductBatch> ProductBatches { get; set; } = new List<ProductBatch>();

    [ValidateNever, BindNever]
    public virtual Recipe Recipe { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual Status Status { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<TechStep> TechSteps { get; set; } = new List<TechStep>();
}
