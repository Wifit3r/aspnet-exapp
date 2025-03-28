using Microsoft.AspNetCore.Mvc;
using ASPNetExapp.Models;
using ASPNetExapp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class NewspaperController : ControllerBase
{
    private readonly NewspaperService _newspaperService;

    public NewspaperController(NewspaperService newspaperService)
    {
        _newspaperService = newspaperService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<Newspaper>>> GetNewspapers([FromQuery] string? query, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        return Ok(await _newspaperService.GetNewspapers(query, page, pageSize));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Newspaper>> GetNewspaper(int id)
    {
        var newspaper = await _newspaperService.GetNewsById(id);
        if (newspaper == null) return NotFound();
        return Ok(newspaper);
    }

    [HttpPost]
    public async Task<ActionResult<Newspaper>> CreateNewspaper([FromBody] Newspaper newNewspaper)
    {
        var createdNewspaper = await _newspaperService.CreateNewspaper(newNewspaper);
        return CreatedAtAction(nameof(GetNewspaper), new { id = createdNewspaper.ID }, createdNewspaper);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNewspaper(int id, [FromBody] Newspaper updatedNewspaper)
    {
        var updated = await _newspaperService.UpdateNewspaper(id, updatedNewspaper);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNewspaper(int id)
    {
        var deleted = await _newspaperService.DeleteNewspaper(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
