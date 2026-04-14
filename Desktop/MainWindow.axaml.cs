using System.Net.Http;
using API.Controllers;
using Avalonia.Controls;
using Desktop.Pages;
using Desktop.Services;

namespace Desktop;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void OnLoginClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var resp = await HttpService.Request<LoginResponse>(HttpMethod.Post, "Auth/login", new { Login = Login.Text, Password = Password.Text });
        if (resp.Error != null)
        {
            Error.Text = resp.Error;
            return;
        }

        HttpService.Token = resp.Data.Token;
        var newWindow = new MainPageWindow(resp.Data.User);
        newWindow.Show();

        Close();
    }
}