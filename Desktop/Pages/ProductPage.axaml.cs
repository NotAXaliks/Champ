using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using Avalonia.Controls;
using Desktop.Services;

namespace Desktop.Pages
{
    public record ProductResult(Product Product, TechCard TechCard, Recipe? Recipe);
    
    public partial class ProductPage : UserControl
    {

        private readonly ObservableCollection<ProductResult> _products = new ();
        
        public ProductPage()
        {
            InitializeComponent();

            ProductsGrid.ItemsSource = _products;

            Update();
        }

        public async void Update()
        {
            try 
            {
                _products.Clear();

                var resp = await HttpService.Request<ProductResult[]>(HttpMethod.Get, "Product");

                foreach (var item in resp.Data)
                {
                    _products.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");
            }
        }
        
       private async void ClickProduct(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is ProductResult selected)
            {
                var productWindow = new ProductPageWindow(selected);
                productWindow.Show();

                var topLevel = TopLevel.GetTopLevel(this);
                var file = await topLevel.StorageProvider.SaveFilePickerAsync(
                        new Avalonia.Platform.Storage.FilePickerSaveOptions()
                        {
                            SuggestedFileName = "На_складе.pdf",
                        });

                    if (file == null)
                        return;

                ExportExcel.ExportBatches(file.Path.LocalPath);
            }

        }
    }
}