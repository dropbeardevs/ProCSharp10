using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoLot.Models.Entities;

[Keyless]
public partial class CustomerOrderView
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    [StringLength(50)]
    public string Color { get; set; }

    [Required]
    [StringLength(50)]
    public string PetName { get; set; }

    [Required]
    [StringLength(50)]
    public string Make { get; set; }
}
