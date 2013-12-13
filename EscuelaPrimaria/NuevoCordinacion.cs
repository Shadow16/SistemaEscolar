using System;
using Gtk;
using MySql.Data.MySqlClient;

namespace EscuelaPrimaria
{
	public partial class NuevoCordinacion : Gtk.Window
	{
		private Cordinacion padre;
		public NuevoCordinacion (Cordinacion padre) : 
		base (Gtk.WindowType.Toplevel)
		{
			this.padre = padre;
			this.Build ();
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

		protected void OnGuardarClicked (object sender, EventArgs e)
		{
			try 
			{
				string codigo = codTxtEst.Text;
				string nombre = nomTxtEst.Text;
				string apellidoP = paternoTxtEst.Text;
				string apellidoM = maternoTxtEst.Text;
				string calle = calleTxtEst.Text;
				string numero = numExtTxtEst.Text;
				string numeroInt = numIntTexEst.Text;
				string ciudad = ciudadTextEst.Text;
				string colonia = colTxtEst.Text;
				string telefono = telTxtEst.Text;
				string avisarA = aviTxtEst.Text;
				string telEm = telEmTxtEst.Text;
				string cargo = cargoText.Text;
				string usuario = usuarioTextEst.Text;
				string password = passwordTextEst.Text;

				if(codigo == "" || nombre == "" || telefono == "" || apellidoP  == "" || apellidoM == "" || calle == "" || numero == "" || numeroInt == "" || 
					ciudad == "" || colonia == "" || telefono == "" || avisarA == "" || telEm == "" || usuario == "" || password == ""){
					MessageDialog md = new MessageDialog (this, DialogFlags.Modal, 
						MessageType.Info, 
						ButtonsType.Ok, 
						"\nAlguno de tus campos estan vasios");
					md.Run ();md.Destroy ();

				}else{
					this.abrirConexion();
					string sql = "INSERT INTO `Cordinacion` (`codigo`, `nombre`, `apellidoP`, `apellidoM`, `calle`, `numero`, `numeroInt`, `ciudad`, `colonia`, `telefono`, `avisarA`, `telEm`, `cargo`, `usuario`, `password`) " +
					             "VALUES ('" + codigo + "', '" + nombre + "', '" + apellidoP + "', '" + apellidoM + "', '" + calle + "', '" + numero + "', '" + numeroInt + "', '" + ciudad + "', '" + colonia + "', '" + telefono + "', '" + avisarA + "', '" + telEm + "', '" + cargo + "', '" + usuario + "', '" + password + "')";
					this.ejecutarComando(sql);
					this.cerrarConexion();
					MessageDialog md = new MessageDialog (this, DialogFlags.Modal, 
						MessageType.Info, 
						ButtonsType.Ok, 
						"Sea guardado exitosamente");
					md.Run ();md.Destroy ();
					this.Visible = false;
				}
			}catch(Exception h)
			{
				MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.None,"Exception: " + h.Message);md.Show();
			}
		}

		protected void OnCanselarClicked (object sender, EventArgs e)
		{
			this.Visible = false;
		}
	}
}

