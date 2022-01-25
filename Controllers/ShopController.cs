using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopsAPI.Dtos;
using ShopsAPI.Models;
using ShopsAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly ShopService _shopService;

        public ShopController(ShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_shopService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_shopService.GetById(id));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Bad request.");
            }
        }

        [HttpPost]
        public IActionResult Create(CreateShopDto createShopDto)
        {
            try
            {
                _shopService.Create(createShopDto);

                return Created("", "Shop created successfully.");
            }
            catch (ArgumentException ex)
            {
                return StatusCode(409, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Bad request.");
            }
        }

        [HttpPut]
        public IActionResult Edit(EditShopDto editShopDto)
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
