using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Service
{
    public class TodoService : ITodoService
    {
        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            _context = context;
        }

        public static TodoItemDTO TodoItemToDTO(TodoItem item) => new TodoItemDTO
        {
            Id = item.Id,
            Name = item.Name,
            IsComplete = item.IsComplete
        };

        public async Task<TodoItemDTO> CreateAsync(TodoItemDTO request)
        {
            var item = new TodoItem
            {
                Name = request.Name,
                IsComplete = request.IsComplete
            };

            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return TodoItemToDTO(item);
        }

        public async Task DeleteAsync(int id)
        {
            TodoItem item = new () { Id = id };
            _context.TodoItems.Remove(item);

            await _context.SaveChangesAsync();
        }

        public Task<List<TodoItemDTO>> QueryAsync()
        {
            return _context.TodoItems
                .Select(x => TodoItemToDTO(x))
                .ToListAsync();
        }

        public async Task<TodoItemDTO?> ReadAsync(int id)
        {
            TodoItem? result = await _context.TodoItems
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result is null) return null;

            return TodoItemToDTO(result);
        }

        public async Task<TodoItemDTO?> UpdateAsync(TodoItemDTO request)
        {
            TodoItem? item = await _context.TodoItems.FindAsync(request.Id);
            if(item is null) return null;

            item.Name = request.Name;
            item.IsComplete = request.IsComplete;

            await _context.SaveChangesAsync();
            return request;
        }
    }
}