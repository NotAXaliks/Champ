using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class EquipmentTelemetry
{
    public int Id { get; set; }

    public int EquipmentId { get; set; }

    public int? BatchId { get; set; }

    public string Parameter { get; set; } = null!;

    public int Value { get; set; }

    public DateTime CreatedAt { get; set; }

    [ValidateNever, BindNever]
    public virtual ProductBatch? Batch { get; set; }

    [ValidateNever, BindNever]
    public virtual Equipment Equipment { get; set; } = null!;
}
