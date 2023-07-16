using MauiApp1.Models;
using System.Net.Http.Json;

namespace MauiApp1;

public partial class ExchangeRateList : ContentPage
{

    Response currencyResponse = null;

    public ExchangeRateList()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await GetRates();

        USD.Text = "USD: "+  currencyResponse.rates.USD.ToString();
        BAM.Text = "BAM: " + currencyResponse.rates.BAM.ToString();
        UAD.Text = "UAD: " + currencyResponse.rates.UAH.ToString();
        KWD.Text = "KWD: " + currencyResponse.rates.KWD.ToString();
        BHD.Text = "BHD: " + currencyResponse.rates.BHD.ToString();
        GBP.Text = "GBP: " + currencyResponse.rates.GBP.ToString();
        KYD.Text = "KYD: " + currencyResponse.rates.KYD.ToString();
        CHF.Text = "CHF: " + currencyResponse.rates.CHF.ToString();
        JPY.Text = "JPY: " + currencyResponse.rates.JPY.ToString();
        CAD.Text = "CAD: " + currencyResponse.rates.CAD.ToString();
        AUD.Text = "AUD: " + currencyResponse.rates.AUD.ToString();
        RSD.Text = "RSD: " + currencyResponse.rates.RSD.ToString();
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
}