using System.Collections.Immutable;
using AuthLocationApp.Application.CQRS.Provinces.Queries;
using AuthLocationApp.Application.DTOs.Provinces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ILogger = Serilog.ILogger;

namespace AuthLocationApp.Api.Controllers
{
    [Route("api/provinces")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public ProvinceController(IMediator mediator)
        {
            _mediator = mediator;
            _logger = Log.ForContext<ProvinceController>();
        }

        // GET api/provinces/by-country/{countryId}
        [HttpGet("by-country/{countryId:int}")]
        public async Task<ActionResult<IImmutableList<ProvinceDto>>> GetByCountryId(int countryId)
        {
            _logger.Information("Received request: GET /api/provinces/by-country/{CountryId}", countryId);

            var result = await _mediator.Send(new GetAllProvincesByCountryIdQuery(countryId));

            _logger.Information("Returning {Count} provinces for country ID {CountryId}", result.Count, countryId);
            return Ok(result);
        }
    }
}
