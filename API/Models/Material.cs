using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class Material
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int TypeId { get; set; }

    public int StatusId { get; set; }

    [ValidateNever, BindNever]
    public virtual ICollection<MaterialBatch> MaterialBatches { get; set; } = new List<MaterialBatch>();

    [ValidateNever, BindNever]
    public virtual ICollection<RecipeComponent> RecipeComponents { get; set; } = new List<RecipeComponent>();

    [ValidateNever, BindNever]
    public virtual Status Status { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual MaterialType Type { get; set; } = null!;
}
