using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Entity { get; set; } = null!;

    public string Name { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<BatchStep> BatchSteps { get; set; } = new List<BatchStep>();

    [ValidateNever, BindNever]
    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();

    [ValidateNever, BindNever]
    public virtual ICollection<LabTest> LabTests { get; set; } = new List<LabTest>();

    [ValidateNever, BindNever]
    public virtual ICollection<MaterialBatch> MaterialBatches { get; set; } = new List<MaterialBatch>();

    [ValidateNever, BindNever]
    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

    [ValidateNever, BindNever]
    public virtual ICollection<ProductBatch> ProductBatches { get; set; } = new List<ProductBatch>();

    [ValidateNever, BindNever]
    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

    [ValidateNever, BindNever]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    [ValidateNever, BindNever]
    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    [ValidateNever, BindNever]
    public virtual ICollection<StatusHistory> StatusHistoryNewStatuses { get; set; } = new List<StatusHistory>();

    [ValidateNever, BindNever]
    public virtual ICollection<StatusHistory> StatusHistoryOldStatuses { get; set; } = new List<StatusHistory>();

    [ValidateNever, BindNever]
    public virtual ICollection<TechCard> TechCards { get; set; } = new List<TechCard>();

    [ValidateNever, BindNever]
    public virtual ICollection<TechStep> TechSteps { get; set; } = new List<TechStep>();
}
