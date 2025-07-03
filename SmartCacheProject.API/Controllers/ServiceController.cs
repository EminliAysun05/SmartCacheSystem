using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCacheProject.Application.Services.Interfaces;
using SmartCacheProject.Domain.Dtos.Service;

namespace SmartCacheProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _serviceService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _serviceService.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceCreateDto dto)
        {
            var newId = await _serviceService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ServiceUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var success = await _serviceService.UpdateAsync(dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _serviceService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
