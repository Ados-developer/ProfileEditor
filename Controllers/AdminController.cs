using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileEditor.Data;
using ProfileEditor.Models;

namespace ProfileEditor.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly ProfileEditorDbContext _context;
        public AdminController(UserManager<UserModel> userManager, ProfileEditorDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> GetRecords()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            var role = await _userManager.GetRolesAsync(user);
            var currentUserId = _userManager.GetUserId(User);

            // Zamedzenie vymazania seba samého
            if (user.Id == currentUserId)
            {
                TempData["ErrorMessage"] = "You cannot delete yourself.";
                return RedirectToAction("GetRecords","Admin");
            }

            // Zamedzenie vymazania iného admina, ak je posledný
            if (role.Contains("Admin"))
            {
                var adminCount = 0;
                var allUsers = await _userManager.Users.ToListAsync();
                foreach (var u in allUsers)
                {
                    var r = await _userManager.GetRolesAsync(u);
                    if (r.Contains("Admin")) adminCount++;
                }

                if (adminCount <= 1)
                {
                    TempData["ErrorMessage"] = "Cannot delete the last admin user.";
                    return RedirectToAction("GetRecords", "Admin");
                }
            }
            // Najprv zmaž súvisiaci profil (ak existuje)
            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (profile != null)
            {
                _context.Profiles.Remove(profile);
                await _context.SaveChangesAsync();
            }

            // Potom zmaž používateľa cez UserManager (správne hashy, identity cleanup)
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = string.Join("; ", result.Errors.Select(e => e.Description));
                return RedirectToAction("GetRecords", "Admin");
            }

            TempData["SuccessMessage"] = "User deleted successfully.";
            return RedirectToAction("GetRecords", "Admin");
        }
    }
}
