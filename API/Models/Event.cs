using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class Event
{
    public int Id { get; set; }

    public string EntityType { get; set; } = null!;

    public int EntityId { get; set; }

    public string EventType { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public string? Data { get; set; }

    [ValidateNever, BindNever]
    public virtual User? CreatedByNavigation { get; set; }
}
