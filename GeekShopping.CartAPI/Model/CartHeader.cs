using GeekShopping.CartAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartAPI.Model;

[Table("carrinho_cabecalho")]
public class CartHeader : BaseEntity
{
    [Column("id_usuario")]
    public string? UserId { get; set; }

    [Column("codigo_cupom")]
    public string? CouponCode { get; set; }
}