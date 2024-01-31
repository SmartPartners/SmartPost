using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Interfaces.Cards;
using SmartPost.DataAccess.Interfaces.StockProducts;
using SmartPost.Domain.Entities.Cards;
using SmartPost.Domain.Entities.StokProducts;
using SmartPost.Domain.Entities.StorageProducts;
using SmartPost.Service.Commons;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.DTOs.Cards;
using SmartPost.Service.Interfaces.Cards;

namespace SmartPost.Service.Services.Cards;

public class CardManagementService : ICardManagementService
{
    private readonly IStockProductRepository _stockProductRepository;
    private readonly ICardRepository _cardRepository;
    private readonly IMapper _mapper;

    public CardManagementService(IStockProductRepository stockProductRepository, ICardRepository cardRepository, IMapper mapper = null)
    {
        _stockProductRepository = stockProductRepository;
        _cardRepository = cardRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// StokProdutni ichidan sotilganlarni cardga olib o'tish
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userId"></param>
    /// <param name="quantityToMove"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<bool> MoveProductToStockAsync(long id, long userId, decimal quantityToMove)
    {
        var insufficientProduct = await _stockProductRepository.SelectAll()
            .Where(q => q.Id == id && q.Quantity < quantityToMove)
            .FirstOrDefaultAsync();

        if (insufficientProduct != null)
            throw new CustomException(400, $"Magazinda buncha mahsulot mavjud emas.\nHozirda {insufficientProduct.Quantity} ta mahsulot bor.");

        if (quantityToMove <= 0)
            quantityToMove = 1;

        var product = await _stockProductRepository.SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (product == null)
            throw new CustomException(404, "Mahsulot topilmadi.");
        else if (product.Quantity == 0)
            throw new CustomException(404, "Hozirda bu mahsulotimiz tugagan.");

        //var cardList = new List<Card>();
        var identity = IdentitySingleton.GetInstance();

        var transactionNumber = GenerateTransactionNumber();

        var card = new Card
        {
            UserId = userId,
            PCode = product.PCode,
            BarCode = product.BarCode,
            ProductName = product.ProductName,
            Price = product.Price,
            Quantity = quantityToMove,
            Status = "Kutilmoqda",
            TransNo = transactionNumber,
            CreatedAt = DateTime.UtcNow
        };
        card.TotalPrice = card.Price * card.Quantity;

        identity.cardList.Add(card);

        //cardList.Add(card);

        await UpdateCardAsync(new List<Card> { card }, product, quantityToMove);

        return true;
    }

    public async Task<bool> UpdateCardAsync(List<Card> cards, StokProduct product, decimal quantityToMove)
    {
        /*var existingCard = await _cardRepository.SelectAll()
            .Where(p => p.TransNo == cards.TransNo)
            .FirstOrDefaultAsync();*/

        /* if (existingCard != null)
         {
             existingCard.Quantity += quantityToMove;
             existingCard.TotalPrice = existingCard.Price * existingCard.Quantity;
             await _cardRepository.UpdateAsync(existingCard);

             product.Quantity -= quantityToMove;
             await _stockProductRepository.UpdateAsync(product);
         }
         else
         {*/
        foreach (var card in cards)
        {
            await _cardRepository.InsertAsync(card);

            card.Status = "Sotildi";
            await _cardRepository.UpdateAsync(card);

            product.Quantity -= quantityToMove;
            await _stockProductRepository.UpdateAsync(product);
        }
        //}
        var identity = IdentitySingleton.GetInstance();

        identity.cardList.Clear();

        return true;
    }


    private static int lastTransactionNumberSuffix = 1000;

    public string GenerateTransactionNumber()
    {
        // Use current date in "yyyyMMdd" format
        string currentDate = DateTime.Now.ToString("yyyyMMdd");

        // Append the lastTransactionNumberSuffix to the currentDate
        string transactionNumber = currentDate + (lastTransactionNumberSuffix++).ToString();

        // Reset the suffix to 1001 for the next day
        if (lastTransactionNumberSuffix > 1001)
        {
            lastTransactionNumberSuffix = 1001;
        }

        return transactionNumber;
    }


    public async Task<bool> CalculeteDiscountPercentageAsync(long id, short discountPercentage)
    {
        var card = await _cardRepository.SelectAll()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (card is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        decimal discountAmount = (card.TotalPrice / 100) * discountPercentage;

        decimal discountedTotalPrice = card.TotalPrice - discountAmount;

        card.TotalPrice = discountedTotalPrice;
        card.DiscPercent = discountAmount;

        await _cardRepository.UpdateAsync(card);

        return true;
    }

    public async Task<IEnumerable<CardForResultDto>> GetByBarCodeAsync(string barCode)
    {
        var codes = await _cardRepository.SelectAll()
            .Where(c => c.BarCode == barCode)
            .ToListAsync();
        if (codes is null)
            throw new CustomException(404, "Mahsulot topilmadi.");


        return _mapper.Map<IEnumerable<CardForResultDto>>(codes);
    }

    public async Task<IEnumerable<CardForResultDto>> SvetUchgandaAsync(string status)
    {
        var result = await _cardRepository.SelectAll()
            .Where(c => c.Status.ToLower() == status.ToLower())
            .AsNoTracking()
            .ToListAsync();
        if (result is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        return _mapper.Map<IEnumerable<CardForResultDto>>(result);
    }


    #region UI qismida yoziladi
    // Assuming you have a class representing your main window (MainWindow.xaml.cs)
    /*public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Wire up the button click event handler
            updateButton.Click += async (sender, e) =>
            {
                // Assuming you have values for card, product, and quantityToMove
                Card card = *//* your card object *//*;
                StockProduct product = *//* your product object *//*;
                decimal quantityToMove = *//* your quantityToMove value *//*;

                // Call the asynchronous method
                await UpdateCardAsync(card, product, quantityToMove);

                // Optionally, you can perform additional actions after the method call
                // For example, display a success message, update UI, etc.
                MessageBox.Show("Card updated successfully!");
            };
        }

        // Rest of your MainWindow class...
    }*/
    #endregion

    /// <summary>
    /// sotilgan mahsulotlarni chiqarib beradi
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public async Task<IEnumerable<CardForResultDto>> RetrieveAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate)
    {
        if (userId != null)
        {
            var product = await _cardRepository.SelectAll()
                .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate && p.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        var products = await _cardRepository.SelectAll()
            .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<CardForResultDto>>(products);
    }

    /// <summary>
    /// Maximal sotilgan mahsulotlarni qaytaradi
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<CardForResultDto>> RetrieveAllWithMaxSaledAsync(int takeMax)
    {
        var result = await _cardRepository.SelectAll()
            .Where(c => c.Status == "Sotildi")
            .OrderByDescending(c => c.Quantity)
            .Take(takeMax)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<CardForResultDto>>(result);
    }

}
