using Base.DAL.Models.SystemModels;
using Base.Shared.Enums;
using Microsoft.AspNetCore.Identity;

namespace Base.DAL.Models.BaseModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public UserTypes Type { get; set; }
        public bool IsActive { get; set; } = true;
        public  Admin? Admin { get; set; }
        public Client? Client { get; set; }

    }
}
