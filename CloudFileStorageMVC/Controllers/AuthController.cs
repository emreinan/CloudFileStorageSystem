using AutoMapper;
using CloudFileStorageMVC.Dtos.Auth;
using CloudFileStorageMVC.Models;
using CloudFileStorageMVC.Services.Auth;
using CloudFileStorageMVC.Services.Token;
using Microsoft.AspNetCore.Mvc;

namespace CloudFileStorageMVC.Controllers;

public class AuthController(
    IAuthService authService,
    ITokenService tokenService,
    IMapper mapper
    ) : Controller
{
    [HttpGet("/Login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("/Login")]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
            return View(loginViewModel);

        var loginDto = mapper.Map<LoginDto>(loginViewModel);
        var token = await authService.LoginAsync(loginDto);

        if (token == null)
        {
            ModelState.AddModelError(string.Empty, "Email or password is wrong.");
            return View(loginViewModel);
        }

        tokenService.SetRefreshToken(token.RefreshToken);
        tokenService.SetAccessToken(token.AccessToken);

        TempData["SuccessMessage"] = "Login successfully!";
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("/Register")]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost("/Register")]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid)
            return View(registerViewModel);

        var registerDto = mapper.Map<RegisterDto>(registerViewModel);

        var token = await authService.RegisterAsync(registerDto);
        tokenService.SetRefreshToken(token.RefreshToken);
        tokenService.SetAccessToken(token.AccessToken);

        TempData["SuccessMessage"] = "Register successfully! Please verify Email.";
        return RedirectToAction("Login", "Auth");
    }

    [HttpGet("/Logout")]
    public IActionResult Logout()
    {
        // Çerezleri silmek için süresini geçmiş bir tarihe ayarlıyoruz. 
        tokenService.RemoveAccessToken();
        tokenService.RemoveRefreshToken();

        TempData["SuccessMessage"] = "Logout successfully!";
        return RedirectToAction("Index", "Home");
    }
}
