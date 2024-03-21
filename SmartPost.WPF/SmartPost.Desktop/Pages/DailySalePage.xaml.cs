using System.Windows;
using System.Windows.Controls;

namespace SmartPost.Desktop.Pages;

/// <summary>
/// Interaction logic for DailySalePage.xaml
/// </summary>
/// 

public class SaleItem
{
    public int No { get; set; }
    public string TransactionNo { get; set; }
    public string PartnerName { get; set; }
    public string ProductName { get; set; }
    public string Brand { get; set; }
    public string Category { get; set; }
    public string Barcode { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
    public string Seller { get; set; }
}

public partial class DailySalePage : Page
{
    public DailySalePage()
    {
        InitializeComponent();
        LoadSampleData();
    }

    private void LoadSampleData()
    {
        List<SaleItem> sales = new List<SaleItem>
{
    new SaleItem { No = 1, TransactionNo = "ABC123456", PartnerName = "John Doe", ProductName = "Product A", Brand = "Brand X", Category = "Category Y", Barcode = "1234567890", Price = 19.99m, Quantity = 3, TotalAmount = 59.97m, Seller = "Seller1" },
    new SaleItem { No = 2, TransactionNo = "ABC987654", PartnerName = "Jane Smith", ProductName = "Product B", Brand = "Brand Y", Category = "Category Z", Barcode = "9876543210", Price = 24.99m, Quantity = 2, TotalAmount = 49.98m, Seller = "Seller2" },
        // Add more sample data as needed
    };

        // Set the sample data as the DataGrid's ItemsSource
        salesDataGrid.ItemsSource = sales;
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {

        // Navigate back to PartnersPage
        if (mainFrame.CanGoBack)
        {
            mainFrame.GoBack();
        }
        else
        {
            //// If the PartnersPage is not in the navigation history, create a new instance and show it
            //PartnersPage partnersPage = new PartnersPage();
            //partnersPage.Show();
        }

        // Close the current window
        Window.GetWindow(this).Close();
    }

    private void RowActionButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Qaytarish");
    }


}
