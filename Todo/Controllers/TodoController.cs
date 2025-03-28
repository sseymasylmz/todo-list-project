using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<TodoItem>> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        [HttpGet("GetById/{id}")]
        public ActionResult<TodoItem> GetById(int id)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null) return NotFound();
            return todo;
        }

        [HttpPost("Create")]
        public ActionResult<TodoItem> Create(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, TodoItem updatedTodo)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null) return NotFound();

            todo.Title = updatedTodo.Title;
            todo.Description = updatedTodo.Description;
            todo.IsCompleted = updatedTodo.IsCompleted;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null) return NotFound();

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
