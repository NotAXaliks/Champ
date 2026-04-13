using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class BatchMaterial
{
    public int Id { get; set; }

    public int BatchId { get; set; }

    public int MaterialBatchId { get; set; }

    public int Count { get; set; }

    public int UnitId { get; set; }

    [ValidateNever, BindNever]
    public virtual ProductBatch Batch { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual MaterialBatch MaterialBatch { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual Unit Unit { get; set; } = null!;
}
