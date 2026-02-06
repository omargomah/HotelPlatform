using Base.DAL.Models.BaseModels;
using Base.Shared.Enums;

namespace Base.DAL.Models.SystemModels
{
    public class SeasonalPricing:BaseEntity
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public SeasonalPricingStatus SeasonalPricingStatus { get; set; }
        public double IncreasingPercentage { get; set; }
        public ICollection<Hotel> Hotels { get; set; } = new HashSet<Hotel>();
    }

}
