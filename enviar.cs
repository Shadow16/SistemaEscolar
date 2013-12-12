//********************************* LIBRERIA *********************************//
//   System.Net;

using System;
using Gtk;
using MySql.Data.MySqlClient;
using Pango;
using System.Text;
using System.Net.Mail;
using System.Data;
using System.Linq;


namespace EscuelaPrimaria
{
	public partial class enviar : Gtk.Window
	{

		public enviar () : 
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}


		protected void OnBtn2Clicked (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void OnBtn1Clicked (object sender, EventArgs e)
		{
			MailMessage mail = new MailMessage (MailAddress = asunto.Text, 
			MailAddress = body.Text);
			SmtpClient cliente = new SmtpClient (de.Text);
			cliente.Port = 587;
			cliente.Credentials = new System.Net.NetworkCredential (user.Text, pass.Text);
			 
		}
	}
}
