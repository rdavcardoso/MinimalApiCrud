using MinimalApiCrud.Models;

namespace MinimalApiCrud.Repositories;

public static class ProdutoRepository
{
    private static readonly List<Produto> produtos = new();
    private static int proximoId = 1;

    public static List<Produto> ObterTodos() => produtos;
    public static Produto? ObterPorId(int id) => 
        produtos.FirstOrDefault(p => p.Id == id);

    public static Produto Criar(Produto produto)
    {
        produto.Id = proximoId++;
        produtos.Add(produto);
        return produto;
    }

    public static void Atualizar(Produto produto)
    {
        var index = produtos.FindIndex(p => p.Id == produto.Id);
        if (index != -1) 
            produtos[index] = produto;
    }

    public static void Remover(int id)
    {
        var produto = ObterPorId(id);
        if (produto != null) 
            produtos.Remove(produto);
    }
}