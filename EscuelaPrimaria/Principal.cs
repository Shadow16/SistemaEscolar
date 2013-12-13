using System;

namespace EscuelaPrimaria
{
	public partial class Principal : Gtk.Window
	{
		private MainWindow padre;
		public Principal (MainWindow padre) : 
			base (Gtk.WindowType.Toplevel)
		{
			this.padre = padre;
			this.Build ();
		}
	}
}

