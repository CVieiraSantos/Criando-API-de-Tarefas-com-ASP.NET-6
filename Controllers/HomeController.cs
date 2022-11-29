using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
       [HttpGet("/")]
       public IActionResult Get([FromServices]AppDbContext context)
       {
            var result = context.Todos.ToList();
            return Ok(result);
       }

       [HttpGet("/{id:int}")]
       public IActionResult GetById([FromRoute]int id,[FromServices] AppDbContext context)
       {
            var result = context.Todos.FirstOrDefault(x=> x.Id == id);

            if(result == null)
                return NotFound();
            
            return Ok(result);
       }

       [HttpPost("/")]
       public IActionResult Put([FromBody]TodoModel todo, [FromServices]AppDbContext context)
       {
            context.Todos.Add(todo);
            context.SaveChanges();
            return Created($"/{todo.Id}", todo);
       }

       [HttpPut("/{id:int}")]
       public IActionResult Put([FromRoute]int id,
       [FromBody]TodoModel todo,
       [FromServices]AppDbContext context)
       {
            var result = context.Todos.FirstOrDefault(x=> x.Id == id);

            if(result == null)
                return NotFound();
            
            result.Title = todo.Title;
            result.Done = todo.Done;

            context.Todos.Update(result);
            context.SaveChanges();
            return Ok(result);            
       }

       [HttpDelete("/{id:int}")]
       public IActionResult Delete([FromRoute]int id,
       [FromServices]AppDbContext context)
       {
            var result = context.Todos.FirstOrDefault(x=> x.Id == id);
            
            if(result == null)
                return NotFound();
            
            context.Todos.Remove(result);
            context.SaveChanges();
            return Ok(result);
       }
    }
}