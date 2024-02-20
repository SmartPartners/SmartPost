using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartPost.Desktop.Components;

/// <summary>
/// Interaction logic for CategoryComponent.xaml
/// </summary>
public partial class CategoryComponent : UserControl
{
    public CategoryComponent()
    {
        InitializeComponent();
    }

    private void category_Border_MouseEnter(object sender, MouseEventArgs e)
    {
        category_Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#329DFF"));
    }

    private void category_Border_MouseLeave(object sender, MouseEventArgs e)
    {
        category_Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
    }
}
