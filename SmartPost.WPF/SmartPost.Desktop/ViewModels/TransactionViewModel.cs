using SmartPost.Service.DTOs.Products;
using SmartPost.Service.DTOs.Transactions;
using System.Collections.ObjectModel;

namespace SmartPost.Desktop.ViewModels;

public class TransactionViewModel
{
    public ObservableCollection<TransactionDto> Transactions = new ObservableCollection<TransactionDto>();

    public TransactionViewModel()
    {
    }

    public void Add(ProductForResultDto dProduct)
    {
        Transactions.Add(new TransactionDto()
        {
            Id = dProduct.Id,
            Barcode = dProduct.BarCode,
            Name = dProduct.ProductName,
            Price = dProduct.Price,
            Quantity = 1,
            TotalPrice = dProduct.Price
        });
    }

    public void Increment(TransactionDto transaction)
    {
        foreach (var m in Transactions)
        {
            if (m.Barcode == transaction.Barcode)
            {
                m.Quantity++;
            }
        }
    }
}
