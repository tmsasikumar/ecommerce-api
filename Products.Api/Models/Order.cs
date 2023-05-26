using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Api.Models;

public class Order
{
    public Guid Id { get; set; }

    [Column(TypeName = "jsonb")]
    public string Products { get; set; }
}