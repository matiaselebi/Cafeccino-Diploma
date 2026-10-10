using System;
using DAL;

namespace BLL
{
    public class BLLBackup
    {
        private readonly DALBackup dalBackup = new DALBackup();
        private readonly BLLUsuario bllUsuario = new BLLUsuario();

        public bool ExisteBackupDisponible() => dalBackup.ExisteBackup();

        public string ObtenerRutaBackup() => dalBackup.ObtenerRutaBackup();

        public void Recalcular()
        {
            bllUsuario.SincronizarDigitos();
            dalBackup.GenerarBackup();
        }

        public void Restaurar()
        {
            dalBackup.RestaurarBackup();
        }
    }
}
