using System.Net.Http;
using API.Controllers;
using API.Models;
using Avalonia.Controls;
using Desktop.Services;

namespace Desktop.Pages;

public partial class MainPageWindow : Window
{
    public MainPageWindow(User user)
    {
        InitializeComponent();

        UserName.Text = user.Name;
        UserDepartment.Text = user.Department.Name;
        UserRole.Text = user.Role.Name;

        SelectMain();
    }

    private void OnMainClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SelectMain();
    }

    public void SelectProduct()
    {
        PageName.Text = "Продукция";
        CurrentPage.Content = new ProductPage();
    }

    public void SelectMain()
    {
        PageName.Text = "Главная";
        CurrentPage.Content = new MainPage();
    }

    private void OnProductClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SelectProduct();
    }
}