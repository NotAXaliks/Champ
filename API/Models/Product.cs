using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int FormId { get; set; }

    public int TypeId { get; set; }

    public int StatusId { get; set; }

    [ValidateNever, BindNever]
    public virtual ProductForm Form { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

    [ValidateNever, BindNever]
    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    [ValidateNever, BindNever]
    public virtual Status Status { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ProductType Type { get; set; } = null!;
}
