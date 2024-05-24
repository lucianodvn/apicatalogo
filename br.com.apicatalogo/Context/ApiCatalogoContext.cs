using System;
using br.com.apicatalogo.Models;
using br.com.apicatalogo.Models.Tokens;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace br.com.apicatalogo.Context;

public class ApiCatalogoContext : IdentityDbContext<ApplicationUser>
{
    public ApiCatalogoContext(DbContextOptions<ApiCatalogoContext> contextOptions) : base(contextOptions) { }

    public DbSet<Categoria>? Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
}

