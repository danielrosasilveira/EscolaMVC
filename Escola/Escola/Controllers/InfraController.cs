using Escola.Models.Infra;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.Controllers
{
    [Authorize]
    public class InfraController : Controller
    {

        private readonly UserManager<UsuarioDaAplicacao> _userManager;
        private readonly SignInManager<UsuarioDaAplicacao> _signInManager;
        private readonly ILogger _logger;

        public InfraController(
            UserManager<UsuarioDaAplicacao> userManager,
            SignInManager<UsuarioDaAplicacao> signInManager,
            ILogger<InfraController> logger
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Acessar (string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            if (returnUrl == null)
            {
                returnUrl = "/Home/Index";
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
