using AutoMapper;
using Data;
using Data.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    /// <summary>
    /// UserController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PersonManagementContext context;
        private readonly IMapper mapper;

        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        public UserController(PersonManagementContext context, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="model">The model.</param>
        [HttpPost("Login")]
        public async Task<object> Login(LoginBindingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        return await Task.FromResult("Login successfully");
                    }
                }

                return await Task.FromResult("Invalid Email or password");
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="model">The application user dto.</param>
        [HttpPost("Register")]
        public async Task<object> Register(AddUpdateRegisterUserBindingModel model)
        {
            try
            {
                var appUser = mapper.Map<AppUser>(model);
                appUser.Created = appUser.Modified = DateTime.Now;
                var result = await userManager.CreateAsync(appUser, model.Password);

                if (result.Succeeded)
                {
                    return await Task.FromResult("User has been Registered");
                }

                return string.Join(',', result.Errors.Select(x => x.Description).ToArray());
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }
    }
}