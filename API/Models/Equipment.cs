using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class Equipment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int StatusId { get; set; }

    [ValidateNever, BindNever]
    public virtual ICollection<EquipmentTelemetry> EquipmentTelemetries { get; set; } = new List<EquipmentTelemetry>();

    [ValidateNever, BindNever]
    public virtual Status Status { get; set; } = null!;
}
