using System;
using Gtk;

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
			this.Title = "Profesor con ID " + id.ToString();
		}
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			MainWindow win = new MainWindow ();
			win.Visible = true;
		}
	}
}

