using SmartPost.DataAccess.Data;
using SmartPost.DataAccess.Interfaces.Cards;
using SmartPost.Domain.Entities.Cards;

namespace SmartPost.DataAccess.Repositories.Cards;

/// <summary>
/// Genric repositorydagidan boshqa method qoshish kerak bo'lsa
/// Faqat shu service uchun shu yerga qoshiladi
/// 
/// yozib bolib API da Extensions folderdagi ServiceExstensionsdan royxatdan otkazish kerak
/// 
/// Keyingi bosqich DTO yaratish
/// </summary>
public class CardRepository : Repository<Card>, ICardRepository
{
    public CardRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
