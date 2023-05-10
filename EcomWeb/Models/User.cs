using System;
using System.Collections.Generic;

namespace EcomWeb.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Active { get; set; }

    public string FullName { get; set; } = null!;

    public DateTime? LastLogin { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;
}
