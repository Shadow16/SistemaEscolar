using System;
using Gtk;
using MySql.Data.MySqlClient;

namespace EscuelaPrimaria
{
	public partial class EditarCoordinacion : Gtk.Window
	{
		private Cordinacion padre;
		public EditarCoordinacion (Cordinacion padre) : 
		base (Gtk.WindowType.Toplevel)
		{
			this.padre = padre;
			this.Build ();
			this.listaFull();
			this.node();
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
		private void node(){
			nodeview1.AppendColumn("ID", new CellRendererText(), "text", 0);
			nodeview1.AppendColumn("Código", new CellRendererText(), "text", 1); 
			nodeview1.AppendColumn("Nombre", new CellRendererText(), "text", 2);
			nodeview1.AppendColumn("Apellido Paterno", new CellRendererText(), "text", 3);
			nodeview1.AppendColumn("Papellido Materno", new CellRendererText(), "text", 4);
			nodeview1.EnableGridLines = TreeViewGridLines.Horizontal;
			ListStore TipoDeListado;
			TipoDeListado = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
		}
		private void listaFull(){
			ListStore TipoDeListado;
			TipoDeListado = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
			nodeview1.Model = TipoDeListado;

			this.abrirConexion();
			MySqlCommand myCommand = new MySqlCommand(
				"SELECT * FROM `Cordinacion`", myConnection);
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

		protected void OnCargerClicked (object sender, EventArgs e)
		{
			TreeModel model;
			TreeIter iterSelected;
			if(nodeview1.Selection.GetSelected (out model, out iterSelected)){
				string id = (string)model.GetValue(iterSelected, 0);
				this.abrirConexion ();
				MySqlCommand myCommand = new MySqlCommand ("SELECT * FROM `Cordinacion` WHERE (`id`='" + id + "')", 
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
					string cargo = myReader ["cargo"].ToString ();
					string usuario = myReader ["usuario"].ToString ();
					string password = myReader ["password"].ToString ();
					idTxt.Text = id.ToString ();
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
					cargoTxt.Text = cargo.ToString ();
					usuarioText.Text = usuario.ToString ();
					passwordText.Text = password.ToString ();
				}

				myReader.Close ();
				myReader = null;
				myCommand.Dispose ();
				myCommand = null;
				this.cerrarConexion ();
			}
		}

		protected void OnGuardarClicked (object sender, EventArgs e)
		{
			try 
			{
				string id = idTxt.Text;
				string codigo = codTxt.Text;
				string nombre = nomTxt.Text;
				string apellidoP = paternoTxt.Text;
				string apellidoM = maternoTxt.Text;
				string calle = calleTxt.Text;
				string numero = numExtTxt.Text;
				string numeroInt = numIntTex.Text;
				string ciudad = ciudadText.Text;
				string colonia = colTxt.Text;
				string telefono = telTxt.Text;
				string avisarA = aviTxt.Text;
				string telEm = telEmTxt.Text;
				string usuario = usuarioText.Text;
				string password = passwordText.Text;
				string cargo = cargoTxt.Text;

				if(codigo == "" || nombre == "" || telefono == "" || apellidoP  == "" || apellidoM == "" || calle == "" || numero == "" || numeroInt == "" || 
					ciudad == "" || colonia == "" || telefono == "" || avisarA == "" || telEm == "" || usuario == "" || password == "" || cargo == ""){
					MessageDialog md = new MessageDialog (this, DialogFlags.Modal, 
						MessageType.Info, 
						ButtonsType.Ok, 
						"\nAlguno de tus campos estan vasios");
					md.Run ();md.Destroy ();

				}else{
					this.abrirConexion();
					string sql = "UPDATE `Cordinacion` SET `codigo`='" + codigo + "', `nombre`='" + nombre + "', `apellidoP`='" + apellidoP + "', " +
					             "`apellidoM`='" + apellidoM + "', `calle`='" + calle + "', `numero`='" + numero + "', `numeroInt`='" + numeroInt + "', " +
					             "`ciudad`='" + ciudad + "', `colonia`='" + colonia + "', `telefono`='" + telefono + "', `avisarA`='" + avisarA + "', " +
					             "`telEm`='" + telEm + "', `cargo`='" + cargo + "',`usuario`='" + usuario + "', `password`='" + password + "' WHERE (`id`='" + id + "')";
					this.ejecutarComando(sql);
					this.cerrarConexion();
					MessageDialog md = new MessageDialog (this, DialogFlags.Modal, 
						MessageType.Info, 
						ButtonsType.Ok, 
						"Sea guardado exitosamente");
					md.Run ();md.Destroy ();
					this.listaFull();
				}
			}catch(Exception h)
			{
				MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.None,"Exception: " + h.Message);md.Show();
			}
		}

		protected void OnEliminarClicked (object sender, EventArgs e)
		{
			TreeModel model;
			TreeIter iterSelected;
			if(nodeview1.Selection.GetSelected (out model, out iterSelected)){
				string id = (string)model.GetValue(iterSelected, 0);
				MessageDialog md = new MessageDialog (this, DialogFlags.Modal, 
					MessageType.Warning, 
					ButtonsType.YesNo, 
					"¿Realmente desea eliminarlo?");
				ResponseType boton = (ResponseType)md.Run ();
				md.Destroy ();
				if (boton == ResponseType.Yes) { 
					this.abrirConexion();
					string sql = "DELETE FROM `Cordinacion` WHERE (`id`='" + id + "')";
					this.ejecutarComando(sql);
					this.cerrarConexion();
					listaFull();
				}
				if (boton == ResponseType.No) {

				}
			}
		}

		protected void OnCanselarClicked (object sender, EventArgs e)
		{
			this.Visible = false;
		}
	}
}

