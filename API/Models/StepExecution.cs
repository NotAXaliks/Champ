using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class StepExecution
{
    public int Id { get; set; }

    public int ProductionBatchId { get; set; }

    public int TechStepId { get; set; }

    public int StartedById { get; set; }

    public DateTime? StartDate { get; set; }

    public int? EndOperatorId { get; set; }

    public DateTime? EndDate { get; set; }

    public string Param { get; set; } = null!;

    public string? Value { get; set; }

    public string? Status { get; set; }

    public string? Comment { get; set; }

    [ValidateNever, BindNever]
    public virtual ICollection<Deviation> Deviations { get; set; } = new List<Deviation>();

    [ValidateNever, BindNever]
    public virtual User? EndOperator { get; set; }

    [ValidateNever, BindNever]
    public virtual ICollection<Incident> Incidents { get; set; } = new List<Incident>();

    [ValidateNever, BindNever]
    public virtual ProductionBatch ProductionBatch { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual User StartedBy { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual TechStep TechStep { get; set; } = null!;
}
