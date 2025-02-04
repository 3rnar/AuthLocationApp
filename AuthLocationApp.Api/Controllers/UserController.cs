using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuthLocationApp.Application.CQRS.Users.Commands;
using AuthLocationApp.Application.CQRS.Users.Queries;
using AuthLocationApp.Application.DTOs.Users;
using Serilog;
using ILogger = Serilog.ILogger;

namespace AuthLocationApp.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
            _logger = Log.ForContext<UserController>();
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] UserCreateDto userDto)
        {
            _logger.Information("Creating a new user with email {Email}", userDto.Email);

            var command = new CreateUserCommand(userDto);
            var userId = await _mediator.Send(command);

            _logger.Information("User created successfully with ID {UserId}", userId);
            return CreatedAtAction(nameof(GetById), new { id = userId }, userId);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            _logger.Information("Fetching user with ID {UserId}", id);

            var query = new GetUserByIdQuery(id);
            var user = await _mediator.Send(query);

            if (user == null)
            {
                _logger.Warning("User with ID {UserId} not found", id);
                return NotFound();
            }

            return Ok(user);
        }
    }
}
