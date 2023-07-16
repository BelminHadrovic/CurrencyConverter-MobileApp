using MauiApp1.Models;
using System.Net.Http.Json;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    Response currencyResponse = null;

    public MainPage()
    {
        InitializeComponent();

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetRates();
    }

    public async Task<Response> GetRates()
    {
        HttpClient httpClient = new HttpClient();
        var url = "http://api.exchangeratesapi.io/v1/latest?access_key=46dc6f8416635459f6e36bbbdd9f745f";
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            currencyResponse = await response.Content.ReadFromJsonAsync<Response>();

        }
        return currencyResponse;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var ammount = 0.0;
        var from = "";
        var to = "";
        try
        {
            ammount = double.Parse(AmountEntry.Text.ToString());
            from = FromCurrencyPicker.SelectedItem.ToString();
            to = ToCurrencyPicker.SelectedItem.ToString();
        }
        catch
        {
            Application.Current.MainPage.DisplayAlert("Upozorenje", "Molimo vas unesite validan unos", "Ok");

        }

        if (ammount > 0 && from.Length > 0 && to.Length > 0)
        {
            var rates = currencyResponse.rates;
            var exchangeList = new Dictionary<string, double>(){
                {"EUR", rates.EUR},
                {"BAM", rates.BAM},
                {"USD", rates.USD},
                {"GBP", rates.GBP},

            };


            var baseValue = ammount / exchangeList[from];
            var resultValue = baseValue * exchangeList[to];

            ResultLabel.Text = resultValue.ToString("0.00") + " " + to;

        }
    }
}

