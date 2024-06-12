using BugTracker.Api.Models.Requests;
using BugTracker.Api.Services.StorageServices;
using BugTracker.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BugTracker.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PhotosController : Controller
    {
        private readonly UserManager<User> UserManager;
        private readonly BlobStorageService BlobStorageService;

        public PhotosController(UserManager<User> userManager, BlobStorageService blobStorageService)
        {
            UserManager = userManager;
            BlobStorageService = blobStorageService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm]IFormFile file)
        {
            //checks if the file is empty
            if(file == null || file.Length == 0)
                return BadRequest("No file was uploaded.");

            //checks if the file is an image
            if (!IsImage(file.ContentType))
                return BadRequest("Uploaded file is not an image.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await UserManager.FindByIdAsync(userId);

            if(user.ProfilePictureUrl != null)
            {
                await BlobStorageService.Delete(user.ProfilePictureUrl);
            }

            string imageUrl = await BlobStorageService.Upload(file);

            user.ProfilePictureUrl = imageUrl;

            var result = await UserManager.UpdateAsync(user);
            if(!result.Succeeded)
                return BadRequest();

            return Ok(imageUrl);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await UserManager.FindByIdAsync(userId);

            if (user.ProfilePictureUrl == null) 
                return BadRequest("User profile picture is already null.");

            await BlobStorageService.Delete(user.ProfilePictureUrl);
            user.ProfilePictureUrl = null;

            var result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest("Something went wrong when saving changes.");

            return Ok();
        }

        //helpers
        private bool IsImage(string contentType)
        {
            return contentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase);
        }
    }
}
