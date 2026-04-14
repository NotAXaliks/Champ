using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int ForRoleId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime Date { get; set; }

    [ValidateNever, BindNever]
    public virtual Role ForRole { get; set; } = null!;
}
