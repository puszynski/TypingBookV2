using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TypingBook.TagHelpers
{
    [HtmlTargetElement("async-roleusers")] // looking for html tags => <roleusers>
    public class AsyncRoleUsersTagHelper : TagHelper 
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<IdentityUser> _userManager;

        public AsyncRoleUsersTagHelper(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HtmlAttributeName("i-role")] // Assign data from view
        public string Role { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> names = new List<string>();
            IdentityRole role = await _roleManager.FindByIdAsync(Role);
            if (role != null)
            {
                foreach (var user in _userManager.Users)
                {
                    if (user != null && await _userManager.IsInRoleAsync(user, role.Name))
                        names.Add(user.UserName);
                }
            }
            output.Content.SetContent(names.Count == 0 ? "No Users" : string.Join(", ", names));
        }
    }
}
