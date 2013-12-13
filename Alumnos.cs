using System;
using Gtk;
using MySql.Data.MySqlClient;

namespace EscuelaPrimaria
{
	public partial class Alumnos : Gtk.Window
	{
		private MainWindow padre;
		public Alumnos (MainWindow padre,string id) : 
			base (Gtk.WindowType.Toplevel)
		{
			this.padre = padre;
			this.Build ();
			this.Title = "Alumno con ID " + id.ToString();
			this.Cargar (id);

		}
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			MessageDialog md = new MessageDialog (this, DialogFlags.Modal, 
				MessageType.Warning, 
				ButtonsType.YesNo, 
				"¿Realmente desea salir con el registro con el ID "+id+"?");

			if (boton == ResponseType.Yes) { 
				ResponseType boton = (ResponseType)md.Run ();
				md.Destroy ();
				MainWindow win = new MainWindow ();
				win.Visible = true;
			}
			if (boton == ResponseType.No) {

			}
		}
		protected MySqlConnection myConnection;
		protected void abrirConexion(){
			string connectionString =
				"Server=localhost;" +
				"Database=SistemaEscolar;" +
				"User ID=root;" +
				"Password=;" +
				"Pooling=false;";
			this.myConnection = new MySqlConnection(connectionString);
			this.myConnection.Open();
		}

		protected void cerrarConexion(){
			this.myConnection.Close(); 
			this.myConnection = null;
		}
		private int ejecutarComando(string sql){
			MySqlCommand myCommand = new MySqlCommand(sql,this.myConnection);
			int afectadas = myCommand.ExecuteNonQuery();
			myCommand.Dispose();
			myCommand = null;
			return afectadas;
		}
		public void Cargar(string id){
			this.abrirConexion();
			MySqlCommand myCommand = new MySqlCommand("SELECT * FROM `Estudiante` WHERE (`id`='" + id + "')", 
				myConnection);
			MySqlDataReader myReader = myCommand.ExecuteReader();	
			while (myReader.Read()){
				string codigo = myReader["codigo"].ToString();
				string nombre = myReader["nombre"].ToString();
				string apellidoP = myReader["apellidoP"].ToString();
				string apellidoM = myReader["apellidoM"].ToString();
				string calle = myReader["calle"].ToString();
				string numero = myReader["numero"].ToString();
				string numeroInt = myReader["numeroInt"].ToString();
				string colonia = myReader["colonia"].ToString();
				string telefono = myReader["telefono"].ToString();
				string avisarA = myReader["avisarA"].ToString();
				string telEm = myReader["telEm"].ToString();
				codTxt.Text = codigo.ToString();
				nomTxt.Text = nombre.ToString();
				paternoTxt.Text = apellidoP.ToString();
				maternoTxt.Text = apellidoM.ToString();
				calleTxt.Text = calle.ToString();
				numExtTxt.Text = numero.ToString();
				numIntTex.Text = numeroInt.ToString();
				colTxt.Text = colonia.ToString();
				telTxt.Text = telefono.ToString();
				aviTxt.Text = avisarA.ToString();
				telEmTxt.Text = telEm.ToString();
			}

			myReader.Close();
			myReader = null;
			myCommand.Dispose();
			myCommand = null;
			this.cerrarConexion();
		}
	}
}

