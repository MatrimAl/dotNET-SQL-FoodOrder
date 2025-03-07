using System;
using System.Collections.Generic;

namespace FoodOrderSystem.Models;

public partial class RolesDetail
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
