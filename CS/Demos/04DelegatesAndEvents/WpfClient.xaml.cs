using System.Windows;

namespace Demos.DelegatesAndEvents;

/// <summary>
/// Interaction logic for WpfClient.xaml
/// </summary>
public partial class WpfClient : Window
{
  public WpfClient()
  {
    InitializeComponent();
  }

  private void StartButton_Click(object sender, RoutedEventArgs e)
  {
    //Printer.PrintFinished += OnPrintFinished;
    Printer.QueuePrintJob("Hallo world!");
  }

  //private void OnPrintFinished(object? sender, PrintFinishedEventArgs e)
  //{
  //  TextBox1.Text = "WpfClient: OnPrintFinished called";
  //}

  private void CloseButton_Click(object sender, RoutedEventArgs e)
  {
    Close();
  }
}
