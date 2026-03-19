using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyScraper.Application.DTOs;
using CurrencyScraper.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyScraper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrencyUseCase _useCase;

        public CurrenciesController(ICurrencyUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrencyResponse>>> GetAll()
        {
            var result = await _useCase.GetAllCurrenciesAsync();
            return Ok(result);
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<CurrencyResponse>> GetLatest(string code)
        {
            var result = await _useCase.GetLatestByCodeAsync(code.ToUpper());
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
