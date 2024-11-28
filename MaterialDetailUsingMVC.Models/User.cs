

using Microsoft.AspNetCore.Identity;

namespace MaterialDetailUsingMVC.Models
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        
    }
}
