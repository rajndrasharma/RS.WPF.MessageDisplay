using System;
using System.Windows;

namespace RS.WPF.MessageDisplay;

/// <summary>
/// Interaction logic for DisplayMessageDetails.xaml
/// </summary>
public partial class DisplayMessageDetails : Window
{
    public DisplayMessageDetails(string Message,Exception ex=null)
    {
        InitializeComponent();
        this.TxtMessage.Text = Message;
        if (ex!=null)
        {
            this.GrpInnerException.Visibility = Visibility.Visible;
            this.TxtInnerException.Text = ex.ToString();
        }
        else
        {
            this.GrpInnerException.Visibility = Visibility.Collapsed;
        }
    }
    private void BtnCopyMessage_Click(object sender, RoutedEventArgs e)
    {
        Clipboard.Clear();
        string InnerException = string.IsNullOrWhiteSpace(TxtInnerException.Text) ? "" : $"Inner Exception:\n {TxtInnerException.Text}";
        Clipboard.SetText($"Message:-\n" +
            $"{TxtMessage.Text}" +
            $"\n" +
            $"\n{InnerException}");
    }
    private void BtnClose_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
