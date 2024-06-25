using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaTarea.DAL
{
    public class TareaDAL
    {
        //Cadena de conexion a la base de datos SQL Server
        private string _cadenaConexion = "Data Source = LAPTOP-LGPSKH9S\\SQLEXPRESS; Initial Catalog = ToDoListDB; Integrated Security = True; Trusted_Connection = True; TrustServerCertificate = True; ";

        //Metodo para agregar una nueva tarea a la base de datos
        public void AgregarTarea(Tarea tarea)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
                {
                    conexion.Open();

                    //Query SQL para insertar una nueva tarea
                    string query = "INSERT INTO Tareas(Titulo, Descripcion, Completada) VALUES (@Titulo, @descripcion, @completada)";
                    SqlCommand cmd = new SqlCommand(query, conexion);

                    //Agregar parametros al comando SQL
                    cmd.Parameters.AddWithValue("@titulo", tarea.Titulo);
                    cmd.Parameters.AddWithValue("@descripcion", tarea.Descripcion);
                    cmd.Parameters.AddWithValue("@completada", tarea.Completada);

                    //Ejecuta el comando
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Problema al tratar de conectarse a la base de datos", ex);
            }
        }
        //Metodo para Actualizar una tarea
        public void ActualizarTarea(Tarea tarea)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
                {
                    conexion.Open();
                    //Query SQL para actualizar una tarea
                    string query = "UPDATE Tareas SET Titulo = @titulo, Descripcion = @descripcion, Completada = @completada WHERE Id = @id";
                    SqlCommand cmd = new SqlCommand(query, conexion);

                    //Agregar parametros al comando SQL
                    cmd.Parameters.AddWithValue("@id", tarea.Id);
                    cmd.Parameters.AddWithValue("@titulo", tarea.Titulo);
                    cmd.Parameters.AddWithValue("@descripcion", tarea.Descripcion);
                    cmd.Parameters.AddWithValue("@completada", tarea.Completada);

                    //Ejecuta el comando
                    cmd.ExecuteNonQuery();
                    conexion.Close();

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Problema al tratar de conectarse a la base de datos", ex);
            }
        }
        //Metodo para eliminar una tarea
        public void EliminarTarea(int id)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
                {
                    conexion.Open();
                    //Query SQL para eliminar una tarea
                    string query = "DELETE FROM Tareas WHERE Id = @id";
                    SqlCommand cmd = new SqlCommand(query, conexion);

                    //Agrega parametros al comando
                    cmd.Parameters.AddWithValue("@id", id);

                    //Ejecuta el comando
                    cmd.ExecuteNonQuery();
                    conexion.Close();

                }
            }
            catch (SqlException ex)
            {

                Console.WriteLine("Problema al tratar de conectarse a la base de datos", ex);
            }
        }
        //Metodo para obtener todas las tareas de nuestra base de datos
        public List<Tarea> GetTareas()
        {

            List<Tarea> tareas = new List<Tarea>();

            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                conexion.Open();
                string query = "SELECT * FROM Tareas";
                SqlCommand cmd = new SqlCommand(query, conexion);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Tarea tarea = new Tarea
                    {
                        Id = (int)reader["Id"],
                        Titulo = reader["Titulo"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        Completada = (bool)reader["Completada"]
                    };
                    tareas.Add(tarea);
                }
            }
            return tareas;

        }

        //Metodo para encontrar un registro por un item 
        public List<Tarea> BuscarTarea(string item)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
                {
                    conexion.Open();

                    string query = "SELECT * FROM Tareas WHERE " + 
                        "Titulo LIKE @item OR " + "Descripcion LIKE @item";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@item", item);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Tarea> listaTareas = new List<Tarea>();

                    while (reader.Read())
                    {
                        int id = int.Parse(reader["Id"].ToString());
                        string titulo = reader["Titulo"].ToString();
                        string descripcion = reader["Apellido"].ToString();

                        Tarea tarea = new Tarea(id, titulo, descripcion);
                        listaTareas.Add(tarea);
                    }
                    reader.Close();
                    return listaTareas;
                    
                }
            }
            catch
            {
                return null;
            }
        }

    }
}
