using Microsoft.AspNetCore.Mvc;
using ShopsAPI.Dtos;
using ShopsAPI.Services;
using System;

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
        public IActionResult GetAll()
        {
            return Ok(_shopItemService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_shopItemService.GetById(id));
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
        public IActionResult Create(CreateShopItemDto createShopItemDto)
        {
            try
            {
                _shopItemService.Create(createShopItemDto);

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
        public IActionResult Edit(EditShopItemDto editShopItemDto)
        {
            try
            {
                _shopItemService.Update(editShopItemDto);

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
        public IActionResult Delete(int id)
        {
            try
            {
                _shopItemService.Delete(id);

                return Ok($"Shop item with id: {id} was deleted successfully.");
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
