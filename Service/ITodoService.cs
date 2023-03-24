using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Service
{
    public interface ITodoService
    {
        Task<List<TodoItemDTO>> QueryAsync();
        Task<TodoItemDTO?> ReadAsync(int id);
        Task<TodoItemDTO> CreateAsync(TodoItemDTO request);
        Task<TodoItemDTO?> UpdateAsync(TodoItemDTO request);
        Task DeleteAsync(int id);
    }
}