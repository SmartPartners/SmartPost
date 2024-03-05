using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using static SmartPost.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;

namespace SmartPost.Desktop.Windows.Brands;

/// <summary>
/// Interaction logic for BrandCreateWindow.xaml
/// </summary>
public partial class BrandCreateWindow : Window
{
    public BrandCreateWindow()
    {
        InitializeComponent();
    }

    ///////////////////////////////////////////////////////////////////////////////////////

    [DllImport("user32.dll")]
    internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    internal void EnableBlur()
    {
        var windowHelper = new WindowInteropHelper(this);

        var accent = new AccentPolicy();
        accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

        var accentStructSize = Marshal.SizeOf(accent);

        var accentPtr = Marshal.AllocHGlobal(accentStructSize);
        Marshal.StructureToPtr(accent, accentPtr, false);

        var data = new WindowCompositionAttributeData();
        data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
        data.SizeOfData = accentStructSize;
        data.Data = accentPtr;

        SetWindowCompositionAttribute(windowHelper.Handle, ref data);

        Marshal.FreeHGlobal(accentPtr);
    }

    ///////////////////////////////////////////////////////////////////////////////////////


    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
    }

    private void product_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
    }

    private void create_button_Click(object sender, RoutedEventArgs e)
    {

    }

    private void close_Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
