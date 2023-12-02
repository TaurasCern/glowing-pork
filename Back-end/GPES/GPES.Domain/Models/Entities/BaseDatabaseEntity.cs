using GPES.Domain.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GPES.Domain.Models.Database
{
    public class BaseDatabaseEntity : IBaseDatabaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CretedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
