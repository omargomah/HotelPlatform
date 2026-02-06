using Base.DAL.Models.BaseModels;

namespace Base.DAL.Models.SystemModels
{
    public class UnitPhoto : BaseEntity
    {
        public string UnitId { get; set; }
        public string PhotoUrl { get; set; }
        public Unit Unit { get; set; }
    }

}
