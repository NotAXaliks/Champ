using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class LabDecision
{
    public int Id { get; set; }

    public int TestId { get; set; }

    public string Decision { get; set; } = null!;

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    [ValidateNever, BindNever]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual LabTest Test { get; set; } = null!;
}
