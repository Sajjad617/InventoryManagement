using System;
using System.Collections.Generic;

namespace InventoryManagement.Model.Models;

public partial class categoriesVM
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }
}
