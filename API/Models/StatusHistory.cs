using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class StatusHistory
{
    public int Id { get; set; }

    public string EntityType { get; set; } = null!;

    public int EntityId { get; set; }

    public int? OldStatusId { get; set; }

    public int? NewStatusId { get; set; }

    public DateTime ChangedAt { get; set; }

    public int? ChangedBy { get; set; }

    [ValidateNever, BindNever]
    public virtual User? ChangedByNavigation { get; set; }

    [ValidateNever, BindNever]
    public virtual Status? NewStatus { get; set; }

    [ValidateNever, BindNever]
    public virtual Status? OldStatus { get; set; }
}
