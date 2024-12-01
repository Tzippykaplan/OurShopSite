using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities;

public partial class User
{
    [Required]
    public int Id { get; set; }

    [MaxLength(15, ErrorMessage = "name cannot be longer than 15 characters")]

    public string FullName { get; set; } = null!;

    [EmailAddress, Required]

    public string Email { get; set; } = null!;

    [StringLength(15, ErrorMessage = "password must contain 15 characters"), Required]

    public string Password { get; set; } = null!;

    public string? Phone { get; set; }
}
