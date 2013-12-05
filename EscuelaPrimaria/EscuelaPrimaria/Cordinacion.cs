using System;
using Gtk;

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
		}
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			MainWindow win = new MainWindow ();
			win.Visible = true;
		}
	}
}

