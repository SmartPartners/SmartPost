using System.Runtime.InteropServices;
using System.Windows;
using static SmartPost.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using SmartPost.Desktop.Components;

namespace SmartPost.Desktop.Windows.Products;

/// <summary>
/// Interaction logic for AddProductStoreWindow.xaml
/// </summary>
public partial class AddProductStoreWindow : Window
{
    public AddProductStoreWindow()
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

    private void close_btn_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();

        for (int i = 0; i < 20; i++)
        {
            AddStoreProductComponent component = new AddStoreProductComponent();
            wrpProduct.Children.Add(component);
        }
    }
}
