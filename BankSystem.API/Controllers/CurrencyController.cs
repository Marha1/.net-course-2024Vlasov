using BankSystem.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly CurrencyService _currencyService;

    public CurrencyController(CurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    [HttpGet("convert")]
    public async Task<IActionResult> Convert(decimal amount, string from, string to)
    {
        try
        {
            //HttpContext.RequestAborted вместео CancellationToken
            var convertedAmount =
                await _currencyService.ConvertCurrencyAsync(amount, from, to, HttpContext.RequestAborted);
            return Ok(new { Amount = convertedAmount });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}