using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class RecipeComponent
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public decimal Percentage { get; set; }

    public int Order { get; set; }

    public int MaterialId { get; set; }

    [ValidateNever, BindNever]
    public virtual Material Material { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual Recipe Recipe { get; set; } = null!;
}
