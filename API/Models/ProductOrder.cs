using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class ProductOrder
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Code { get; set; } = null!;

    public int Count { get; set; }

    public DateTime Date { get; set; }

    public int StatusId { get; set; }

    public int CreatedBy { get; set; }

    [ValidateNever, BindNever]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual Product Product { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<ProductBatch> ProductBatches { get; set; } = new List<ProductBatch>();

    [ValidateNever, BindNever]
    public virtual Status Status { get; set; } = null!;
}
