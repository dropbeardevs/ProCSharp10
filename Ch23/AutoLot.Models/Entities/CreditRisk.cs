using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoLot.Models.Entities;

[Index("CustomerId", Name = "IX_CreditRisks_CustomerId")]
public partial class CreditRisk
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    public int CustomerId { get; set; }

    public byte[] TimeStamp { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("CreditRisks")]
    public virtual Customer Customer { get; set; }
}
