using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileEditor.Data;
using ProfileEditor.Models;

namespace ProfileEditor.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly ProfileEditorDbContext _context;
        public ProfileController(UserManager<UserModel> userManager, ProfileEditorDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == user.Id);
            var viewModel = new ProfileViewModel
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Adress = profile.Adress,
                City = profile.City,
                Country = profile.Country,
                ContactNumber= profile.ContactNumber,
                PhotoImageName = profile.PhotoImage,
                UserName = user.UserName,
                Email = user.Email,
                Age = profile.Age,
                Description = profile.Description

            };

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            UserModel user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ProfileModel? profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == user.Id);
            ProfileViewModel viewModel = new ProfileViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Adress = profile.Adress,
                City = profile.City,
                Country = profile.Country,
                ContactNumber = profile.ContactNumber,
                Age = profile.Age,
                Description = profile.Description,
                PhotoImageName = string.IsNullOrEmpty(profile.PhotoImage) ? "default.png" : profile.PhotoImage
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProfileViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == user.Id);

            profile.FirstName = model.FirstName;
            profile.LastName = model.LastName;
            profile.Adress = model.Adress;
            profile.City = model.City;
            profile.Country = model.Country;
            profile.ContactNumber = model.ContactNumber;
            profile.Age = model.Age ?? 0;
            profile.Description = model.Description;

            // cesta k priečinku pre nahrávanie
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            if (model.UseDefaultPhoto)
            {
                // vymaž starú fotku ak nie je default
                if (!string.IsNullOrEmpty(profile.PhotoImage) && profile.PhotoImage != "default.png")
                {
                    var oldPath = Path.Combine(uploadPath, profile.PhotoImage);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
                profile.PhotoImage = "default.png";
            }
            else if (model.PhotoImage != null && model.PhotoImage.Length > 0)
            {
                var extension = Path.GetExtension(model.PhotoImage.FileName).ToLower();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("PhotoImage", "Nepovolený formát obrázka. Povolené sú: JPG, PNG.");
                    model.PhotoImageName = profile.PhotoImage;
                    return View(model);
                }

                // vymaž starú fotku ak nie je default
                if (!string.IsNullOrEmpty(profile.PhotoImage) && profile.PhotoImage != "default.png")
                {
                    var oldPath = Path.Combine(uploadPath, profile.PhotoImage);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                // nahraj novú fotku
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.PhotoImage.FileName)}";
                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.PhotoImage.CopyToAsync(stream);
                }
                profile.PhotoImage = fileName;
            }

            _context.Profiles.Update(profile);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Your profile succesfully changed.";
            return RedirectToAction("Profile", "Profile");
        }
    }
}
