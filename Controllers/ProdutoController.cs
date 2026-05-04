using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CrudProdutos.Data;
using Microsoft.EntityFrameworkCore;
using CrudProdutos.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CrudProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _AppDbContext;

        public ProdutoController(AppDbContext AppDbContext)
        {
            _AppDbContext = AppDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> Get()
        {
            var produtos = await _AppDbContext.Produtos.ToListAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var produto = await _AppDbContext.Produtos.FindAsync(id);
            if (produto == null)
                return NotFound();
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Create(Produto produto)
        {
            _AppDbContext.Produtos.Add(produto);
            await _AppDbContext.SaveChangesAsync();
            return Ok(produto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Produto>> Update(int id, Produto produto)
        {
            if (id != produto.Id)
                return BadRequest();
            _AppDbContext.Entry(produto).State = EntityState.Modified;
            await _AppDbContext.SaveChangesAsync();
            return NoContent();
        }
       
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var produto = await _AppDbContext.Produtos.FindAsync(id);
            if (produto == null)
                return NotFound();
            _AppDbContext.Produtos.Remove(produto);
            await _AppDbContext.SaveChangesAsync();
            return NoContent();

        }

    }
}
