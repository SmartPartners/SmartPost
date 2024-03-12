using SmartPost.Domain.Entities.Cards;

namespace SmartPost.Service.Commons;

public class IdentitySingleton
{
    public static IdentitySingleton _identitySingleton;
    public IList<Card> cardList = new List<Card>();

    private IdentitySingleton()
    {

    }
    public static IdentitySingleton GetInstance()
    {
        if (_identitySingleton == null)
        {
            _identitySingleton = new IdentitySingleton();
        }
        return _identitySingleton;
    }
}
