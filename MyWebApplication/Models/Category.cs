using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MyWebApplication.Validations;

namespace MyWebApplication.Models;

public class Category
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    [FirstLetterUpperCase]
    public string? Name { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    
    [JsonIgnore]
    public ICollection<Product>? Products { get; set; } = new Collection<Product>();
}