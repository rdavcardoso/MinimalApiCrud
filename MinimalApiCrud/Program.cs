using Microsoft.AspNetCore.Http.HttpResults;
using MinimalApiCrud.Models;
using MinimalApiCrud.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/", () => "API de Produtos Minimal!");

app.MapGet("/produtos", () =>
{
    return ProdutoRepository.ObterTodos();
});

app.MapGet("/produtos/{id:int}", (int id) =>
{
    var produto = ProdutoRepository.ObterPorId(id);
    return produto is null ? Results.NotFound() : Results.Ok(produto);
});

app.MapPost("/produtos", (Produto produto) =>
{
   var novoProduto = ProdutoRepository.Criar(produto);
   return Results.Created($"/produtos/{novoProduto.Id}", novoProduto);
});

app.MapPut("/produtos/{id:int}", (int id, Produto produto) =>
{
   var existente = ProdutoRepository.ObterPorId(id);
   if (existente is null) return Results.NotFound();
   
   produto.Id = id;
   ProdutoRepository.Atualizar(produto);
   return Results.Ok(produto);
});

app.MapDelete("/produtos/{id:int}", (int id) =>
{
   var existente = ProdutoRepository.ObterPorId(id); 
   if (existente is null) return Results.NotFound();
   
   ProdutoRepository.Remover(id);
   return Results.NoContent();
});

app.UseSwagger();
app.UseSwaggerUI();
app.Run();