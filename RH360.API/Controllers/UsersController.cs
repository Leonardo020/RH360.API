using MediatR;
using Microsoft.AspNetCore.Mvc;
using RH360.Application.Users.CreateUser;
using RH360.Application.Users.DeleteUser;
using RH360.Application.Users.GetUserById;
using RH360.Application.Users.GetUsers;
using RH360.Application.Users.UpdateUser;
using RH360.Domain.Payloads;

namespace RH360.API.Controllers
{

    [ApiController]
    [Route("api/users")]
    public class UsersController(IMediator Mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequestPayload pagination)
        {
            var result = await Mediator.Send(
                new GetUsersQuery(pagination)
            );

            return (result is null || !result.Items.Any()) ?
                NoContent() :
                Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await Mediator.Send(
                new GetUserByIdQuery(id)
            );

            return result is null ?
                throw new Exception("Error on get user") :
                Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand cmd)
        {
            var id = await Mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserCommand cmd)
        {
            if (id != cmd.Id)
                return BadRequest($"ID mismatch: Route: {id} | Body: {cmd.Id}");

            var ok = await Mediator.Send(cmd);

            return ok ? 
                NoContent() : 
                throw new Exception("Error on update user");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePassword(int id, UpdateUserPasswordCommand cmd)
        {
            if (id != cmd.Id)
                return BadRequest($"ID mismatch: Route: {id} | Body: {cmd.Id}");

            var ok = await Mediator.Send(cmd);

            return ok ?
                NoContent() :
                throw new Exception("Error on update user");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await Mediator.Send(new HardDeleteUserCommand(id));

            return ok ? 
                NoContent() : 
                throw new Exception("Error on delete user");
        }
    }

}
