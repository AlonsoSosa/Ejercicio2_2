using Ejercicio2_2.Vistas;
using Ejercicio2_2.Modelos;


namespace Ejercicio2_2
{
    public partial class App : Application
    {
        static BDFirma BaseFirma;

        public static BDFirma BaseFirmaDB
        {
            get
            {
                if (BaseFirma == null)
                {
                    BaseFirma = new BDFirma(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DBFirmas.db3"));
                }
                return BaseFirma;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PageLista());
        }
    }
}