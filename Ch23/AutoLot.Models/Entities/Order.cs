﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoLot.Models.Entities;

[Index("CarId", Name = "IX_Orders_CarId")]
[Index("CustomerId", "CarId", Name = "IX_Orders_CustomerId_CarId", IsUnique = true)]
public partial class Order
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int CarId { get; set; }

    public byte[] TimeStamp { get; set; }

    [ForeignKey("CarId")]
    [InverseProperty("Orders")]
    public virtual Car Car { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    public virtual Customer Customer { get; set; }
}
