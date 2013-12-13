using System;
using Gtk;
using MySql.Data.MySqlClient;

namespace EscuelaPrimaria
{
	public partial class Grado : Gtk.Window
	{
		private Cordinacion padre;
		public Grado (Cordinacion padre) : 
		base (Gtk.WindowType.Toplevel)
		{
			this.padre = padre;
			this.Build ();
			this.listaFullAlumno();
			this.nodeAlumno();
			this.listaFullMaestro();
			this.nodeMaestro();
			this.nodeGrad ();
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
				this.grado(id);
			}
		}
		private void nodeGrad(){
			ListStore TipoDeListado;
			TipoDeListado = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string));
			nodeGrado.Model = TipoDeListado;
			nodeGrado.AppendColumn("ID", new CellRendererText(), "text", 0);
			nodeGrado.AppendColumn("Grado", new CellRendererText(), "text", 1);
			nodeGrado.AppendColumn("Aula", new CellRendererText(), "text", 2);
			nodeGrado.AppendColumn("Edificio", new CellRendererText(), "text", 3);
			nodeGrado.EnableGridLines = TreeViewGridLines.Horizontal;
		}
		private void grado(string id){
			ListStore TipoDeListado;
			TipoDeListado = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string));
			nodeGrado.Model = TipoDeListado;

			this.abrirConexion();
			MySqlCommand myCommand = new MySqlCommand(
				"SELECT * FROM `Grado` WHERE (`id_Estudiante`='" + id + "_Estudiante')", myConnection);
			MySqlDataReader myReader = myCommand.ExecuteReader();	
			while (myReader.Read()){
				string idGrado = myReader["id"].ToString();
				string numGrado = myReader["numGrado"].ToString();
				string aula = myReader["aula"].ToString();
				string edificio = myReader["edificio"].ToString();
				TipoDeListado.AppendValues(idGrado, numGrado, aula, edificio);
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
				CargarMaestro (id);
			}
		}
		protected void CargarMaestro(string id){
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
		protected void OnEliminarClicked (object sender, EventArgs e)
		{
			TreeModel model;
			TreeIter iterSelected;
			if(nodeGrado.Selection.GetSelected (out model, out iterSelected)){
				string id = (string)model.GetValue(iterSelected, 0);
				MessageDialog md = new MessageDialog (this, DialogFlags.Modal, 
					MessageType.Warning, 
					ButtonsType.YesNo, 
					"¿Realmente desea eliminarlo?");
				ResponseType boton = (ResponseType)md.Run ();
				md.Destroy ();
				if (boton == ResponseType.Yes) { 
					this.abrirConexion();
					string sql = "DELETE FROM `Grado` WHERE (`id`='" + id + "')";
					this.ejecutarComando(sql);
					this.cerrarConexion();
					id = idTxt.Text;
					this.grado(id);
				}
				if (boton == ResponseType.No) {

				}
			}
		}

		protected void OnCancelarClicked (object sender, EventArgs e)
		{
			this.Visible = false;
		}

		protected void OnAgregarClicked (object sender, EventArgs e)
		{
			string id_Estudiante = idTxt.Text;
			string id_Maestro = idTxt2.Text;
			string numGrado = gradoTxt.Text;
			string edificio = edificioTxt.Text;
			string aula = aulaTxt.Text;

			if(numGrado == "" || edificio == "" || aula == ""){
				MessageDialog md = new MessageDialog (this, DialogFlags.Modal, 
					MessageType.Info, 
					ButtonsType.Ok, 
					"\nAlguno de tus campos estan vasios");
				md.Run ();md.Destroy ();

			}else{
				this.abrirConexion();
				string sql = "INSERT INTO `Grado` (`id_Estudiante`, `id_Maestro`, `numGrado`, `aula`, `edificio`) " +
				             "VALUES ('" + id_Estudiante + "', '" + id_Maestro + "', '" + numGrado + "', '" + aula + "', '" + edificio + "')";
				this.ejecutarComando(sql);
				this.cerrarConexion();
				MessageDialog md = new MessageDialog (this, DialogFlags.Modal, 
					MessageType.Info, 
					ButtonsType.Ok, 
					"Sea guardado exitosamente el Grado Nueva");
				md.Run ();md.Destroy ();
				string id = idTxt.Text;
				this.grado(id);
			}
		}

		protected void OnActualizarClicked (object sender, EventArgs e)
		{
			string id_Maestro = idTxt2.Text;
			string numGrado = gradoTxt.Text;
			string edificio = edificioTxt.Text;
			string aula = aulaTxt.Text;
			TreeModel model;
			TreeIter iterSelected;
			if (nodeGrado.Selection.GetSelected (out model, out iterSelected)) {
				string id = (string)model.GetValue (iterSelected, 0);
				if(numGrado == "" || edificio == "" || aula == "" || id_Maestro == ""){
					MessageDialog md = new MessageDialog (this, DialogFlags.Modal, 
						MessageType.Info, 
						ButtonsType.Ok, 
						"\nAlguno de tus campos estan vasios");
					md.Run ();md.Destroy ();

				}else{
					this.abrirConexion();
					string sql = "UPDATE `Grado` SET `id_Maestro`='" + id_Maestro + "', `numGrado`='" + numGrado + "', " +
					             "`edificio`='" + edificio + "',`aula`='" + aula + "' WHERE (`id`='" + id + "')";
					this.ejecutarComando(sql);
					this.cerrarConexion();
					MessageDialog md = new MessageDialog (this, DialogFlags.Modal, 
						MessageType.Info, 
						ButtonsType.Ok, 
						"Sea Actualizado exitosamente el Grado");
					md.Run ();md.Destroy ();
					id = idTxt.Text;
					this.grado(id);
				}
			}
		}

		protected void OnCargarClicked (object sender, EventArgs e)
		{
			TreeModel model;
			TreeIter iterSelected;
			if (nodeGrado.Selection.GetSelected (out model, out iterSelected)) {
				string id = (string)model.GetValue (iterSelected, 0);
				this.abrirConexion ();
				MySqlCommand myCommand = new MySqlCommand ("SELECT * FROM `Grado` WHERE (`id`='" + id + "')", 
					myConnection);
				MySqlDataReader myReader = myCommand.ExecuteReader ();	
				while (myReader.Read ()) {
					string id_Maestro = myReader ["id_Maestro"].ToString ();
					string numGrado = myReader ["numGrado"].ToString ();
					string edificio = myReader ["edificio"].ToString ();
					string aula = myReader ["aula"].ToString ();
					idTxt2.Text = id_Maestro.ToString ();
					gradoTxt.Text = numGrado.ToString ();
					edificioTxt.Text = edificio.ToString ();
					aulaTxt.Text = aula.ToString ();
				}
				myReader.Close ();
				myReader = null;
				myCommand.Dispose ();
				myCommand = null;
				this.cerrarConexion ();
				id = idTxt2.Text;
				CargarMaestro (id);
			}
		}
	}
}

