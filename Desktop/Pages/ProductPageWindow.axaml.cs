using System.Collections.ObjectModel;
using System.Net.Http;
using API.Models;
using Avalonia.Controls;
using Desktop.Services;

namespace Desktop.Pages;

public partial class ProductPageWindow : Window
{
    private readonly ObservableCollection<Recipe> _recipes = new();

    public ProductPageWindow(ProductResult productResult)
    {
        InitializeComponent();

        RecipesGrid.ItemsSource = _recipes;

        Update(productResult.Product.Id);
    }

    public async void Update(int id)
    {
        var resp = await HttpService.Request<Product>(HttpMethod.Get, $"Product/{id}");

        if (resp?.Data != null)
        {
            Name.Text = $"Имя: {resp.Data.Name}";
            Code.Text = $"Код: {resp.Data.Code}";
            Form.Text = $"Форма выпуска: {resp.Data.Form.Name}";
            Type.Text = $"Тип: {resp.Data.Type.Name}";

            _recipes.Clear();
            foreach (var recipe in resp.Data.Recipes)
            {
                _recipes.Add(recipe);
            }
        }
    }
}