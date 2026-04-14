using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class ProductionBatch
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int RecipeId { get; set; }

    public int TechCardId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Status { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<Deviation> Deviations { get; set; } = new List<Deviation>();

    [ValidateNever, BindNever]
    public virtual ICollection<EquipmentTelemetry> EquipmentTelemetries { get; set; } = new List<EquipmentTelemetry>();

    [ValidateNever, BindNever]
    public virtual ICollection<Incident> Incidents { get; set; } = new List<Incident>();

    [ValidateNever, BindNever]
    public virtual ICollection<LabTest> LabTests { get; set; } = new List<LabTest>();

    [ValidateNever, BindNever]
    public virtual Product Product { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual Recipe Recipe { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<StepExecution> StepExecutions { get; set; } = new List<StepExecution>();

    [ValidateNever, BindNever]
    public virtual TechCard TechCard { get; set; } = null!;
}
