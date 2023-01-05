using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using REST_API_LEARN.Controllers;
using REST_API_LEARN.DB;
using REST_API_LEARN.Filters;
using REST_API_LEARN.Models;
using System;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class TodoItemsController : ControllerBase
{
    private readonly TodoContext _context;
    private readonly ILogger<TodoItemsController> _logger;

    public TodoItemsController(TodoContext context, ILogger<TodoItemsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoItemDTO>>> GetTodoItems()
    {
        return await _context.ToDoRows
            .Select(x => ItemToDTO(x))
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDTO>> GetTodoItem(int id)
    {
        var todoItem = await _context.ToDoRows.FindAsync(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        return ItemToDTO(todoItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(int id, TodoItemDTO todoDTO)
    {
        if (id != todoDTO.Id)
        {
            return BadRequest();
        }

        var todoItem = await _context.ToDoRows.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }

        todoItem.Name = todoDTO.Name;
        todoItem.IsComplete = todoDTO.IsComplete;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

  
    [HttpPost]
    [SampleAsyncActionFilter]
    public async Task<ActionResult<TodoItemDTO>> PostTodoItem([FromBody] TodoItemDTO todoDTO)
    {
        if (!ModelState.IsValid)
        {
            throw new Exception("Ошибка");
        }

        var todoItem = new TodoItem
            {
                IsComplete = todoDTO.IsComplete,
                Name = todoDTO.Name
            };

            _context.ToDoRows.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDTO(todoItem));

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(int id)
    {
        var todoItem = await _context.ToDoRows.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }

        _context.ToDoRows.Remove(todoItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TodoItemExists(int id)
    {
        return _context.ToDoRows.Any(e => e.Id == id);
    }

    private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
       new TodoItemDTO
       {
           Id = todoItem.Id,
           Name = todoItem.Name,
           IsComplete = todoItem.IsComplete
       };
}

