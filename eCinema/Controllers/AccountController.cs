using eCinema.Data;
using eCinema.Data.Static;
using eCinema.Data.ViewModels;
using eCinema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCinema.Controllers
{
    [Authorize(Roles = UserRoles.Admin)] // السماح فقط للمسؤولين بالوصول إلى الإدارة
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.Roles = roles.ToList(); // تحميل الأدوار
            }

            return View(users);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, loginVM.Password)))
            {
                TempData["Error"] = "Invalid email or password. Please try again.";
                return View(loginVM);
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Movies");
            }

            TempData["Error"] = "Login failed. Please try again.";
            return View(loginVM);
        }

        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterVM());

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var existingUser = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (existingUser != null)
            {
                TempData["Error"] = "This email address is already registered.";
                return View(registerVM);
            }

            var newUser = new ApplicationUser
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };

            var createUserResult = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (createUserResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return View("RegisterCompleted");
            }

            foreach (var error in createUserResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(registerVM);
        }

        [HttpPost]
        [AllowAnonymous] // ✅ السماح لأي مستخدم بتسجيل الخروج
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }

        // إدارة الأدوار
        public async Task<IActionResult> ManageRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new ManageRolesVM
            {
                UserId = user.Id,
                UserRoles = userRoles.ToList(),
                AvailableRoles = roles.Select(r => r.Name).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRoles(string userId, List<string> selectedRoles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!result.Succeeded) return View("ManageRoles");

            result = await _userManager.AddToRolesAsync(user, selectedRoles);
            if (!result.Succeeded) return View("ManageRoles");

            return RedirectToAction("Index");
        }

        // حذف المستخدم
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) return View("Index");

            return RedirectToAction("Index");
        }
    }
}
