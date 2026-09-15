using System;
using DAL;

namespace BLL
{
    public class BLLBackup
    {
        private readonly DALBackup _dal = new DALBackup();
        private readonly BLLUsuario _usuarioBLL = new BLLUsuario();

        public bool ExisteBackupDisponible() => _dal.ExisteBackup();

        public string ObtenerRutaBackup() => _dal.ObtenerRutaBackup();

        public void Recalcular()
        {
            _usuarioBLL.SincronizarDigitos();
            _dal.GenerarBackup();
        }

        public void Restaurar()
        {
            _dal.RestaurarBackup();
        }
    }
}
