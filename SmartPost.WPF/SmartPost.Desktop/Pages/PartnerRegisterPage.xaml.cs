using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using SmartPost.Service.DTOs.Brands;
using SmartPost.Service.DTOs.Partners;
using SmartPost.Service.Interfaces.Partners;

namespace SmartPost.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for PartnerRegisterPage.xaml
    /// </summary>
    public partial class PartnerRegisterPage : Page
    {
        private readonly IPartnerService partnerService;
        
        public PartnerRegisterPage(IServiceProvider services)
        {
            InitializeComponent();
            this.partnerService = services.GetRequiredService<IPartnerService>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PartnerForCreationDto partnerForCreationDto = new PartnerForCreationDto();

            partnerForCreationDto.FirstName = NameTextBox.Text;
            partnerForCreationDto.LastName = SurnameTextBox.Text;
            partnerForCreationDto.PhoneNumber = PhoneTextBox.Text;

            var result = await this.partnerService.CreateAsync(partnerForCreationDto);

            if (result is not null)
                MessageBox.Show("Hamkor qo'shildi.");
            else
                MessageBox.Show("Xatolik yuza keldi.");
        }
    }
}
