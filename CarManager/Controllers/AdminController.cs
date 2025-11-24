using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarManager.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> EditRole(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return NotFound();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var roles = _roleManager.Roles.Select(r => r.Name).ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

            ViewBag.Roles = roles;
            ViewBag.UserRoles = userRoles;
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(string userId, string role)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
                return NotFound();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);

            if (currentRoles.Contains("admin") && role != "admin")
            {
                var adminCount = _userManager.Users.Count(u => _userManager.IsInRoleAsync(u, "admin").Result);
                if (adminCount <= 1)
                {
                    ModelState.AddModelError("", "Cannot remove the last admin from admin role.");
                    var rolesList = _roleManager.Roles.Select(r => r.Name).ToList();
                    ViewBag.Roles = rolesList;
                    ViewBag.UserRoles = currentRoles;
                    return View(user);
                }
            }

            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, role);

            return RedirectToAction(nameof(Index));
        }
    }
}