using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class ProductBatch
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int RecipeId { get; set; }

    public int TechCardId { get; set; }

    public string Code { get; set; } = null!;

    public int Count { get; set; }

    public DateTime Date { get; set; }

    public int StatusId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? EndedAt { get; set; }

    [ValidateNever, BindNever]
    public virtual ICollection<BatchMaterial> BatchMaterials { get; set; } = new List<BatchMaterial>();

    [ValidateNever, BindNever]
    public virtual ICollection<BatchStep> BatchSteps { get; set; } = new List<BatchStep>();

    [ValidateNever, BindNever]
    public virtual ICollection<Deviation> Deviations { get; set; } = new List<Deviation>();

    [ValidateNever, BindNever]
    public virtual ICollection<EquipmentTelemetry> EquipmentTelemetries { get; set; } = new List<EquipmentTelemetry>();

    [ValidateNever, BindNever]
    public virtual ICollection<LabTest> LabTests { get; set; } = new List<LabTest>();

    [ValidateNever, BindNever]
    public virtual ProductOrder Order { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual Recipe Recipe { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual Status Status { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual TechCard TechCard { get; set; } = null!;
}
