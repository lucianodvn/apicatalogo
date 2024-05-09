using System;
using br.com.apicatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace br.com.apicatalogo.Context;

public class ApiCatalogoContext : DbContext
{
	public ApiCatalogoContext(DbContextOptions<ApiCatalogoContext> contextOptions) : base(contextOptions) {}

	public DbSet<Categoria>? Categorias { get; set; }
	public DbSet<Produto> Produtos { get; set; }
}

