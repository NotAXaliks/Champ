using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int RoleId { get; set; }

    public int DepartmentId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<BatchStep> BatchSteps { get; set; } = new List<BatchStep>();

    [ValidateNever, BindNever]
    public virtual Department Department { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<Deviation> Deviations { get; set; } = new List<Deviation>();

    [ValidateNever, BindNever]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    [ValidateNever, BindNever]
    public virtual ICollection<LabDecision> LabDecisions { get; set; } = new List<LabDecision>();

    [ValidateNever, BindNever]
    public virtual ICollection<LabTest> LabTests { get; set; } = new List<LabTest>();

    [ValidateNever, BindNever]
    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

    [ValidateNever, BindNever]
    public virtual ICollection<Recipe> RecipeApprovedByNavigations { get; set; } = new List<Recipe>();

    [ValidateNever, BindNever]
    public virtual ICollection<Recipe> RecipeCreatedByNavigations { get; set; } = new List<Recipe>();

    [ValidateNever, BindNever]
    public virtual Role Role { get; set; } = null!;

    [ValidateNever, BindNever]
    public virtual ICollection<StatusHistory> StatusHistories { get; set; } = new List<StatusHistory>();
}
