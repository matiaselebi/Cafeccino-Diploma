using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SERVICIOS;

namespace BLL
{
    public class BLLBitacoraEventos
    {
        DALBitacoraEventos eventoDAL = new DALBitacoraEventos();

        public void RegistrarEvento(string usuario,
                                    string evento,
                                    string modulo,
                                    string criticidad)
        {
            BitacoraEventos ev = new BitacoraEventos();

            ev.NombreUsuario = usuario;
            ev.Fecha = DateTime.Now;
            ev.Evento = evento;
            ev.Modulo = modulo;
            ev.Criticidad = criticidad;

            eventoDAL.RegistrarEvento(ev);
            new BLLDigitoVerificador().Recalcular_389ME("BitacoraEventos");
        }
        // Registra un evento con el detalle de lo que paso. Si no se indica el usuario,
        // se toma el de la sesion abierta (en el login todavia no hay sesion).
        public void RegistrarEvento_389ME(string evento_389ME, string detalle_389ME, string modulo_389ME, string criticidad_389ME, string usuario_389ME = null)
        {
            BitacoraEventos bitacora_389ME = new BitacoraEventos
            {
                NombreUsuario = usuario_389ME ?? Singleton.Instancia.Usuario?.NombreUsuario ?? "Sin sesion",
                Fecha = DateTime.Now,
                Evento = evento_389ME,
                Detalle_389ME = detalle_389ME,
                Modulo = modulo_389ME,
                Criticidad = criticidad_389ME
            };

            eventoDAL.RegistrarEvento(bitacora_389ME);
            new BLLDigitoVerificador().Recalcular_389ME("BitacoraEventos");
        }

        // Agrega "Campo: antes -> despues" a la lista solo si el valor cambio.
        public static void AgregarCambio_389ME(List<string> cambios_389ME, string campo_389ME, object antes_389ME, object despues_389ME)
        {
            if (Convert.ToString(antes_389ME) != Convert.ToString(despues_389ME))
                cambios_389ME.Add($"{campo_389ME}: {antes_389ME} -> {despues_389ME}");
        }

        // Pasa a la bitacora los desbloqueos hechos desde la base (los anota el trigger TR_Usuario_Desbloqueo).
        // Asi el evento queda con su digito verificador y la verificacion de integridad no da una falsa alarma.
        public void RegistrarDesbloqueos_389ME()
        {
            foreach (DataRow fila_389ME in eventoDAL.ObtenerDesbloqueos_389ME().Rows)
            {
                RegistrarEvento_389ME("Desbloqueo de usuario",
                    $"Usuario {fila_389ME["Username_389ME"]} desbloqueado desde la base de datos el {(DateTime)fila_389ME["Fecha_389ME"]:dd/MM/yyyy HH:mm}",
                    "Login", "Media", fila_389ME["Responsable_389ME"].ToString());
                eventoDAL.BorrarDesbloqueo_389ME((int)fila_389ME["CodDesbloqueo_389ME"]);
            }
        }

        public DataTable ObtenerEventos()
        {
            return eventoDAL.ListarEventos();
        }

        public DataTable FiltrarEventos(string modulo, string criticidad, string evento, string fecha)
        {
            return eventoDAL.FiltrarEventos(modulo, criticidad, evento, fecha);
        }
    }
}
