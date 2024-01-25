using SmartPost.DataAccess.Data;
using SmartPost.DataAccess.Interfaces.CancelOrders;
using SmartPost.Domain.Entities.CancelOrders;

namespace SmartPost.DataAccess.Repositories.CancelOrders;

/// <summary>
/// Genric repositorydagidan boshqa method qoshish kerak bo'lsa
/// Faqat shu service uchun shu yerga qoshiladi
/// 
/// yozib bolib API da Extensions folderdagi ServiceExstensionsdan royxatdan otkazish kerak
/// 
/// Keyingi bosqich DTO yaratish
/// </summary>
public class CancelOrderRepository : Repository<CancelOrder>, ICancelOrderRepository
{
    public CancelOrderRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
