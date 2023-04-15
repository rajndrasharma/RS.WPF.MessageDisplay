using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace RS.WPF.MessageDisplay;

/// <summary>
/// Interaction logic for UserControl1.xaml
/// </summary>
public partial class MessageDisplayControl : UserControl
{
    public MessageDisplayControl()
    {
        InitializeComponent();
    }

    private Exception Ex { get; set; }
    private string Message { get; set; }

    public void DisplayErrorMessage(string Message, Exception ex=null)
    {
        this.Message = Message;
        this.Ex = ex;
        this.StatusMessage.Content = Message;
        this.StatusMessage.Foreground = Brushes.Maroon;
    }
    public void DisplaySuccessMessage(string Message)
    {
        this.Message = Message;
        this.Ex = null;
        this.StatusMessage.Content = Message;
        this.StatusMessage.Foreground = Brushes.DarkGreen;

        var timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(4.5)
        };

        timer.Tick +=
            (o, e) =>
            {
                timer.Stop();
                StatusMessage.Content = string.Empty;
            };
        timer.Start();
    }
    public void DisplayAnimatedMessage(string Message)
    {
        StatusMessage.Content = Message;

        var animation = new ColorAnimation
        {
            From = Colors.Red,
            To = Colors.SaddleBrown,
            AutoReverse = true,
            Duration = TimeSpan.FromSeconds(5),
            RepeatBehavior= new RepeatBehavior(TimeSpan.FromSeconds(20))
        };

        StatusMessage.Foreground = new SolidColorBrush();
        StatusMessage.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, animation);
    }
    public void Clear()
    {
        this.StatusMessage.Content = "";
        this.Ex = null;
        this.Message = "";
        this.StatusMessage.Background = Brushes.Transparent;
    }

    private void StatusMessage_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var MsgWindow = new DisplayMessageDetails(this.Message, Ex);
        MsgWindow.ShowDialog();
    }
}