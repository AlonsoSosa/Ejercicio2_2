using CommunityToolkit.Maui.Views;
using Ejercicio2_2.Modelos;
using Ejercicio2_2.Vistas;
using SQLite;

namespace Ejercicio2_2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    await MostrarAlerta("Error", "Por favor, complete los campos y firme antes de guardar.");
                    return;
                }

                byte[] imagenBytes = await ObtenerImagenDibujada();

                if (imagenBytes == null || imagenBytes.Length == 0)
                {
                    await MostrarAlerta("Error", "Por favor, firme antes de guardar.");
                    return;
                }

                Constructor nuevoConstructor = new Constructor
                {
                    descripcion = txtDescripcion.Text,
                    img = imagenBytes
                };

                var resultado = await App.BaseFirmaDB.EmpleadoGuardar(nuevoConstructor);

                LimpiarCampos();

                ((DrawingView)this.FindByName<DrawingView>("drawingView")).Clear();

                await MostrarAlerta("Éxito", "La información se guardó correctamente.");
            }
            catch (SQLiteException ex)
            {
                await MostrarAlerta("Error", $"Error de base de datos: {ex.Message}");
            }
            catch (Exception ex)
            {
                await MostrarAlerta("Error", $"Hubo un error al intentar guardar: {ex.Message}");
            }
        }

        private async Task<byte[]> ObtenerImagenDibujada()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Stream imagenStream = await ((DrawingView)this.FindByName<DrawingView>("drawingView")).GetImageStream(200, 200);
                await imagenStream.CopyToAsync(stream);
                return stream.ToArray();
            }
        }

        private void LimpiarCampos()
        {
            txtDescripcion.Text = "";
        }

        private async Task MostrarAlerta(string titulo, string mensaje)
        {
            await DisplayAlert(titulo, mensaje, "Aceptar");
        }


        private async void btnLista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageLista());
        }
    }
}