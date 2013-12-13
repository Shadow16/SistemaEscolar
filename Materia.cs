using System;
using Gtk;
using MySql.Data.MySqlClient;

namespace EscuelaPrimaria
{
	public partial class Materia : Gtk.Window
	{
		private Cordinacion padre;
		public Materia (Cordinacion padre) : 
			base (Gtk.WindowType.Toplevel)
		{
			this.padre = padre;
			this.Build ();
			this.listaFullAlumno();
			this.nodeAlumno();
			this.listaFullMaestro();
			this.nodeMaestro();
			this.nodeMat ();
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
		private void nodeAlumno(){
			nodeview1.AppendColumn("ID", new CellRendererText(), "text", 0);
			nodeview1.AppendColumn("Código", new CellRendererText(), "text", 1); 
			nodeview1.AppendColumn("Nombre", new CellRendererText(), "text", 2);
			nodeview1.AppendColumn("Apellido Paterno", new CellRendererText(), "text", 3);
			nodeview1.AppendColumn("Papellido Materno", new CellRendererText(), "text", 4);
			nodeview1.EnableGridLines = TreeViewGridLines.Horizontal;
			ListStore TipoDeListado;
			TipoDeListado = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
		}
		private void listaFullAlumno(){
			ListStore TipoDeListado;
			TipoDeListado = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
			nodeview1.Model = TipoDeListado;

			this.abrirConexion();
			MySqlCommand myCommand = new MySqlCommand(
				"SELECT * FROM `Estudiante`", myConnection);
			MySqlDataReader myReader = myCommand.ExecuteReader();	
			while (myReader.Read()){
				string id = myReader["id"].ToString();
				string codigo = myReader["codigo"].ToString();
				string nombre = myReader["nombre"].ToString();
				string apellidoP = myReader["apellidoP"].ToString();
				string apellidoM = myReader["apellidoM"].ToString();
				TipoDeListado.AppendValues(id, codigo, nombre,apellidoP,apellidoM);
			}
			myReader.Close();
			myReader = null;
			myCommand.Dispose();
			myCommand = null;
			this.cerrarConexion();
		}
		private void nodeMaestro(){
			nodeview2.AppendColumn("ID", new CellRendererText(), "text", 0);
			nodeview2.AppendColumn("Código", new CellRendererText(), "text", 1); 
			nodeview2.AppendColumn("Nombre", new CellRendererText(), "text", 2);
			nodeview2.AppendColumn("Apellido Paterno", new CellRendererText(), "text", 3);
			nodeview2.AppendColumn("Papellido Materno", new CellRendererText(), "text", 4);
			nodeview2.EnableGridLines = TreeViewGridLines.Horizontal;
			ListStore TipoDeListado;
			TipoDeListado = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
		}
		private void listaFullMaestro(){
			ListStore TipoDeListado;
			TipoDeListado = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
			nodeview2.Model = TipoDeListado;

			this.abrirConexion();
			MySqlCommand myCommand = new MySqlCommand(
				"SELECT * FROM `Maestro`", myConnection);
			MySqlDataReader myReader = myCommand.ExecuteReader();	
			while (myReader.Read()){
				string id = myReader["id"].ToString();
				string codigo = myReader["codigo"].ToString();
				string nombre = myReader["nombre"].ToString();
				string apellidoP = myReader["apellidoP"].ToString();
				string apellidoM = myReader["apellidoM"].ToString();
				TipoDeListado.AppendValues(id, codigo, nombre,apellidoP,apellidoM);
			}
			myReader.Close();
			myReader = null;
			myCommand.Dispose();
			myCommand = null;
			this.cerrarConexion();
		}
		protected void OnCargerAlumnoClicked (object sender, EventArgs e)
		{
			TreeModel model;
			TreeIter iterSelected;
			if(nodeview1.Selection.GetSelected (out model, out iterSelected)){
				string id = (string)model.GetValue(iterSelected, 0);
				this.abrirConexion ();
				MySqlCommand myCommand = new MySqlCommand ("SELECT * FROM `Estudiante` WHERE (`id`='" + id + "')", 
					myConnection);
				MySqlDataReader myReader = myCommand.ExecuteReader ();	
				while (myReader.Read ()) {
					string codigo = myReader ["codigo"].ToString ();
					string nombre = myReader ["nombre"].ToString ();
					string apellidoP = myReader ["apellidoP"].ToString ();
					string apellidoM = myReader ["apellidoM"].ToString ();
					idTxt.Text = id.ToString ();
					codTxt.Text = codigo.ToString ();
					nomTxt.Text = nombre.ToString ()+" "+apellidoP.ToString ()+" "+apellidoM.ToString ();

				}
				myReader.Close ();
				myReader = null;
				myCommand.Dispose ();
				myCommand = null;
				this.cerrarConexion ();
				this.Materias(id);
			}
		}
		private void nodeMat(){
			ListStore TipoDeListado;
			TipoDeListado = new ListStore (typeof(string), typeof(string));
			nodeMateria.Model = TipoDeListado;
			nodeMateria.AppendColumn("ID", new CellRendererText(), "text", 0);
			nodeMateria.AppendColumn("Nombre", new CellRendererText(), "text", 1);
			nodeMateria.EnableGridLines = TreeViewGridLines.Horizontal;
		}
		private void Materias(string id){
			ListStore TipoDeListado;
			TipoDeListado = new ListStore (typeof(string), typeof(string));
			nodeMateria.Model = TipoDeListado;

			this.abrirConexion();
			MySqlCommand myCommand = new MySqlCommand(
				"SELECT * FROM `Materia` WHERE (`id_Estudiante`='" + id + "_Estudiante')", myConnection);
			MySqlDataReader myReader = myCommand.ExecuteReader();	
			while (myReader.Read()){
				string idMat = myReader["id"].ToString();
				string nombre = myReader["nombre"].ToString();
				TipoDeListado.AppendValues(idMat, nombre);
			}

			myReader.Close();
			myReader = null;
			myCommand.Dispose();
			myCommand = null;
			this.cerrarConexion();


		}
		protected void OnCargarMaestroClicked (object sender, EventArgs e)
		{
			TreeModel model;
			TreeIter iterSelected;
			if(nodeview2.Selection.GetSelected (out model, out iterSelected)){
				string id = (string)model.GetValue(iterSelected, 0);
				this.abrirConexion ();
				MySqlCommand myCommand = new MySqlCommand ("SELECT * FROM `Maestro` WHERE (`id`='" + id + "')", 
					myConnection);
				MySqlDataReader myReader = myCommand.ExecuteReader ();	
				while (myReader.Read ()) {
					string codigo = myReader ["codigo"].ToString ();
					string nombre = myReader ["nombre"].ToString ();
					string apellidoP = myReader ["apellidoP"].ToString ();
					string apellidoM = myReader ["apellidoM"].ToString ();
					idTxt2.Text = id.ToString ();
					codTxt2.Text = codigo.ToString ();
					nomTxt2.Text = nombre.ToString ()+" "+apellidoP.ToString ()+" "+apellidoM.ToString ();
				}
				myReader.Close ();
				myReader = null;
				myCommand.Dispose ();
				myCommand = null;
				this.cerrarConexion ();
			}
		}

		protected void OnPagarClicked (object sender, EventArgs e)
		{
			string id_Estudiante = idTxt.Text;
			string id_Maestro = idTxt2.Text;
			string codigo = codigoMateria.Text;
			string nombre = nomMat.Text;
			string area = areaMat.Text;
			string horas = horasMat.Text;

			if(codigo == "" || area == "" || nombre == "" || horas  == "0" || id_Estudiante  == "" || id_Maestro  == ""){
				MessageDialog md = new MessageDialog (this, DialogFlags.Modal, 
					MessageType.Info, 
					ButtonsType.Ok, 
					"\nAlguno de tus campos estan vasios");
				md.Run ();md.Destroy ();

			}else{
				this.abrirConexion();
				string sql = "INSERT INTO `Materia` (`id_Estudiante`, `id_Maestro`, `codigo`, `nombre`, `area`, `horas`) " +
				             "VALUES ('" + id_Estudiante + "', '" + id_Maestro + "', '" + codigo + "', '" + nombre + "', '" + area + "', '" + horas + "')";
				this.ejecutarComando(sql);
				this.cerrarConexion();
				MessageDialog md = new MessageDialog (this, DialogFlags.Modal, 
					MessageType.Info, 
					ButtonsType.Ok, 
					"Sea guardado exitosamente la Materia Nueva");
				md.Run ();md.Destroy ();
				string id = idTxt.Text;
				this.Materias(id);
			}
		}

		protected void OnEliminarClicked (object sender, EventArgs e)
		{
			TreeModel model;
			TreeIter iterSelected;
			if(nodeMateria.Selection.GetSelected (out model, out iterSelected)){
				string id = (string)model.GetValue(iterSelected, 0);
				MessageDialog md = new MessageDialog (this, DialogFlags.Modal, 
					MessageType.Warning, 
					ButtonsType.YesNo, 
					"¿Realmente desea eliminarlo?");
				ResponseType boton = (ResponseType)md.Run ();
				md.Destroy ();
				if (boton == ResponseType.Yes) { 
					this.abrirConexion();
					string sql = "DELETE FROM `Materia` WHERE (`id`='" + id + "')";
					this.ejecutarComando(sql);
					this.cerrarConexion();
					id = idTxt.Text;
					this.Materias(id);
				}
				if (boton == ResponseType.No) {

				}
			}
		}

		protected void OnCancelarClicked (object sender, EventArgs e)
		{
			this.Visible = false;
		}
	}
}

