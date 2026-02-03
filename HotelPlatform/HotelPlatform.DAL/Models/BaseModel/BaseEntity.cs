using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelPlatform.DAL.Models.BaseModel
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string? CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedById { get; set; }
        public DateTime UndatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
