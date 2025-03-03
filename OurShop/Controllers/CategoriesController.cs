using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Services;

namespace OurShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        ICategoryService _CategoryService;
        IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public CategoriesController(ICategoryService CategoryService, IMapper mapper , IMemoryCache memoryCache)
        {
            _CategoryService = CategoryService;
            _mapper = mapper;
           _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<getCategoryDto>>> Get()
        {
            if (!_memoryCache.TryGetValue("CategoriesCache", out IEnumerable<Category> categories))
            {
                categories = await _CategoryService.getCategories();

                if (categories == null || !categories.Any())
                {
                    return NotFound();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                };

                _memoryCache.Set("CategoriesCache", categories, cacheEntryOptions);
            }

            return Ok(_mapper.Map<IEnumerable<Category>, IEnumerable<getCategoryDto>>(categories));
        }
    }
}
