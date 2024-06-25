using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaTarea.DAL
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion {  get; set; }
        public bool Completada { get; set; }

        public Tarea(int id, string titulo, string descripcion)
        {
            this.Id = id;
            this.Titulo = titulo;
            this.Descripcion = descripcion;
        }
        public Tarea() { }
    }
}
