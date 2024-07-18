using AutoMapper;
using AutoMapper.QueryableExtensions;
using BugTracker.Domain.Models;
using BugTracker.Domain.Models.DTOs;
using BugTracker.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly BugTrackerDbContext DbContext;
        private readonly UserManager<User> UserManager;
        private readonly IMapper Mapper;

        public UsersController(BugTrackerDbContext dbContext, UserManager<User> userManager, IMapper mapper)
        {
            DbContext = dbContext;
            UserManager = userManager;
            Mapper = mapper;
        }

        [HttpPatch]
        [Route("{userId}")]
        public async Task<IActionResult> Update([FromRoute] string userId, [FromBody] JsonPatchDocument<UserDTO> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            User user = await UserManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            UserDTO? userToUpdate = Mapper.Map<UserDTO>(user);

            patchDoc.ApplyTo(userToUpdate!, ModelState);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            Mapper.Map(userToUpdate, user);

            foreach(var operation in patchDoc.Operations)
            {
                var path = operation.path.TrimStart('/');
                if (path == "email")
                    user.UserName = user.Email;
            }

            var result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            UserDTO? updatedUser = await DbContext.Users.ProjectTo<UserDTO>(Mapper.ConfigurationProvider).SingleOrDefaultAsync(u => u.Id == user.Id);
            return Ok(updatedUser);
        }

        [HttpDelete]
        [Route ("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            User? user = await DbContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
            if (user == null)
                return NotFound();

            DbContext.Users.Remove(user);
            await DbContext.SaveChangesAsync();

            return Ok("User was successfully deleted.");
        }

        
    }
}
