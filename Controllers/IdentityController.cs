using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc_IdentityApp.Controllers
{
    public class IdentityController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public IdentityController(
            SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IEnumerable<string> GetIdentities( [FromServices] IHttpContextAccessor httpContextAccessor)       
            => httpContextAccessor.HttpContext.User.Identities.Select(i => i.Name==null? "": (string)(i.Name)).ToList<string>();


        public async Task<bool> Signin(string username, string password)
        {
            await Task.CompletedTask;

            var signInResult = await signInManager.PasswordSignInAsync(username, password, true, false);
            return signInResult.Succeeded;
        }

        public async Task<object> Signup(string username, string password, string confirmation)
        {
            var user = new IdentityUser { UserName = username, Email = username };
            await Task.CompletedTask;
            var createUserResult = await userManager.CreateAsync(user, password );
            if (createUserResult.Succeeded == false)
            {

                return createUserResult;
            }
            var signInResult = await signInManager.PasswordSignInAsync(username, password, true, false);
            return signInResult;
        }
    }
}
