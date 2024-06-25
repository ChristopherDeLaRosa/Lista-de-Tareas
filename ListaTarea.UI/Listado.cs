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
    public partial class Listado : Form
    {
        //Instancia de la capa de BL
        private TareaService tareaService = new TareaService();
        public Listado()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Listado_Load(object sender, EventArgs e)
        {
            CargarTareas();
        }

        //Evento para agregar una tarea
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Crea una nueva tarea 
            Tarea nuevaTarea = new Tarea
            {
                Titulo = txtTitulo.Text,
                Descripcion = txtDescripcion.Text,
                Completada = false,
            };
            tareaService.AgregarTarea(nuevaTarea);//Agrega una tarea 
            MessageBox.Show("Tarea agregada con éxito.");
            CargarTareas();
            LimpiarControles();
        }

        //Metodo para cargar los datos correspondientes a las tareas
        private void CargarTareas()
        {
            List<Tarea> listaTareas = tareaService.GetTareas();
            dgvTareas.DataSource = listaTareas;
        }

        //Metodo para limpiar los controles despues de haber ejecutado una accion
        private void LimpiarControles()
        {
            txtTitulo.Clear();
            txtDescripcion.Clear();
            
        }

        
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvTareas.SelectedRows.Count > 0)
            {
                // Obtener la tarea seleccionada directamente del DataBoundItem
                Tarea tareaSeleccionada = (Tarea)dgvTareas.SelectedRows[0].DataBoundItem;

                if (tareaSeleccionada != null)
                {
                    // Crear y mostrar el formulario de edicion
                    using (EditarTareaForm formEditar = new EditarTareaForm(tareaSeleccionada))
                    {
                        if (formEditar.ShowDialog() == DialogResult.OK)
                        {
                            // Actualizar la tarea en el servicio
                            tareaService.ActualizarTarea(tareaSeleccionada);
                            // Recargar las tareas en el DataGridView
                            CargarTareas();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo obtener la tarea seleccionada.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione una tarea para modificar");
            }
        }
        //Evento para eliminar un registro
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Tarea tareaSeleccionada = (Tarea)dgvTareas.CurrentRow.DataBoundItem;

            //Muestra un dialogo para saber si el usuario esta seguro de eliminar el registro
            DialogResult resultado = MessageBox.Show("¿Estás seguro que deseas eliminar esta tarea?",
                                                 "Confirmar eliminación",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

            //Si la opcion del usuario es "Yes" o "Si" se procede a eliminar el registro
            if (resultado == DialogResult.Yes)
            {
                tareaService.EliminarTarea(tareaSeleccionada.Id);
                MessageBox.Show("Tarea eliminada con éxito.");
                this.DialogResult = DialogResult.OK;
                CargarTareas();
            }
        }     

        //Evento para marcar una tarea como completada
        private void btnCompletar_Click(object sender, EventArgs e)
        {
            if (dgvTareas.CurrentRow != null)
            {
                Tarea tareaSeleccionada = (Tarea)dgvTareas.CurrentRow.DataBoundItem;
                tareaSeleccionada.Completada = true;
                tareaService.ActualizarTarea(tareaSeleccionada);
                CargarTareas();
            }
            
        }

        //Evento para hacer que las tareas marcadas como completadas se vean diferentes a las imcompletas
        private void dgvTareas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                bool completada = (bool)dgvTareas.Rows[e.RowIndex].Cells["Completada"].Value;
                if (completada)
                {
                    dgvTareas.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(dgvTareas.Font, FontStyle.Strikeout);
                    dgvTareas.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    dgvTareas.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(dgvTareas.Font, FontStyle.Regular);
                    dgvTareas.Rows[e.RowIndex].DefaultCellStyle.BackColor = dgvTareas.DefaultCellStyle.BackColor;
                }
            }
        }
    }
}
