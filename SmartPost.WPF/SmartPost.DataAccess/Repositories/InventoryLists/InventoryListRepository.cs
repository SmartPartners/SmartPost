using SmartPost.DataAccess.Data;
using SmartPost.DataAccess.Interfaces.InventoryLists;
using SmartPost.Domain.Entities.InventoryLists;

namespace SmartPost.DataAccess.Repositories.InventoryLists;

/// <summary>
/// Genric repositorydagidan boshqa method qoshish kerak bo'lsa
/// Faqat shu service uchun shu yerga qoshiladi
/// 
/// yozib bolib API da Extensions folderdagi ServiceExstensionsdan royxatdan otkazish kerak
/// 
/// Keyingi bosqich DTO yaratish
/// </summary>
public class InventoryListRepository : Repository<InventoryList>, IInventoryListRepository
{
    public InventoryListRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
