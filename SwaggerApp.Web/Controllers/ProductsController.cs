using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwaggerApp.Web.Models;

namespace SwaggerApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SwaggerDBContext _context;

        public ProductsController(SwaggerDBContext context)
        {
            _context = context;
        }

        //3 tane /// bastığımızda oluşur summary, ayrıca model de açıklama yapabiliriz.

        /// <summary>
        /// Tüm Ürünleri Liste Şeklinde döner
        /// </summary>
        /// <remarks>
        /// örnek: https://localhost:44358/api/products
        /// </remarks>
        /// <returns></returns>
  
        [Produces("application/json")]//  dönüş tipini belirledik     
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()        {
        
            return await _context.Products.ToListAsync();
        }

        /// <summary>
        /// İd'ye göre ürünü döner
        /// </summary>
        /// <param name="id">Ürünün id'si</param>
        /// <returns></returns>
        /// <response code="404">Verilen id'ye sahip ürün bulunamadı</response>
        /// <response code="200">Verilen id'ye sahip ürün var</response>
        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        /// <summary>
        /// Verilen değere göre ürün ekler
        /// </summary>
        /// <remarks>
        /// örnek json:{"name":"kalem","price":20,"date":"2.2.2016","category":"Kırtasiye"}
        /// </remarks>
        /// <param name="id">Urünün id'si</param>
        /// <param name="product">Ürün Nesnesi</param>
        /// <returns></returns>
        /// <response code="404">Verilen id'ye sahip ürün bulunamadı</response>
        /// <response code="200">Verilen id'ye sahip ürün var</response>
        [Consumes("application/json")]//tipinde veri isteriz
        [Produces("application/json")]//  dönüş tipini belirledik    
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
