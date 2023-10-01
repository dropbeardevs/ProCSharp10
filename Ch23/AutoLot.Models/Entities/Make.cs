﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoLot.Models.Entities;

public partial class Make
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    public byte[] TimeStamp { get; set; }

    [InverseProperty("Make")]
    public virtual ICollection<Car> Inventories { get; set; } = new List<Car>();
}
