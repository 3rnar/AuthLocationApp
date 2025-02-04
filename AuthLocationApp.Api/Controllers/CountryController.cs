using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuthLocationApp.Application.CQRS.Countries.Queries;
using AuthLocationApp.Application.DTOs.Countries;
using Serilog;
using ILogger = Serilog.ILogger;

namespace AuthLocationApp.Api.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
            _logger = Log.ForContext<CountryController>();
        }

        // GET api/countries
        [HttpGet]
        public async Task<ActionResult<List<CountryDto>>> GetAll()
        {
            _logger.Information("Received request: GET /api/countries");

            var result = await _mediator.Send(new GetAllCountriesQuery());

            _logger.Information("Returning {Count} countries", result.Count);
            return Ok(result);
        }
    }
}
