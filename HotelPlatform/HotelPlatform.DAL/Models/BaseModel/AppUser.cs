using HotelPlatform.Shared.Enums;
using Microsoft.AspNetCore.Identity;

namespace HotelPlatform.DAL.Models.BaseModel
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
