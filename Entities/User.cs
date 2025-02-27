﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities;

public partial class User
{
    public int UserId { get; set; }
    public string Email { get; set; } = null!;
    [MinLength(1)]
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
    
    public string Password { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
