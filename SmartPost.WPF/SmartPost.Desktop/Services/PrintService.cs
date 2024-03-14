using ESC_POS_USB_NET.Printer;
using System.IO;
using System.Text;

namespace SmartPost.Desktop.Services;

public class PrintService : IDisposable
{

    private readonly string PRINTER_NAME = Path.Combine(Path.GetTempPath(),
                                                        "40c765b3-3e9c-4dd7-b592-f53c83c0bd4a.txt");
    public string printerName { get; set; } = string.Empty;
    Printer? printer;
    public PrintService()
    {
        //printerName = GetSavedPrinterName();
    }

    public void Print(AddReceiptDto receipt, List<TransactionDto> transactions, int receiptId)
    {
        //using var tokenService = new TokenService();
        printer = new Printer(printerName, "UTF-8");
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        printer.Separator();
        //Bitmap image = new Bitmap(Bitmap.FromFile("logo.png"));
        //printer.Image(image);
        printer.AlignCenter();
        printer.DoubleWidth2();
        printer.Append("\"MDevs Group LLC\"");
        printer.Separator();
        printer.Append("\n");

        printer.AlignLeft();
        printer.Append("t/r Nomi                Miqdori     Jami narxi");
        printer.Separator();

        int tr = 1;
        decimal sum = 0;
        printer.Append("\n");
        foreach (var item in transactions)
        {
            string text = $"{tr}.  {item.Name}";
            int strLength = 32 - text.Length;
            for (int i = 1; i <= strLength; i++)
            {
                text += " ";
            }
            string temp = $"{item.Quantity}*{item.Price}";
            text += temp;
            strLength = 16 - temp.Length;
            for (int i = 0; i < strLength; i++)
            {
                text += " ";
            }
            text += item.TotalPrice.ToString();
            printer.CondensedMode(text);
            printer.Append("\n");
            tr++;
            sum += item.TotalPrice;
        }

        printer.Separator();
        printer.AlignLeft();
        printer.Append("\n");
        printer.Append($"Naqd:                         {receipt.PaidCash} so'm");
        printer.Append($"Plastik:                      {receipt.PaidCard} so'm");
        printer.Append($"Chegirma:                     {receipt.Discount} so'm");
        printer.Append($"Jami summa:                   {sum} so'm");
        printer.Append("\n");

        printer.Separator();
        printer.AlignLeft();
        printer.Append("\n");
        //printer.Append($"Sotuvchi:               {tokenService.GetFullName()}");
        printer.AlignRight();
        printer.Append("\n");
        printer.AlignLeft();
        printer.Append("Sana:                      " + DateTime.Now.ToString());


        string barcode = (100000000000 + receiptId).ToString();


        printer.Append("\n");
        printer.AlignCenter();
        printer.Code128(barcode);
        printer.Append("\n");

        printer.AlignCenter();
        printer.BoldMode("Xaridingiz uchun tashakkur!");

        printer.Append("\n");
        printer.Separator();


        printer.FullPaperCut();
        printer.PrintDocument();
    }

    public void Test()
    {
        printer = new Printer(printerName, "UTF-8");
        Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        printer.Separator();
        printer.AlignCenter();
        printer.Append("\n");
        printer.BoldMode("Test printer!");
        printer.Append("\n");
        printer.Separator();
        printer.FullPaperCut();
        printer.PrintDocument();
    }
    public string GetSavedPrinterName()
    {
        try
        {
            StreamReader streamReader = new StreamReader(PRINTER_NAME);
            string res = streamReader.ReadToEnd();
            streamReader.Close();
            return res;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    public void SavePrinter(string name)
    {
        StreamWriter sw = new StreamWriter(PRINTER_NAME);
        sw.Write(name);
        sw.Flush();
        sw.Close();
    }

    public List<string> ConnectedPrinters()
    {
        List<string> printers = new List<string>();
        foreach (var print in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
        {
            printers.Add(print.ToString());
        }

        return printers;
    }

    public void Dispose()
         => GC.SuppressFinalize(this);
}
