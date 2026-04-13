using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class ProductType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
