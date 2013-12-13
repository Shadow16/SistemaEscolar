using System;
using Gtk;
// Libreria de MySQL
using MySql.Data.MySqlClient;
using Pango;

namespace EscuelaPrimaria
{
	public partial class Alumnos : Gtk.Window
	{
		/*************** Metdo de Heredacion 
						Donde la clase principal es: MainWindow  *************************/
		private MainWindow padre;
		public Alumnos (MainWindow padre, string id) : 
			base (Gtk.WindowType.Toplevel)
		{
			this.padre = padre;
			this.Build ();
			this.Cargar (id);
			this.Materias(id);
			this.Cursos (id);
			this.Pagos (id);
			this.Grado (id);
		}

		// Boton Borrar
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			MainWindow win = new MainWindow ();
			win.Visible = true;
		}

		// Conexion de MySql utilizando Wampserver
		protected MySqlConnection myConnection;
		protected void abrirConexion ()
		{
			string connectionString =
				"Server=localhost;" +
				"Database=SistemaEscolar;" +
				"User ID=root;" +
				"Password=;" +
				"Pooling=false;";
			// Declarar la conexion abierta
			this.myConnection = new MySqlConnection (connectionString);
			this.myConnection.Open ();
		}
		// Metodo de conexion cerrada
		protected void cerrarConexion ()
		{
			this.myConnection.Close (); 
			this.myConnection = null;
		}

		/*  Al ejecutar un comando lo primero que se hace es utilizar la 
		conexion de mysql, declarar la variable y correr comando       */
		private int ejecutarComando (string sql)
		{
			MySqlCommand myCommand = new MySqlCommand (sql, this.myConnection);
			int afectadas = myCommand.ExecuteNonQuery ();
			myCommand.Dispose ();
			myCommand = null;
			return afectadas;
		}

		/*   Metodo para cargar datos de la tabla estudiantes   */
		public void Cargar (string id)
		{
			this.abrirConexion ();
			MySqlCommand myCommand = new MySqlCommand ("SELECT * FROM `Estudiante` WHERE (`id`='" + id + "')", 
				                         myConnection);
			MySqlDataReader myReader = myCommand.ExecuteReader ();	
			while (myReader.Read ()) {
				// Asignamos la variable al campo de la base de datos con el nombre
				string codigo = myReader ["codigo"].ToString ();
				string nombre = myReader ["nombre"].ToString ();
				string apellidoP = myReader ["apellidoP"].ToString ();
				string apellidoM = myReader ["apellidoM"].ToString ();
				string calle = myReader ["calle"].ToString ();
				string numero = myReader ["numero"].ToString ();
				string numeroInt = myReader ["numeroInt"].ToString ();
				string ciudad = myReader ["ciudad"].ToString ();
				string colonia = myReader ["colonia"].ToString ();
				string telefono = myReader ["telefono"].ToString ();
				string avisarA = myReader ["avisarA"].ToString ();
				string telEm = myReader ["telEm"].ToString ();

				// Asignamos el campo con la variable tipo string
				codTxt.Text = codigo.ToString ();
				nomTxt.Text = nombre.ToString ();
				paternoTxt.Text = apellidoP.ToString ();
				maternoTxt.Text = apellidoM.ToString ();
				calleTxt.Text = calle.ToString ();
				numExtTxt.Text = numero.ToString ();
				numIntTex.Text = numeroInt.ToString ();
				ciudadText.Text = ciudad.ToString ();
				colTxt.Text = colonia.ToString ();
				telTxt.Text = telefono.ToString ();
				aviTxt.Text = avisarA.ToString ();
				telEmTxt.Text = telEm.ToString ();
				// Nombre de la ventana al ingresar el usuario
				this.Title = "Bienvenido " + nombre.ToString ()
				+ " " + apellidoP.ToString () + " " + apellidoM.ToString ();
				labelNombre.Text = "Bienvenido " + nombre.ToString ()
				+ " " + apellidoP.ToString () + " " + apellidoM.ToString ();
				labelNombre.ModifyFont (FontDescription.FromString ("Arial Bold 12"));
			}
			// Una vez echo esto cerramos la conexion para evitar posibles fugas de datos
			myReader.Close ();
			myReader = null;
			myCommand.Dispose ();
			myCommand = null;
			this.cerrarConexion ();
		}

		// Metodo para la ventana de grado
		public void Grado (string id)
		{
			// Abrimos la conexion igual mediante MySQL
			this.abrirConexion ();
			MySqlCommand myCommand = new MySqlCommand ("SELECT * FROM `Grado` WHERE (`id_Maestro`='" + id + "_Estudiante')", 
				myConnection);
			MySqlDataReader myReader = myCommand.ExecuteReader ();	
			while (myReader.Read ()) {
				// Declaramos variables y asignamos el nombre del campo de la base de datos
				string numGrado = myReader ["numGrado"].ToString ();
				string aula = myReader ["aula"].ToString ();
				string edificio = myReader ["edificio"].ToString ();
				gradoText.Text = "Grado: " + numGrado.ToString ()
				                 + " Aula: " + aula.ToString () + " Edificio: " + edificio.ToString ();
				gradoText.ModifyFont (FontDescription.FromString ("Arial Bold 10"));
			}
			//  Cerramos el tipo de conexion
			myReader.Close ();
			myReader = null;
			myCommand.Dispose ();
			myCommand = null;
			this.cerrarConexion ();
		}

		// Creamos una clase que nos despliegara las diferentes materias para cada alumno
		private void Materias(string id){
			ListStore TipoDeListado;
			TipoDeListado = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
			nodeMateria.Model = TipoDeListado;

			// Abrimos la conexion de la tabla Materia
			this.abrirConexion();
			MySqlCommand myCommand = new MySqlCommand(
				"SELECT * FROM `Materia` WHERE (`id_Estudiante`='" + id + "_Estudiante')", myConnection);
			MySqlDataReader myReader = myCommand.ExecuteReader();	
			while (myReader.Read()){
				// Mientras existan valores dentro de la tabla va a mostrarlos con el ciclo while
				string codigo = myReader["codigo"].ToString();
				string nombre = myReader["nombre"].ToString();
				string area = myReader["area"].ToString();
				string horas = myReader["horas"].ToString();
				string calificacion = myReader["calificacion"].ToString();
				string faltas = myReader["faltas"].ToString();
				TipoDeListado.AppendValues(codigo, nombre, area, horas, calificacion, faltas);
			}
			// Cerramos conexion
			myReader.Close();
			myReader = null;
			myCommand.Dispose();
			myCommand = null;
			this.cerrarConexion();

			nodeMateria.AppendColumn("Código", new CellRendererText(), "text", 0);
			nodeMateria.AppendColumn("Nombre", new CellRendererText(), "text", 1); 
			nodeMateria.AppendColumn("Area", new CellRendererText(), "text", 2);
			nodeMateria.AppendColumn("Horas", new CellRendererText(), "text", 3); 
			nodeMateria.AppendColumn("Calificacion", new CellRendererText(), "text", 4);
			nodeMateria.AppendColumn("Faltas", new CellRendererText(), "text", 5);
			nodeMateria.EnableGridLines = TreeViewGridLines.Horizontal;
		}
		// Abrimos una clase llamada cursos y despliegara dicha informacion
		private void Cursos(string id){
			ListStore TipoDeListado;
			TipoDeListado = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
			nodeCursos.Model = TipoDeListado;
			// Abrimos la conexion de mysql
			this.abrirConexion();
			MySqlCommand myCommand = new MySqlCommand(
				"SELECT * FROM `Cursos` WHERE (`id_Estudiante`='" + id + "_Estudiante')", myConnection);
			MySqlDataReader myReader = myCommand.ExecuteReader();	
			while (myReader.Read()){
				// Mientras en la tabla de cursos haya informacion disponible los mostrara por la instancia del while
				string codigo = myReader["codigo"].ToString();
				string nombre = myReader["nombre"].ToString();
				string area = myReader["area"].ToString();
				string horas = myReader["horas"].ToString();
				string acreditacion = myReader["acreditacion"].ToString();
				string faltas = myReader["faltas"].ToString();
				TipoDeListado.AppendValues(nombre, codigo, horas, area, acreditacion, faltas);
			}
			// Cerramos conexion
			myReader.Close();
			myReader = null;
			myCommand.Dispose();
			myCommand = null;
			this.cerrarConexion();
			// Asignamos los valores de la base de datos con la variable
			nodeCursos.AppendColumn("Código", new CellRendererText(), "text", 0);
			nodeCursos.AppendColumn("Nombre", new CellRendererText(), "text", 1); 
			nodeCursos.AppendColumn("Area", new CellRendererText(), "text", 2);
			nodeCursos.AppendColumn("Horas", new CellRendererText(), "text", 3); 
			nodeCursos.AppendColumn("Acreditacion", new CellRendererText(), "text", 4);
			nodeCursos.AppendColumn("Faltas", new CellRendererText(), "text", 5);
			nodeCursos.EnableGridLines = TreeViewGridLines.Horizontal;
		}
		// Clase pagos
		private void Pagos(string id){
			ListStore TipoDeListado;
			TipoDeListado = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
			nodePagos.Model = TipoDeListado;
			// Abrimos conexion
			this.abrirConexion();
			MySqlCommand myCommand = new MySqlCommand(
				"SELECT * FROM `Pagos` WHERE (`id_Estudiante`='" + id + "_Estudiante')", myConnection);
			MySqlDataReader myReader = myCommand.ExecuteReader();	
			while (myReader.Read()){
				// Mientras existan datos 
				string codigo = myReader["codigo"].ToString();
				string fecha = myReader["fecha"].ToString();
				string tipopago = myReader["tipopago"].ToString();
				string importe = myReader["importe"].ToString();
				string observaciones = myReader["observaciones"].ToString();
				TipoDeListado.AppendValues(codigo, fecha, tipopago, importe, observaciones);
			}

			myReader.Close();
			myReader = null;
			myCommand.Dispose();
			myCommand = null;
			this.cerrarConexion();

			nodePagos.AppendColumn("Código", new CellRendererText(), "text", 0);
			nodePagos.AppendColumn("Fecha", new CellRendererText(), "text", 1);
			nodePagos.AppendColumn("Tipo de pago", new CellRendererText(), "text", 2); 
			nodePagos.AppendColumn("Importe", new CellRendererText(), "text", 3);
			nodePagos.AppendColumn("Observaciones", new CellRendererText(), "text", 4); 
			nodePagos.EnableGridLines = TreeViewGridLines.Horizontal;
		}
	}
}

