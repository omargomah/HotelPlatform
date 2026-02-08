using Base.DAL.Models.BaseModels;
using Base.Shared.Enums;
namespace Base.DAL.Models.SystemModels
{
    public class Client:BaseEntity
    {
        public string UserId { get; set; }
        public string SSN { get; set; }
        public DateTime DOB { get; set; }
        public Gender Gender { get; set; }
        public string ProfileImageLink { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new HashSet<PaymentTransaction>();
    }

}
