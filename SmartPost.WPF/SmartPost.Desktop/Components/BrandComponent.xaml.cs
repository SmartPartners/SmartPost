using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartPost.Desktop.Components;

/// <summary>
/// Interaction logic for BrandComponent.xaml
/// </summary>
public partial class BrandComponent : UserControl
{
    public BrandComponent()
    {
        InitializeComponent();
    }

    private void store_Border_MouseEnter(object sender, MouseEventArgs e)
    {
        brand_Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#329DFF"));
    }

    private void store_Border_MouseLeave(object sender, MouseEventArgs e)
    {
        brand_Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("transparent"));
    }
}
