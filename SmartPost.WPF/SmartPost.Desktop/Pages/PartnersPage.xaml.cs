using Microsoft.Extensions.DependencyInjection;
using SmartPost.Desktop.Windows;
using SmartPost.Service.Interfaces.Partners;
using SmartPost.Service.Services.Partners;
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
	/// Interaction logic for PartnersPage.xaml
	/// </summary>
	// Partner class definition (replace this with your actual class)
	public class Partner
	{
		public int No { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string PhoneNumber { get; set; }
		public decimal Debt { get; set; }
		public decimal LastPayment { get; set; }
		public DateTime PaymentDate { get; set; }
	}

	// In your PartnersPage.xaml.cs or PartnersPage.xaml.cs file, create sample data
	public partial class PartnersPage : Window
	{
		private readonly IServiceProvider services;
        public PartnersPage(IServiceProvider services)
        {
            InitializeComponent();
            LoadSampleData();
            this.services = services;
        }
        public void RowActionButton_Click(object sender, RoutedEventArgs e)
		{
			// Handle the button click for each row
			// You can access the data of the clicked row using the DataContext property
			var button = sender as Button;
			var dataItem = "odam"; // Replace YourDataType with the actual type of your data

			if (dataItem != null)
			{
				// Implement your logic here based on the clicked row's data
				MessageBox.Show($"Button clicked for {dataItem}");
			}
		}

		public void LoadSampleData()
		{
			List<Partner> partners = new List<Partner>
		{
			new Partner { No = 1, Name = "John", Surname = "Doe", PhoneNumber = "123-456-7890", Debt = 100.50m, LastPayment = 50.25m, PaymentDate = DateTime.Now.AddDays(-7) },
			new Partner { No = 2, Name = "Jane", Surname = "Smith", PhoneNumber = "987-654-3210", Debt = 75.20m, LastPayment = 25.10m, PaymentDate = DateTime.Now.AddDays(-15) },
            // Add more sample data as needed
        };

			partnersDataGrid.ItemsSource = partners;
		}

		public void SaleHistory_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				DailySalePage dailySalePage = new DailySalePage();
				mainFrame.NavigationService.Navigate(dailySalePage);
			}
			catch (Exception ex)
			{
				// Handle or log the exception
				MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
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
				SaleWindow sotuvWindow = new SaleWindow(services);

				// Close the current window
				Window.GetWindow(this).Close();

				// Show the new SotuvWindow
				//sotuvWindow.Show();
			}
		}

        private void btnQoshish_Click(object sender, RoutedEventArgs e)
        {

            PartnerRegisterPage partnerRegisterPage = new PartnerRegisterPage(services);
			mainFrame.NavigationService.Navigate(partnerRegisterPage);
        }
    }

}
