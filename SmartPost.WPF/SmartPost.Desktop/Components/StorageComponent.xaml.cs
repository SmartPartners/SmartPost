using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartPost.Desktop.Components;

/// <summary>
/// Interaction logic for StorageComponent.xaml
/// </summary>
public partial class StorageComponent : UserControl
{
    bool isPressed = false;
    public StorageComponent()
    {
        InitializeComponent();
    }

    private void store_Border_MouseEnter(object sender, MouseEventArgs e)
    {
        store_Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#329DFF"));
    }

    private void store_Border_MouseLeave(object sender, MouseEventArgs e)
    {
        store_Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
    }

}
