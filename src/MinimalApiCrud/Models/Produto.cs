using System.ComponentModel.DataAnnotations;

namespace MinimalApiCrud.Models;

public class Produto
{
    public int Id { get; set; }
    
    [Required]
    public string Nome { get; set; } = string.Empty;
    
    [Range(0.01, double.MaxValue)]
    public decimal Preco { get; set; }
}
