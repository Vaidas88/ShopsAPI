using Microsoft.AspNetCore.Mvc;
using ShopsAPI.Dtos;
using ShopsAPI.Services;
using System;
using System.Threading.Tasks;

namespace ShopsAPI.Controllers
{
    [Route("[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly ShopService _shopService;

        public ShopController(ShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _shopService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _shopService.GetByIdAsync(id));
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Bad request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateShopDto createShopDto)
        {
            try
            {
                await _shopService.CreateAsync(createShopDto);

                return Created("", "Shop created successfully.");
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(409, ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Bad request.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditShopDto editShopDto)
        {
            try
            {
                await _shopService.UpdateAsync(editShopDto);

                return Ok($"Shop with id: {editShopDto.Id} was updated successfully.");
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
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

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _shopService.DeleteAsync(id);

                return Ok($"Shop with id: {id} was deleted successfully.");
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
