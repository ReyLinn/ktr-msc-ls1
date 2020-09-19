using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project_ktr_msc.Entities.Identity;
using Project_ktr_msc.Entities.Profiles;
using Project_ktr_msc.Models;
using Project_ktr_msc.Services;

namespace Project_ktr_msc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            ProfileViewModel viewmodel = new ProfileViewModel();
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var profile = await _userService.GetUserProfileById(_userManager.GetUserId(HttpContext.User));
                if (profile != null)
                {
                    viewmodel.IsProfileCreated = true;
                    viewmodel.Profile = profile;
                }
                else
                {
                    viewmodel.IsProfileCreated = false;
                }
            }
            return View(viewmodel);
        }
        
        public async Task<IActionResult> Library()
        {
            Library viewmodel = new Library();
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var userLibrary = await _userService.GetUserLibraryById(_userManager.GetUserId(HttpContext.User));
                if (userLibrary == null)
                {
                    userLibrary = await _userService.CreateLibraryForUser(_userManager.GetUserId(HttpContext.User));
                }
                viewmodel = userLibrary;
            }
            return View(viewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> AddProfileToUser(string name, string companyName, string emailAdress, string phoneNumber)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                UserProfile profileToAdd = new UserProfile()
                {
                    Name = name,
                    CompanyName = companyName,
                    EmailAdress = emailAdress,
                    TelephoneNumber = phoneNumber
                };
                await _userService.AddProfileToUser(_userManager.GetUserId(HttpContext.User), profileToAdd);
            }
            return RedirectToAction(nameof(Profile));
        }
        
        [HttpPost]
        public async Task<IActionResult> AddProfileToUserLibrary(string name, string companyName, string emailAdress, string phoneNumber)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                Profile profileToAdd = new Profile()
                {
                    Name = name,
                    CompanyName = companyName,
                    EmailAdress = emailAdress,
                    TelephoneNumber = phoneNumber
                };
                await _userService.AddProfileToUserLibrary(_userManager.GetUserId(HttpContext.User), profileToAdd);
            }
            return RedirectToAction(nameof(Library));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
