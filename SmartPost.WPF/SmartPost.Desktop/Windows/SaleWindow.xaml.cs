using SmartPost.DataAccess.Data;
using SmartPost.Desktop.Pages;
using SmartPost.Domain.Entities.StorageProducts;
using SmartPost.Service.Interfaces.Cards;
using SmartPost.Service.Services.Cards;
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
using System.Windows.Shapes;

namespace SmartPost.Desktop.Windows
{
    /// <summary>
    /// Interaction logic for SotuvWindow.xaml
    /// </summary>
    /// // TransactionItem class representing the data structure
    public class TransactionItem
    {
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }
    public partial class SaleWindow : Window
    {
        CardManagementService cardManagement = new CardManagementService();
        private AppDbContext dbContext;
        public SaleWindow()
        {
            InitializeComponent();
            PopulateSampleData();
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Transaction_No.Text = cardManagement.GenerateTransactionNumber();
            // Add your initialization code here, if needed
        }

        private void naqd_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }

        public void Partner_btn_click(object sender, RoutedEventArgs e)
        {
            try
            {
                PartnersPage partnersPage = new PartnersPage();
                partnersPage.Owner = this;  // Set the owner window to enable proper modality
                partnersPage.ShowDialog();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Close_btn_Click(object sender, RoutedEventArgs e)
        {
            // Assuming this code is inside a Window class
            this.Close();
        }

        private void PopulateSampleData()
        {
            // Creating a list of TransactionItem objects
            List<TransactionItem> sampleData = new List<TransactionItem>
        {
            new TransactionItem { Barcode = "123456", Name = "Product 1", Color = "Red", Size = "M", Price = 15, Quantity = 2 },
            new TransactionItem { Barcode = "789012", Name = "Product 2", Color = "Blue", Size = "L", Price = 24, Quantity = 1 },
            // Add more sample data as needed
        };

            // Set the sample data as the ItemsSource for the DataGrid
            DataGrid.ItemsSource = sampleData;
        }
        private void Kunlik_sotuv_btn(object sender, RoutedEventArgs e)
        {
            try
            {
                KunlikSotuvPage kunlikSotuvPage = new KunlikSotuvPage();
                mainFrame.NavigationService.Navigate(kunlikSotuvPage);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void DiscountButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Check if the TextBox for discount is focused
            if (chegirma.IsFocused)
            {
                // Append the clicked button content to the discount TextBox
                chegirma.Text += clickedButton.Content.ToString();
            }
            else
            {
                // Update the quantity of the selected product in the data grid
                if (DataGrid.SelectedItem != null)
                {
                    // Assuming your Product class has a Quantity property
                    // Adjust this based on your actual model
                    var selectedProduct = (Product)DataGrid.SelectedItem;

                    // Parse the content of the clicked button to an integer
                    if (int.TryParse(clickedButton.Content.ToString(), out int quantityToAdd))
                    {
                        // Update the quantity of the selected product
                        selectedProduct.Quantity += quantityToAdd;

                        // Update the data grid (refresh the view)
                        DataGrid.Items.Refresh();
                    }
                    else
                    {
                        // Handle parsing error if needed
                    }
                }
                else
                {
                    // Handle the case when no product is selected in the data grid
                    // You might want to display a message or handle it accordingly
                }
            }
        }

        private void Button_Sotish(object sender, RoutedEventArgs e)
        {
            //foreach (var item in DataGrid.Items)
            //{
            //	if (item is Product product)
            //	{
            //		string barcode = product.BarCode;
            //		var quantityToMove = product.Quantity;
            //		if (dbContext != null && dbContext.Database.CanConnect())
            //		{
            //			// Use LINQ to find the Id based on the barcode
            //			var productId = dbContext.Products
            //				.Where(p => p.BarCode == barcode)
            //				.Select(p => p.Id)
            //				.FirstOrDefault();
            //		}
            //	}

            //}
        }
    }
}
