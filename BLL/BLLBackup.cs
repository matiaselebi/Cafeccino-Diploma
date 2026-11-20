using System;
using DAL;

namespace BLL
{
    public class BLLBackup
    {
        private readonly DALBackup dalBackup = new DALBackup();
        private readonly BLLUsuario bllUsuario = new BLLUsuario();
        private readonly BLLBitacoraEventos bllBitacora_389ME = new BLLBitacoraEventos();

        public bool ExisteBackupDisponible() => dalBackup.ExisteBackup();

        public string ObtenerRutaBackup() => dalBackup.ObtenerRutaBackup();

        public void Recalcular()
        {
            bllUsuario.SincronizarDigitos();
            new BLLDigitoVerificador().RecalcularTodo_389ME();
            dalBackup.GenerarBackup();

            bllBitacora_389ME.RegistrarEvento_389ME("Recálculo de dígitos verificadores", "Se recalcularon los DVH y el DVV y se generó un backup", "Integridad", "Alta");
        }

        public void Restaurar()
        {
            dalBackup.RestaurarBackup();

            // Se registra despues de restaurar: si se hiciera antes, el propio backup lo borraria.
            bllBitacora_389ME.RegistrarEvento_389ME("Restauración de backup", dalBackup.ObtenerRutaBackup(), "Integridad", "Alta");
        }
    }
}
