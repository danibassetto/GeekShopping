﻿namespace GeekShopping.CartAPI.Model;

public class Cart
{
    public CartHeader? CartHeader { get; set; }
    public IEnumerable<CartDetail>? ListCartDetail { get; set; }
}