using Base.DAL.Models.BaseModels;

namespace Base.DAL.Models.SystemModels
{
    public class Admin : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Hotel> ManagedHotels { get; set; } = new HashSet<Hotel>();
    }

}
