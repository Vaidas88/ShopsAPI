using Microsoft.AspNetCore.Mvc;
using ShopsAPI.Dtos;
using ShopsAPI.Services;
using System;
using System.Threading.Tasks;

namespace ShopsAPI.Controllers
{
    [Route("[controller]")]
    public class ShopItemController : ControllerBase
    {
        private readonly ShopItemService _shopItemService;

        public ShopItemController(ShopItemService shopItemService)
        {
            _shopItemService = shopItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _shopItemService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _shopItemService.GetByIdAsync(id));
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
        public async Task<IActionResult> Create(CreateShopItemDto createShopItemDto)
        {
            try
            {
                await _shopItemService.CreateAsync(createShopItemDto);

                return Created("", "Shop item created successfully.");
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
        public async Task<IActionResult> Edit(EditShopItemDto editShopItemDto)
        {
            try
            {
                await _shopItemService.UpdateAsync(editShopItemDto);

                return Ok($"Shop item with id: {editShopItemDto.Id} was updated successfully.");
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
                await _shopItemService.DeleteAsync(id);

                return Ok($"Shop item with id: {id} was deleted successfully.");
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
