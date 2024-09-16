using System.Windows;

namespace Exercises.Exercise1
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
      File.Write(@"c:\temp\demo.txt", "Hallo wereld");
      TextBox1.Text = "De file is geschreven";
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }
  }
}
