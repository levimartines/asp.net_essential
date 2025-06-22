using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyWebApplication.Models;

public class Product
{
    public int Id { get; set; }

    [Required] [StringLength(50)]
    public string? Name { get; set; }

    [Required] [StringLength(50)]
    public string? Description { get; set; }

    [Required] [Column(TypeName = "decimal(12,2)")]
    public decimal Price { get; set; }

    [Required] [StringLength(300)]
    public string? ImageUrl { get; set; }

    public float Stock { get; set; }
    public DateTime InsertDate { get; set; }

    public int CategoryId { get; set; }
    
    [JsonIgnore]
    public Category? Category { get; set; }
}