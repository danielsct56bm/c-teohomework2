using Microsoft.AspNetCore.Mvc;
using quequesanateo2.Models;

namespace quequesanateo2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private static readonly List<TaskItem> Tasks = new();

    [HttpGet]
    public ActionResult<IEnumerable<TaskItem>> GetTasks()
    {
        return Ok(Tasks);
    }

    [HttpGet("{id}")]
    public ActionResult<TaskItem> GetTask(int id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);
        return task is not null ? Ok(task) : NotFound();
    }

    [HttpPost]
    public ActionResult<TaskItem> CreateTask(TaskItem task)
    {
        task.Id = Tasks.Count + 1;
        Tasks.Add(task);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, TaskItem updatedTask)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);
        if (task is null) return NotFound();

        task.Title = updatedTask.Title;
        task.Status = updatedTask.Status;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);
        if (task is null) return NotFound();

        Tasks.Remove(task);
        return NoContent();
    }
}