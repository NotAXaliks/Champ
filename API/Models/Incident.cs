using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class Incident
{
    public int Id { get; set; }

    public int? ProductionBatchId { get; set; }

    public int? StepExecutionId { get; set; }

    public int UserId { get; set; }

    public string? Type { get; set; }

    public string? Descrpition { get; set; }

    public string? Status { get; set; }

    public DateTime? Date { get; set; }

    [ValidateNever, BindNever]
    public virtual ProductionBatch? ProductionBatch { get; set; }

    [ValidateNever, BindNever]
    public virtual StepExecution? StepExecution { get; set; }

    [ValidateNever, BindNever]
    public virtual User User { get; set; } = null!;
}
