﻿using System.ComponentModel.DataAnnotations;

namespace Tutorial8.Contracts.Request;

public class AddProductWarehouseRequestDto
{
    [Required]
    public int IdProduct { get; set; }
    
    [Required]
    public int IdWarehouse { get; set; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Provided amount must be greater than 0")]
    public int Amount { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
}