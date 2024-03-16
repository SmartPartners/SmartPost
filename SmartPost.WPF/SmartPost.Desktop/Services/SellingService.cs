using Newtonsoft.Json;
using SmartPost.Service.DTOs.Receipts;
using SmartPost.Service.DTOs.Transactions;
using System.Net.Http;
using System.Text;

namespace SmartPost.Desktop.Services;

public class SellingService : IDisposable
{
    HttpClient _client;
    TokenService tokenService;
    public SellingService()
    {
        _client = new HttpClient();
        //_client.BaseAddress = new Uri(Constants.BASE_URL + "receipt/");
        tokenService = new TokenService();
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenService.GetToken());
    }

    //public async Task<ReceiptDto> AddAsync(AddReceiptDto dto)
    //{
    //    var items = dto.Transactions.Select(i => new TransactionAsReceiptItemDto()
    //    {
    //        ProductId = i.Id,
    //        Quantity = i.Quantity,
    //        TotalPrice = i.Quantity * i.Price,
    //        ProductName = i.Name,
    //        ProductPrice = i.Price
    //    });
    //    var json = JsonConvert.SerializeObject(items);
    //    var content = new StringContent(json, Encoding.UTF8, "application/json");

    //    var totalPrice = items.Sum(i => i.TotalPrice);

    //    using var httpClient = new HttpClient();
    //    using var response = await httpClient.PostAsync(
    //        $"{Constants.BASE_URL}Receipt?TotalPrice={totalPrice}&Discount={dto.Discount}&PaidCash={dto.PaidCash}&PaidCard={dto.PaidCard}&HasLoan={dto.HasLoan}&SellerId={tokenService.GetUserId()}",
    //        content);

    //    if (response.IsSuccessStatusCode)
    //    {
    //        var responseContent = await response.Content.ReadAsStringAsync();
    //        var receipt = JsonConvert.DeserializeObject<ReceiptDto>(responseContent);

    //        return receipt;
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}

    public AddReceiptDto CreateEmptyReceipt()
        => new AddReceiptDto();

    public void Dispose()
            => GC.SuppressFinalize(this);
}
