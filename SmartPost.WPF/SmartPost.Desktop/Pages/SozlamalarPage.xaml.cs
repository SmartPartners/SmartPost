using System.Windows.Controls;

namespace SmartPost.Desktop.Pages;

/// <summary>
/// Interaction logic for SozlamalarPage.xaml
/// </summary>
public partial class SozlamalarPage : Page
{
    public SozlamalarPage()
    {
        InitializeComponent();
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {

    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        grSettings.Visibility=System.Windows.Visibility.Collapsed;
    }

    private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
    {
        grSettings.Visibility = System.Windows.Visibility.Collapsed;
    }

    private void cancel_btn_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        grSettings.Visibility = System.Windows.Visibility.Collapsed;
    }
}
