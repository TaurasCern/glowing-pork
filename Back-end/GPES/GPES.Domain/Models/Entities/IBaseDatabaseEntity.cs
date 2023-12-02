namespace GPES.Domain.Models.Entities
{
    public interface IBaseDatabaseEntity
    {
        Guid Id { get; set; }
        DateTime CretedAt { get; set; }
        bool IsDeleted { get; set; }
    }
}
