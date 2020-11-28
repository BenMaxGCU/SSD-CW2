using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace cw2_ssd.Models
{
    public class User : IdentityUser
    {
        /// <summary>
        /// New instance of user manager
        /// </summary>
        [NotMapped] private ApplicationUserManager _userManager;

        /// <summary>
        /// The date the user was registered
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Registration Date")]
        public DateTime? DateRegistered { get; set; }

        /// <summary>
        /// Gets the current role that the user is in
        /// </summary>
        [NotMapped]
        public string CurrentRole
        {
            get
            {
                if (_userManager == null)
                {
                    _userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }

                return _userManager.GetRoles(Id).Single();
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}