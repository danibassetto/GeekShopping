using GeekShopping.OrderAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.OrderAPI.Model;

[Table("pedido_detalhe")]
public class OrderDetail : BaseEntity
{
    [Column("id_pedido_cabecalho")]
    public long OrderHeaderId { get; set; }

    [ForeignKey("OrderHeaderId")]
    public virtual OrderHeader? OrderHeader { get; set; }

    [Column("id_produto")]
    public long ProductId { get; set; }

    [Column("count")]
    public int Count { get; set; }

    [Column("nome_produto")]
    public string? ProductName { get; set; }

    [Column("preco")]
    public decimal Price { get; set; }
}