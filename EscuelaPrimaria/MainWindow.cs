using System;
using Gtk;
using MySql.Data.MySqlClient;
using EscuelaPrimaria;
using Pango;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		this.Title = "Escuela Primaria";
		label1.ModifyFont (FontDescription.FromString ("Arial Bold 12"));
		label2.ModifyFont (FontDescription.FromString ("Arial Bold 12"));
		label3.ModifyFont (FontDescription.FromString ("Arial Bold 12"));
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
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



	protected void OnEntrarClicked (object sender, EventArgs e)
	{
		try{
			string usuario = nombreTxt.Text;
			string password = null, id = null;
			string tabla = this.selecTxt.ActiveText;
			this.abrirConexion();
			MySqlCommand myCommand = new MySqlCommand("SELECT * FROM `"+tabla+"` WHERE (`usuario`='" + usuario + "')", 
			myConnection);
			MySqlDataReader myReader = myCommand.ExecuteReader();
			while (myReader.Read()){
				password = myReader["password"].ToString();
				id = myReader["id"].ToString();
				Console.WriteLine("Nombre: "+usuario+" password:"+password);
			}
			Console.WriteLine("Nombre: "+usuario+" password:"+password);
			if(nombreTxt.Text == usuario && contraTxt.Text == password)
			{
								string selec = this.selecTxt.ActiveText;
				if(selec == "Estudiante"){
					Alumnos alu = new Alumnos(this, id);
					alu.Show();
					this.Visible = false;
				}else if(selec == "Maestro"){
					Profesor pro = new Profesor(this, id);
					pro.Show();
					this.Visible = false;
				}else if(selec == "Cordinacion"){
					Cordinacion cor = new Cordinacion(this, id);
					cor.Show();
					this.Visible = false;
				}
			}else {
				MessageDialog md = new MessageDialog(this, DialogFlags.Modal, 
					MessageType.Info, 
					ButtonsType.Ok, 
					"\nUsuario o Contraseña incorrectos\t");
				md.Run ();md.Destroy ();
				Console.WriteLine("Datos incorrectos");
			}
			myReader.Close();
			myReader = null;
			myCommand.Dispose();
			myCommand = null;
			this.cerrarConexion();
		}catch(Exception){
			MessageDialog md = new MessageDialog (this, DialogFlags.Modal, 
				MessageType.Info, 
				ButtonsType.Ok, 
				"\n\nNo Existe Conexion con la BASE DE DATOS");
			md.Run ();md.Destroy ();
		}

	}
}
