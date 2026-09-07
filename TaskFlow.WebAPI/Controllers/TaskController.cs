using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Features.Task;

namespace TaskFlow.WebAPI.Controllers;

[ApiController]
[Route("api/projects/{projectId}/tasks")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetTasks(Guid projectId)
    {
        var tasks = await _mediator.Send(new GetTasksByProjectQuery(projectId));
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(Guid projectId, [FromBody] CreateTaskDto dto)
    {
        var task = await _mediator.Send(new CreateTaskCommand(projectId, dto));
        return CreatedAtAction(nameof(GetTasks), new { projectId }, task);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask(Guid taskId, UpdateTaskDto dto)
    {
        try
        {
            var task = await _mediator.Send(new UpdateTaskCommand(taskId, dto));
            return Ok(task);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{taskId}")]
    public async Task<IActionResult> DeleteTask(Guid taskId)
    {

        var result = await _mediator.Send(new DeleteTaskCommand(taskId));
        if (!result) return NotFound();
        return NoContent();
    }
}