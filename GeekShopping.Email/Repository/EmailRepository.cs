﻿using GeekShopping.Email.Messages;
using GeekShopping.Email.Model;
using GeekShopping.Email.Model.Context;
using GeekShopping.Email.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.Repository;

public class EmailRepository(DbContextOptions<MySQLContext> context) : IEmailRepository
{
    private readonly DbContextOptions<MySQLContext> _context = context;

    public async Task LogEmail(UpdatePaymentResultMessage message)
    {
        EmailLog email = new()
        {
            Email = message.Email,
            SentDate = DateTime.Now,
            Log = $"Order - {message.OrderId} has been created successfully!"
        };
        await using var _db = new MySQLContext(_context);
        _db.EmailLogs.Add(email);
        await _db.SaveChangesAsync();
    }
}