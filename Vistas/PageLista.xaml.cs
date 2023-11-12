namespace Ejercicio2_2.Vistas;

public partial class PageLista : ContentPage
{
	public PageLista()
	{
		InitializeComponent();
	}

    private async void toolmenu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        listaFirma.ItemsSource = await App.BaseFirmaDB.ObtenerEmpleado();
    }
}