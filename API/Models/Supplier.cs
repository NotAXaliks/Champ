using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ContactInfo { get; set; }

    [ValidateNever, BindNever]
    public virtual ICollection<MaterialBatch> MaterialBatches { get; set; } = new List<MaterialBatch>();
}
