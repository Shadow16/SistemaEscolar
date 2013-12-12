using System;
using Gtk;
using MySql.Data.MySqlClient;
using Pango;

namespace EscuelaPrimaria
{
	public partial class Profesor : Gtk.Window
	{
		private MainWindow padre;
		public Profesor (MainWindow padre,string id) : 
			base (Gtk.WindowType.Toplevel)
		{
			this.padre = padre;
			this.Build ();
			this.Cargar (id);
			this.Materias(id);
			this.Cursos (id);
			this.Nomina (id);
		}
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			MainWindow win = new MainWindow ();
			win.Visible = true;
		}

		protected MySqlConnection myConnection;

		protected void abrirConexion ()
		{
			string connectionString =
				"Server=localhost;" +
				"Database=SistemaEscolar;" +
				"User ID=root;" +
				"Password=;" +
				"Pooling=false;";
			this.myConnection = new MySqlConnection (connectionString);
			this.myConnection.Open ();
		}

		protected void cerrarConexion ()
		{
			this.myConnection.Close (); 
			this.myConnection = null;
		}

		private int ejecutarComando (string sql)
		{
			MySqlCommand myCommand = new MySqlCommand (sql, this.myConnection);
			int afectadas = myCommand.ExecuteNonQuery ();
			myCommand.Dispose ();
			myCommand = null;
			return afectadas;
		}

		public void Cargar (string id)
		{
			this.abrirConexion ();
			MySqlCommand myCommand = new MySqlCommand ("SELECT * FROM `Maestro` WHERE (`id`='" + id + "')", 
				myConnection);
			MySqlDataReader myReader = myCommand.ExecuteReader ();	
			while (myReader.Read ()) {
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
				codTxt2.Text = codigo.ToString ();
				nomTxt2.Text = nombre.ToString ();
				paternoTxt2.Text = apellidoP.ToString ();
				maternoTxt2.Text = apellidoM.ToString ();
				calleTxt2.Text = calle.ToString ();
				numExtTxt2.Text = numero.ToString ();
				numIntTex2.Text = numeroInt.ToString ();
				ciudadText2.Text = ciudad.ToString ();
				colTxt2.Text = colonia.ToString ();
				telTxt2.Text = telefono.ToString ();
				aviTxt2.Text = avisarA.ToString ();
				telEmTxt2.Text = telEm.ToString ();
				this.Title = "Bienvenido Profesor " + nombre.ToString ()
				             + " " + apellidoP.ToString () + " " + apellidoM.ToString ();
				labelNombre.Text = "Bienvenido Profesor " + nombre.ToString ()
				                   + " " + apellidoP.ToString () + " " + apellidoM.ToString ();
				labelNombre.ModifyFont (FontDescription.FromString ("Arial Bold 12"));
			}

			myReader.Close ();
			myReader = null;
			myCommand.Dispose ();
			myCommand = null;
			this.cerrarConexion ();
		}
		
		private void Materias(string id){
			ListStore TipoDeListado;
			TipoDeListado = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
			nodeMateria.Model = TipoDeListado;

			this.abrirConexion();
			MySqlCommand myCommand = new MySqlCommand(
				"SELECT * FROM `Materia` WHERE (`id_Maestro`='" + id + "_Maestro')", myConnection);
			MySqlDataReader myReader = myCommand.ExecuteReader();	
			while (myReader.Read()){
				string id_Estudiante = myReader["id_Estudiante"].ToString();
				string codigo = myReader["codigo"].ToString();
				string nombre = myReader["nombre"].ToString();
				string area = myReader["area"].ToString();
				string horas = myReader["horas"].ToString();
				string calificacion = myReader["calificacion"].ToString();
				string faltas = myReader["faltas"].ToString();
				TipoDeListado.AppendValues(id_Estudiante, nombre, codigo, horas, area, calificacion, faltas);
			}

			myReader.Close();
			myReader = null;
			myCommand.Dispose();
			myCommand = null;
			this.cerrarConexion();

			nodeMateria.AppendColumn("Estudiante", new CellRendererText(), "text", 0);
			nodeMateria.AppendColumn("Código", new CellRendererText(), "text", 1);
			nodeMateria.AppendColumn("Nombreo", new CellRendererText(), "text", 2); 
			nodeMateria.AppendColumn("Area", new CellRendererText(), "text", 3);
			nodeMateria.AppendColumn("Horas", new CellRendererText(), "text", 4); 
			nodeMateria.AppendColumn("Calificacion", new CellRendererText(), "text", 5);
			nodeMateria.AppendColumn("Faltas", new CellRendererText(), "text", 6);
			nodeMateria.EnableGridLines = TreeViewGridLines.Horizontal;
		}
		private void Cursos(string id){
			ListStore TipoDeListado;
			TipoDeListado = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
			nodeCursos.Model = TipoDeListado;

			this.abrirConexion();
			MySqlCommand myCommand = new MySqlCommand(
				"SELECT * FROM `Cursos` WHERE (`id_Maestro`='" + id + "_Maestro')", myConnection);
			MySqlDataReader myReader = myCommand.ExecuteReader();	
			while (myReader.Read()){
				string id_Estudiante = myReader["id_Estudiante"].ToString();
				string codigo = myReader["codigo"].ToString();
				string nombre = myReader["nombre"].ToString();
				string area = myReader["area"].ToString();
				string horas = myReader["horas"].ToString();
				string acreditacion = myReader["acreditacion"].ToString();
				string faltas = myReader["faltas"].ToString();
				TipoDeListado.AppendValues(id_Estudiante, nombre, codigo, horas, area, acreditacion, faltas);
			}

			myReader.Close();
			myReader = null;
			myCommand.Dispose();
			myCommand = null;
			this.cerrarConexion();

			nodeCursos.AppendColumn("Estudiante", new CellRendererText(), "text", 0);
			nodeCursos.AppendColumn("Código", new CellRendererText(), "text", 1);
			nodeCursos.AppendColumn("Nombre", new CellRendererText(), "text", 2); 
			nodeCursos.AppendColumn("Area", new CellRendererText(), "text", 3);
			nodeCursos.AppendColumn("Horas", new CellRendererText(), "text", 4); 
			nodeCursos.AppendColumn("Acreditacion", new CellRendererText(), "text", 5);
			nodeCursos.AppendColumn("Faltas", new CellRendererText(), "text", 6);
			nodeCursos.EnableGridLines = TreeViewGridLines.Horizontal;
		}
		private void Nomina(string id){
			ListStore TipoDeListado;
			TipoDeListado = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
			nodePagos.Model = TipoDeListado;

			this.abrirConexion();
			MySqlCommand myCommand = new MySqlCommand(
				"SELECT * FROM `Nomina` WHERE (`id_Maestro`='" + id + "_Maestro')", myConnection);
			MySqlDataReader myReader = myCommand.ExecuteReader();	
			while (myReader.Read()){
				string codigo = myReader["codigo"].ToString();
				string fecha = myReader["fecha"].ToString();
				string horasTrabajadas = myReader["horasTrabajadas"].ToString();
				string horasExtra = myReader["horasExtra"].ToString();
				string observaciones = myReader["observaciones"].ToString();
				TipoDeListado.AppendValues(codigo, fecha, horasTrabajadas, horasExtra, observaciones);
			}

			myReader.Close();
			myReader = null;
			myCommand.Dispose();
			myCommand = null;
			this.cerrarConexion();

			nodePagos.AppendColumn("Código", new CellRendererText(), "text", 0);
			nodePagos.AppendColumn("Fecha", new CellRendererText(), "text", 1);
			nodePagos.AppendColumn("Horas Trabajadas", new CellRendererText(), "text", 2); 
			nodePagos.AppendColumn("Horas Extras", new CellRendererText(), "text", 3);
			nodePagos.AppendColumn("Observaciones", new CellRendererText(), "text", 4); 
			nodePagos.EnableGridLines = TreeViewGridLines.Horizontal;
		}

	}
}

