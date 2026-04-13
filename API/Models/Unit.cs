using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class Unit
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<BatchMaterial> BatchMaterials { get; set; } = new List<BatchMaterial>();

    [ValidateNever, BindNever]
    public virtual ICollection<BatchStepParam> BatchStepParams { get; set; } = new List<BatchStepParam>();

    [ValidateNever, BindNever]
    public virtual ICollection<LabTestParam> LabTestParams { get; set; } = new List<LabTestParam>();

    [ValidateNever, BindNever]
    public virtual ICollection<MaterialBatch> MaterialBatches { get; set; } = new List<MaterialBatch>();

    [ValidateNever, BindNever]
    public virtual ICollection<TechStepParam> TechStepParams { get; set; } = new List<TechStepParam>();
}
