using System;
using System.Collections.Generic;

namespace InventoryManagement.Model.Models;

public partial class ProductVM
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Price { get; set; } = null!;

    public string Stock { get; set; } = null!;

    public int CategoryId { get; set; }

    public string? Image { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
