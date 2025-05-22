using MinimalApiCrud.Client.Models;
using System.Net.Http.Json;

namespace MinimalApiCrud.Client.Services;

public class ProdutoService
{
    private readonly HttpClient _http;

    public ProdutoService(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<List<Produto>> GetProdutosAsync() => 
        await _http.GetFromJsonAsync<List<Produto>>("produtos") ?? new();
    
    public async Task<Produto?> GetProdutoAsync(int id) => 
        await _http.GetFromJsonAsync<Produto>($"produtos/{id}");
    
    public async Task CreateProdutoAsync(Produto produto) => 
        await _http.PostAsJsonAsync("produtos", produto);
    
    public async Task UpdateProdutoAsync(Produto produto) =>
        await _http.PutAsJsonAsync($"produtos/{produto.Id}", produto);
    
    public async Task DeleteProdutoAsync(int id) =>
        await _http.DeleteAsync($"produtos/{id}");
}