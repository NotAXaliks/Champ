using Avalonia.Controls;

namespace Desktop.Pages
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OrdersPage(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ((MainPageWindow)VisualRoot).SelectProduct();
        }
    }
}