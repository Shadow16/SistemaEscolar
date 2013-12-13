using System;
using Gtk;
using MySql.Data.MySqlClient;
using Pango;


namespace EscuelaPrimaria
{
	public partial class Cordinacion : Gtk.Window
	{
		private MainWindow padre;
		public Cordinacion (MainWindow padre,string id) : 
			base (Gtk.WindowType.Toplevel)
		{
			this.padre = padre;
			this.Build ();
			this.Title = "Cordinacion con ID " + id.ToString();
			this.Cargar (id);
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
			MySqlCommand myCommand = new MySqlCommand ("SELECT * FROM `Cordinacion` WHERE (`id`='" + id + "')", 
				myConnection);
			MySqlDataReader myReader = myCommand.ExecuteReader ();	
			while (myReader.Read ()) {
				string codigo = myReader ["codigo"].ToString ();
				string nombre = myReader ["nombre"].ToString ();
				string apellidoP = myReader ["apellidoP"].ToString ();
				string apellidoM = myReader ["apellidoM"].ToString ();
				string cargo = myReader ["cargo"].ToString ();
				this.Title = "Bienvenido " + nombre.ToString ()
				             + " " + apellidoP.ToString () + " " + apellidoM.ToString ();
				labelNombre.Text = "Bienvenido " + nombre.ToString ()
				                   + " " + apellidoP.ToString () + " " + apellidoM.ToString ();
				labelNombre.ModifyFont (FontDescription.FromString ("Arial Bold 12"));
				cargoText.Text = "Puesto: " + cargo.ToString ();
				cargoText.ModifyFont (FontDescription.FromString ("Arial Bold 10"));
				idTexto.Text = id.ToString ();
			}

			myReader.Close ();
			myReader = null;
			myCommand.Dispose ();
			myCommand = null;
			this.cerrarConexion ();
		}

		protected void OnButtonNuevoClicked (object sender, EventArgs e)
		{
			NuevoAlumno alum = new NuevoAlumno (this);
			alum.Show();
		}

		protected void OnButton5Clicked (object sender, EventArgs e)
		{
			NuevoProfesor prof = new NuevoProfesor (this);
			prof.Show ();
		}

		protected void OnButton9Clicked (object sender, EventArgs e)
		{
			NuevoCordinacion cord = new NuevoCordinacion (this);
			cord.Show ();
		}

		protected void OnEditarAlumnoClicked (object sender, EventArgs e)
		{
			EditarAlumno edAlum = new EditarAlumno (this);
			edAlum.Show ();
		}

		protected void OnButton3Clicked (object sender, EventArgs e)
		{
			string id = idTexto.Text;
			Pagos pag = new Pagos (this, id);
			pag.Show();
		}

		protected void OnButton4Clicked (object sender, EventArgs e)
		{
			Materia mat = new Materia (this);
			mat.Show ();
		}

		protected void OnButton8Clicked (object sender, EventArgs e)
		{
			Cursos cur = new Cursos (this);
			cur.Show ();
		}

		protected void OnButton1Clicked (object sender, EventArgs e)
		{
			Grado gra = new Grado (this);
			gra.Show ();
		}

		protected void OnButton6Clicked (object sender, EventArgs e)
		{
			EditarProfesor ep = new EditarProfesor (this);
			ep.Show ();
		}

		protected void OnButton7Clicked (object sender, EventArgs e)
		{
			string id = idTexto.Text;
			Nomina nomi = new Nomina (this, id);
			nomi.Show ();
		}

		protected void OnButton10Clicked (object sender, EventArgs e)
		{
			EditarCoordinacion ecoo = new EditarCoordinacion (this);
			ecoo.Show ();
		}
	}
}

