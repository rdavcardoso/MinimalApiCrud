using Microsoft.EntityFrameworkCore;
using MinimalApiCrud.Models;

namespace MinimalApiCrud.Data;

public class AppDbContexts : DbContext
{
    public AppDbContexts(DbContextOptions<AppDbContexts> options) : base(options) { }
    
    public DbSet<Produto> Produtos => Set<Produto>();
}
