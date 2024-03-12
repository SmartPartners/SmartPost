using SmartPost.Desktop.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartPost.Desktop.Pages
{
	/// <summary>
	/// Interaction logic for KunlikSotuvPage.xaml
	/// </summary>
	public class SaleItems
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

	public partial class KunlikSotuvPage : Page
	{
		public KunlikSotuvPage()
		{
			InitializeComponent();
			LoadSampleData();
		}

		private void LoadSampleData()
		{
			List<SaleItem> sales = new List<SaleItem>
	{
		new SaleItem { No = 1, TransactionNo = "ABC123456",  ProductName = "Product A", Brand = "Brand X", Category = "Category Y", Barcode = "1234567890", Price = 19.99m, Quantity = 3, TotalAmount = 59.97m, Seller = "Seller1" },
		new SaleItem { No = 2, TransactionNo = "ABC987654",  ProductName = "Product B", Brand = "Brand Y", Category = "Category Z", Barcode = "9876543210", Price = 24.99m, Quantity = 2, TotalAmount = 49.98m, Seller = "Seller2" },
        // Add more sample data as needed
    };

			// Set the sample data as the DataGrid's ItemsSource
			salesDataGrid.ItemsSource = sales;
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			// Check if there is a previous entry in the navigation history
			if (mainFrame.CanGoBack)
			{
				// If there is, go back to the previous entry
				mainFrame.GoBack();
			}
			else
			{
                // If not, create a new instance of SotuvWindow
                SaleWindow sotuvWindow = new SaleWindow();

				// Close the current window
				Window.GetWindow(this).Close();

				// Show the new SotuvWindow
				sotuvWindow.Show();
			}
		}


	}
}
