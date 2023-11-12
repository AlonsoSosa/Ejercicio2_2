using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Ejercicio2_2.Modelos
{
    public class BDFirma
    {
        readonly SQLiteAsyncConnection dbase;

        public BDFirma(String dbpath)
        {
            try
            {
                dbase = new SQLiteAsyncConnection(dbpath);

                dbase.CreateTableAsync<Constructor>().Wait(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la tabla: {ex.Message}");
            }

        }

        public Task<List<Constructor>> listaempleados()
        {

            return dbase.Table<Constructor>().ToListAsync();
        }

        public Task<List<Constructor>> ObtenerEmpleado()
        {
            return dbase.Table<Constructor>().ToListAsync();
        }

        public Task<Constructor> obtenerEmple(int pid)
        {
            return dbase.Table<Constructor>()
                .Where(i => i.codigo == pid)
                .FirstOrDefaultAsync();
        }

        public Task<int> EmpleadoGuardar(Constructor emple)
        {
            if (emple.codigo != 0)
            {
                return dbase.UpdateAsync(emple);
            }
            else
            {
                return dbase.InsertAsync(emple);
            }
        }

        public Task<int> EmpleadoBorrar(Constructor emple)
        {
            return dbase.DeleteAsync(emple);
        }
    }
}
