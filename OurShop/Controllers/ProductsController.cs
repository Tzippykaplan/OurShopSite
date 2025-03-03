using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using DTO;
using Microsoft.Extensions.Caching.Memory;
using OurShop;

namespace OurShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
         IProductService _productService;
         IMapper _mapper;
        IMemoryCache _memoryCache;

        public ProductsController(IProductService productService, IMapper mapper, IMemoryCache memoryCache)
        {
            _productService = productService;
            _mapper = mapper;
            _memoryCache = memoryCache;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get(string? desc, int? minPrice, int? maxPrice, [FromQuery] int?[] categoryIds)
        {
            if (!_memoryCache.TryGetValue("ProductsCache", out IEnumerable<Product> products))
            {

                products = await _productService.getProducts(desc, minPrice, maxPrice, categoryIds);
                if (products == null || !products.Any())
                    return NotFound();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                };

                _memoryCache.Set("ProductsCache", products, cacheEntryOptions);
            }
            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<Product>>(products));
          
        }

   
    }
    }
