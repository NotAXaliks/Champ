using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class Recipe
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int StatusId { get; set; }

    public int Version { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public int? ApprovedById { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedById { get; set; }

    public string Comment { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual User? ApprovedBy { get; set; }

    [ValidateNever, BindNever]
    public virtual User CreatedBy { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual Product Product { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<ProductBatch> ProductBatches { get; set; } = new List<ProductBatch>();

    [ValidateNever, BindNever]
    public virtual ICollection<RecipeComponent> RecipeComponents { get; set; } = new List<RecipeComponent>();

    [ValidateNever, BindNever]
    public virtual Status Status { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<TechCard> TechCards { get; set; } = new List<TechCard>();
}
