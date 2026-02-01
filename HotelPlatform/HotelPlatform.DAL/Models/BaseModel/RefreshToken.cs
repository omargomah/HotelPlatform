using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelPlatform.DAL.Models.BaseModel
{
    public class RefreshToken : BaseEntity
    {
        public string TokenHash { get; set; } = null!; // SHA256 hash of token value
        public string? UserId { get; set; }
        public virtual AppUser User { get; set; }
        public string? CreatedByIp { get; set; } = null!;
        public string CreatedByUserAgent { get; set; } = null!;
        public DateTime ExpiresAtUtc { get; set; }
        public DateTime? RevokedAtUtc { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByTokenHash { get; set; }
        public string? ReasonRevoked { get; set; }
        public bool IsActive => RevokedAtUtc == null && DateTime.UtcNow < ExpiresAtUtc;

        // Concurrency token (row version) to help with concurrent updates    ---> this is for handling concurrency conflicts when multiple processes try to update the same record simultaneously.
        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;
    }
}
