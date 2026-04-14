using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class EquipmentParam
{
    public int Id { get; set; }

    public int? EquipmentId { get; set; }

    public string Param { get; set; } = null!;

    public string Value { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual Equipment? Equipment { get; set; }
}
