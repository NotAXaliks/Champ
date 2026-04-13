using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class MaterialBatch
{
    public int Id { get; set; }

    public int MaterialId { get; set; }

    public string Code { get; set; } = null!;

    public int UnitId { get; set; }

    public int Count { get; set; }

    public int Supplier { get; set; }

    public DateTime Date { get; set; }

    public int StatusId { get; set; }

    [ValidateNever, BindNever]
    public virtual ICollection<BatchMaterial> BatchMaterials { get; set; } = new List<BatchMaterial>();

    [ValidateNever, BindNever]
    public virtual ICollection<LabTest> LabTests { get; set; } = new List<LabTest>();

    [ValidateNever, BindNever]
    public virtual Material Material { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual Status Status { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual Supplier SupplierNavigation { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual Unit Unit { get; set; } = null!;
}
