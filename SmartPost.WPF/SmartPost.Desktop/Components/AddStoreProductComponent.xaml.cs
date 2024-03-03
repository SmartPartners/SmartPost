using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartPost.Desktop.Components;

/// <summary>
/// Interaction logic for AddStoreProductComponent.xaml
/// </summary>
public partial class AddStoreProductComponent : UserControl
{
    public AddStoreProductComponent()
    {
        InitializeComponent();
    }

    private void Border_MouseEnter(object sender, MouseEventArgs e)
    {
        product_border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#329DFF"));
    }

    private void Border_MouseLeave(object sender, MouseEventArgs e)
    {
        product_border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
    }

    private void save_in_store_Click(object sender, System.Windows.RoutedEventArgs e)
    {

    }

    private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
    }
}
