using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinimalApiCrud.Data;
using MinimalApiCrud.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? "Data Source=produtos.db";

builder.Services.AddDbContext<AppDbContexts>(options => 
    options.UseSqlite(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.MapGet("/", () => "API de Produtos Minimal com EF Core + SQLite!");

app.MapGet("/produtos", async (AppDbContexts db) =>
    await db.Produtos.ToListAsync());

app.MapGet("/produtos/{id:int}",  async (int id, AppDbContexts db) =>
    await db.Produtos.FindAsync(id)
        is Produto produto ? Results.Ok(produto) : Results.NotFound());

app.MapPost("/produtos", async (Produto produto, AppDbContexts db) =>
{
    db.Produtos.Add(produto);
    await db.SaveChangesAsync();
    return Results.Created($"/produtos/{produto.Id}", produto);
});

app.MapPut("/produtos/{id:int}", async (int id, Produto inputProduto, AppDbContexts db) =>
{
    var produto = await db.Produtos.FindAsync(id);
    if (produto is null) return Results.NotFound();

    produto.Nome = inputProduto.Nome;
    produto.Preco = inputProduto.Preco;

    await db.SaveChangesAsync();
    return Results.Ok(produto);
});

app.MapDelete("/produtos/{id:int}", async (int id, AppDbContexts db) =>
{
    var produto = await db.Produtos.FindAsync(id);
    if (produto is null) return Results.NotFound();

    db.Produtos.Remove(produto);
    await db.SaveChangesAsync();

    return Results.NoContent();
});


app.UseSwagger();
app.UseSwaggerUI();
app.Run();