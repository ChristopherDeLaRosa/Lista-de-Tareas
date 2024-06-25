using ListaTarea.BL;
using ListaTarea.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListaTarea.UI
{
    public partial class EditarTareaForm : Form
    {
        private Tarea tarea;
        public EditarTareaForm(Tarea tarea)
        {
            InitializeComponent();
            this.tarea = tarea;
            CargarDatosTareas();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            tarea.Titulo = txtTitulo.Text;
            tarea.Descripcion = txtDescripcion.Text;
            tarea.Completada = false;

            TareaService tareaService = new TareaService();
            tareaService.ActualizarTarea(tarea);

            MessageBox.Show("Tarea modificada con éxito.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CargarDatosTareas()
        {
            txtTitulo.Text = tarea.Titulo;
            txtDescripcion.Text = tarea.Descripcion;
        }
    }
}
