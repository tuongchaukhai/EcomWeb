﻿using System;
using System.Collections.Generic;

namespace EcomWeb.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FullName { get; set; } = null!;

    public DateTime? Birthday { get; set; }

    public string? Avatar { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int District { get; set; }

    public int Ward { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? LastLogiin { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
