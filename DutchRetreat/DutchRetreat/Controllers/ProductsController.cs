using DutchRetreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchRetreat.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController:Controller
    {
        private IDutchRepository _repository;
        private ILogger<ProductsController> _logger;

        public ProductsController(IDutchRepository repository,ILogger<ProductsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllProducts()); 
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get products: {ex}");
                return BadRequest("Failed to get Products");
            }
        }
    }
}
