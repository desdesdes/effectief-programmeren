using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Demos.Parallel;

/// <summary>
/// Interaction logic for WpfClient.xaml
/// </summary>
public partial class WpfClient : Window
{
  Printer _printer = new Printer();

  public WpfClient()
  {
    Dispatcher.UnhandledException += (s, e) =>
    {
      MessageBox.Show(e.Exception.ToString(), "Er is een fout opgetreden", MessageBoxButton.OK, MessageBoxImage.Error);
      e.Handled = true;
    };

    InitializeComponent();
  }

  private async void StartButton_Click(object sender, RoutedEventArgs e)
  {
    _printer.Reset();
    TextBox1.Text = _printer.PrintStatus.ToString();
    await _printer.PrintAsync("Hallo Wereld");
    TextBox1.Text = _printer.PrintStatus.ToString();
  }

  private async void ThrowException()
  {
    throw new Exception("Test exception");
  }


  private void CloseButton_Click(object sender, RoutedEventArgs e)
  {
    Close();
  }
}
