using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ListaTarea.DAL;

namespace ListaTarea.BL
{
    public class TareaService
    {
        TareaDAL contexto = new TareaDAL();

        public void AgregarTarea(Tarea tarea)
        {
            contexto.AgregarTarea(tarea);
        }

        public void ActualizarTarea(Tarea tarea)
        {
            contexto.ActualizarTarea(tarea);
        }

        public void EliminarTarea(int id)
        {
            contexto.EliminarTarea(id);
        }

        public List<Tarea> GetTareas()
        {
            return contexto.GetTareas();
        }

        public List<Tarea> BuscarTarea(string item)
        {
            return contexto.BuscarTarea(item);
        }
    }
}
