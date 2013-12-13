using System;
using Gtk;
using MySql.Data.MySqlClient;

namespace EscuelaPrimaria
{
	public partial class Pagos : Gtk.Window
	{
		private Cordinacion padre;
		public Pagos (Cordinacion padre, string id) : 
		base (Gtk.WindowType.Toplevel)
		{
			this.padre = padre;
			this.Build ();
			this.listaFull();
			this.node();
			this.Title = "Cordinacion con ID " + id.ToString();
			idCor.Text = id.ToString ();
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

		protected void OnCargerClicked (object sender, EventArgs e)
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
			}
		}



		protected void OnCanselarClicked (object sender, EventArgs e)
		{
			this.Visible = false;
		}

		protected void OnPagarClicked (object sender, EventArgs e)
		{
			string id_Estudiante = idTxt.Text;
			string id_cordinacion = idCor.Text;
			string codigo = codigoFolio.Text;
			string fecha = dia.Text +"/"+ mes.Text + "/" + ano.Text;
			string tipoPago = null;
			string importe = importe1.Text;
			string observaciones = comentario.Buffer.Text;

			if(this.efectivo.Active){
				tipoPago = "Efectivo";
			}
			if(this.tarjeta.Active){
				tipoPago = "Tarjeta de Credito";
			}
			if(this.otro.Active){
				tipoPago = "Otro";
			}

			if(codigo == "" || fecha == "//" || tipoPago == "" || importe  == "" || id_Estudiante  == "" || id_cordinacion  == ""){
				MessageDialog md = new MessageDialog (this, DialogFlags.Modal, 
					MessageType.Info, 
					ButtonsType.Ok, 
					"\nAlguno de tus campos estan vasios");
				md.Run ();md.Destroy ();

			}else{
				this.abrirConexion();
				string sql = "INSERT INTO `Pagos` (`id_Estudiante`, `id_cordinacion`, `codigo`, `fecha`, `tipoPago`, `importe`, `observaciones`) " +
				             "VALUES ('" + id_Estudiante + "', '" + id_cordinacion + "', '" + codigo + "', '" + fecha + "', '" + tipoPago + "', '" + importe + "', '" + observaciones + "')";
				this.ejecutarComando(sql);
				this.cerrarConexion();
				MessageDialog md = new MessageDialog (this, DialogFlags.Modal, 
					MessageType.Info, 
					ButtonsType.Ok, 
					"Sea guardado exitosamente el pago");
				md.Run ();md.Destroy ();
			}
		}
	}
}
