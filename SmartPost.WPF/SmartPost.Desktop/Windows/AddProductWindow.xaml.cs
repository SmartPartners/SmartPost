using System.Runtime.InteropServices;
using System.Windows;
using static SmartPost.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;

namespace SmartPost.Desktop.Windows;

/// <summary>
/// Interaction logic for AddProductWindow.xaml
/// </summary>
public partial class AddProductWindow : Window
{
    public AddProductWindow()
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

    private void close_Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
    }
}
