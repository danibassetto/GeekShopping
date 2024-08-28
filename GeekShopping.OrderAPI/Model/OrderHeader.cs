using GeekShopping.OrderAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.OrderAPI.Model;

[Table("pedido_cabecalho")]
public class OrderHeader : BaseEntity
{
    [Column("id_usuario")]
    public string? UserId { get; set; }

    [Column("codigo_cupom")]
    public string? CouponCode { get; set; }

    [Column("valor_compra")]
    public decimal PurchaseAmount { get; set; }

    [Column("valor_desconto")]
    public decimal DiscountAmount { get; set; }

    [Column("primeiro_nome")]
    public string? FirstName { get; set; }

    [Column("sobrenome")]
    public string? LastName { get; set; }

    [Column("data_compra")]
    public DateTime DateTime { get; set; }

    [Column("data_pedido")]
    public DateTime OrderDate { get; set; }

    [Column("telefone")]
    public string? Phone { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("numero_cartao")]
    public string? CardNumber { get; set; }

    [Column("cvv")]
    public string? CVV { get; set; }

    [Column("mes_ano_expiracao")]
    public string? ExpiryMonthYear { get; set; }

    [Column("total_itens")]
    public int CartTotalItens { get; set; }

    public List<OrderDetail>? ListOrderDetail { get; set; }

    [Column("status_pagamento")]
    public bool PaymentStatus { get; set; }
}