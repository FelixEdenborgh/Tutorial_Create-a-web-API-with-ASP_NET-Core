

//TodoItemDTO
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial_Create_a_web_API_with_ASP_NET_Core.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly TodoContext _context;

    public TodoItemsController(TodoContext context)
    {
        _context = context;
    }

    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
    {
        return await _context.TodoItems
            .Select(x => ItemToDTO(x))
            .ToListAsync();
    }

    // GET: api/TodoItems/5
    // <snippet_GetByID>
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        return ItemToDTO(todoItem);
    }
    // </snippet_GetByID>

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Update>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoDTO)
    {
        if (id != todoDTO.Id)
        {
            return BadRequest();
        }

        var todoItem = await _context.TodoItems.FindAsync(id);
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
    // </snippet_Update>

    // POST: api/TodoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Create>
    [HttpPost]
    public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoDTO)
    {
        var todoItem = new TodoItem
        {
            IsComplete = todoDTO.IsComplete,
            Name = todoDTO.Name
        };

        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetTodoItem),
            new { id = todoItem.Id },
            ItemToDTO(todoItem));
    }
    // </snippet_Create>

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }

        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TodoItemExists(long id)
    {
        return _context.TodoItems.Any(e => e.Id == id);
    }

    private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
       new TodoItemDTO
       {
           Id = todoItem.Id,
           Name = todoItem.Name,
           IsComplete = todoItem.IsComplete
       };
}






//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Tutorial_Create_a_web_API_with_ASP_NET_Core.Models;

//namespace Tutorial_Create_a_web_API_with_ASP_NET_Core.Controllers
//{
//    [Route("api/TodoItems")]
//    [ApiController]
//    public class TodoItemsController : ControllerBase
//    {
//        private readonly TodoContext _context;

//        public TodoItemsController(TodoContext context)
//        {
//            _context = context;
//        }

//        // GET: api/TodoItems
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
//        {
//            return await _context.TodoItems.ToListAsync();
//        }

//        // GET: api/TodoItems/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
//        {
//            var todoItem = await _context.TodoItems.FindAsync(id);

//            if (todoItem == null)
//            {
//                return NotFound();
//            }

//            return todoItem;
//        }

//        // PUT: api/TodoItems/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
//        {
//            if (id != todoItem.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(todoItem).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!TodoItemExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/TodoItems
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
//        {
//            _context.TodoItems.Add(todoItem);
//            await _context.SaveChangesAsync();

//            //    return CreatedAtAction("PostTodoItem", new { id = todoItem.Id }, todoItem);
//            return CreatedAtAction(nameof(PostTodoItem), new { id = todoItem.Id }, todoItem);
//        }

//        // DELETE: api/TodoItems/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteTodoItem(long id)
//        {
//            var todoItem = await _context.TodoItems.FindAsync(id);
//            if (todoItem == null)
//            {
//                return NotFound();
//            }

//            _context.TodoItems.Remove(todoItem);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool TodoItemExists(long id)
//        {
//            return _context.TodoItems.Any(e => e.Id == id);
//        }
//    }
//}
