using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Service;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetAsync()
        {
            var result = await _service.QueryAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetAsync(int id)
        {
            var result = await _service.ReadAsync(id);
            if(result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostAsync(TodoItemDTO request)
        {
            var result  = await _service.CreateAsync(request);
            return CreatedAtAction( "Get", new { id = request.Id}, request );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> PutAsync(int id, TodoItemDTO request)
        {
            if(id != request.Id) return BadRequest();

            var result = await _service.UpdateAsync(request);
            if(result is null) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}