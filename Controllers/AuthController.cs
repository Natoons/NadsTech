using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NadsTech.Models;

namespace NadsTech.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password, bool rememberMe = false)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ModelState.AddModelError("", "Email et mot de passe requis");
            return View();
        }

        var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
        
        if (result.Succeeded)
        {
            return Redirect("/");
        }
        else if (result.IsLockedOut)
        {
            ModelState.AddModelError("", "Compte verrouillé. Veuillez réessayer plus tard.");
        }
        else
        {
            ModelState.AddModelError("", "Email ou mot de passe incorrect.");
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
} 