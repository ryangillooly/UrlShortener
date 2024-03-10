using Microsoft.AspNetCore.Mvc;
using UrlShortener.Contracts;
using UrlShortener.Services;

namespace UrlShortener.Controllers;

[ApiController]
public class UrlController(IDatabaseService databaseService) : ControllerBase
{
    [HttpPost("")]
    public async Task<IActionResult> CreateUrl([FromBody] CreateUrlRequest request)
    {
        var url = await databaseService.CreateUrl(request);
        return CreatedAtAction(nameof(CreateUrl), url.Hash, url);
    }
    
    [HttpGet("{key}")]
    public async Task<IActionResult> RedirectRequest([FromRoute] string key)
    {
        var url = await databaseService.GetUrl(key);
        if (url == null)
        {
            return NotFound();
        }
        
        return Redirect(url.LongUrl);
    }
    
    [HttpGet("{key}/data")]
    public async Task<IActionResult> GetUrl([FromRoute] string key)
    {
        var url = await databaseService.GetUrl(key);
        if (url == null)
        {
            return NotFound();
        }
        
        return Ok(url);
    }

    [HttpPut("{key}")]
    public async Task<IActionResult> UpdateUrl([FromRoute] string key, [FromBody] UpdateUrlRequest request)
    {
        var data = await databaseService.UpdateUrl(key, request);
        if (data == null) return NotFound();
        
        return Ok(data);
    }


    [HttpDelete("{key}")]
    public async Task<IActionResult> DeleteUrl(string key) =>
        await databaseService.DeleteUrl(key)
            ? NoContent()
            : NotFound();
}
