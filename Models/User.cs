using System;
using System.Collections.Generic;

namespace FoodOrderSystem.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<OrderDetail> OrderDetailOrderKuryes { get; set; } = new List<OrderDetail>();

    public virtual ICollection<OrderDetail> OrderDetailUsers { get; set; } = new List<OrderDetail>();

    public virtual RolesDetail Role { get; set; } = null!;
}
