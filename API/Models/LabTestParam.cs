using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class LabTestParam
{
    public int Id { get; set; }

    public int TestId { get; set; }

    public int ParamId { get; set; }

    public int? MinValue { get; set; }

    public int? MaxValue { get; set; }

    public int? ActualValue { get; set; }

    public int UnitId { get; set; }

    public bool? IsOk { get; set; }

    [ValidateNever, BindNever]
    public virtual LabParam Param { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual LabTest Test { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual Unit Unit { get; set; } = null!;
}
