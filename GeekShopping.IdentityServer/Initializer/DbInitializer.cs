﻿using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShopping.IdentityServer.Initializer;

public class DbInitializer : IDbInitializer
{
    private readonly MySQLContext _context;
    private readonly UserManager<ApplicationUser> _user;
    private readonly RoleManager<IdentityRole> _role;

    public DbInitializer(MySQLContext context,
        UserManager<ApplicationUser> user,
        RoleManager<IdentityRole> role)
    {
        _context = context;
        _user = user;
        _role = role;
    }

    public void Initialize()
    {
        if (_role.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;
        _role.CreateAsync(new IdentityRole(
            IdentityConfiguration.Admin)).GetAwaiter().GetResult();
        _role.CreateAsync(new IdentityRole(
            IdentityConfiguration.Client)).GetAwaiter().GetResult();

        ApplicationUser admin = new ApplicationUser()
        {
            UserName = "dani-admin",
            Email = "dani-admin@erudio.com.br",
            EmailConfirmed = true,
            PhoneNumber = "+55 (34) 12345-6789",
            FirstName = "Dani",
            LastName = "Admin"
        };

        _user.CreateAsync(admin, "Dani123$").GetAwaiter().GetResult();
        _user.AddToRoleAsync(admin,
            IdentityConfiguration.Admin).GetAwaiter().GetResult();
        var adminClaims = _user.AddClaimsAsync(admin, new Claim[]
        {
            new(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
            new(JwtClaimTypes.GivenName, admin.FirstName),
            new(JwtClaimTypes.FamilyName, admin.LastName),
            new(JwtClaimTypes.Role, IdentityConfiguration.Admin)
        }).Result;

        ApplicationUser client = new ApplicationUser()
        {
            UserName = "dani-client",
            Email = "dani-client@erudio.com.br",
            EmailConfirmed = true,
            PhoneNumber = "+55 (34) 12345-6789",
            FirstName = "Dani",
            LastName = "Client"
        };

        _user.CreateAsync(client, "Dani123$").GetAwaiter().GetResult();
        _user.AddToRoleAsync(client,
            IdentityConfiguration.Client).GetAwaiter().GetResult();
        var clientClaims = _user.AddClaimsAsync(client, new Claim[]
        {
            new(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
            new(JwtClaimTypes.GivenName, client.FirstName),
            new(JwtClaimTypes.FamilyName, client.LastName),
            new(JwtClaimTypes.Role, IdentityConfiguration.Client)
        }).Result;
    }
}