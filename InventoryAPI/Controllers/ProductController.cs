
using InventoryAPI.DbContexts;
using InventoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ApplicationDbContext _ApplicationDbContext;

        public ProductController(ApplicationDbContext ApplicationDbContext)
        {
            _ApplicationDbContext = ApplicationDbContext;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _ApplicationDbContext.Products;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _ApplicationDbContext.Products.FirstOrDefault(s => s.ProductId == id);
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] Product value)
        {
            _ApplicationDbContext.Products.Add(value);
            _ApplicationDbContext.SaveChanges();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product value)
        {
            var product = _ApplicationDbContext.Products.FirstOrDefault(s => s.ProductId == id);
            if (product != null)
            {
                _ApplicationDbContext.Entry(product).CurrentValues.SetValues(value);
                _ApplicationDbContext.SaveChanges();
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var product = _ApplicationDbContext.Products.FirstOrDefault(s => s.ProductId == id);
            if (product != null)
            {
                _ApplicationDbContext.Products.Remove(product);
                _ApplicationDbContext.SaveChanges();
            }
        }
    }
}
