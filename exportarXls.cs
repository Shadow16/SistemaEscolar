//**************************** LIBRERIA*************************//
// Microsoft.office.Interop.Excel 

using System;
using Gtk;
using MySql.Data.MySqlClient;
using Pango;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace EscuelaPrimaria
{
	public class imprimir
	{
		public imprimir ()
		{
			Microsoft.Office.Interop.Excel.Application Excel_Aplication;
			Excel._Workbook HojaTrabajo;
			Excel._Worksheet PestañaTrabajo;

			Excel_Aplication = new Microsoft.Office.Interop.Excel.ApplicationClass ();

			object misValue = System.Reflection.Missing.Value;
			HojaTrabajo = Excel_Aplication.Workbooks.Add (misValue);
			PestañaTrabajo = (Excel.Worksheet)HojaTrabajo.ActiveSheet;

			int i = 0, j = 0;
			for (i = 1; i < passwordText; i++) {
				PestañaTrabajo.Cells[1, i] = HojaTrabajo[i - 1]; 
			}

			for (i = 0; i <= nodeview1.RowExpanded - 1; i++) {
				for (j = 0; j <= nodeview1.ColumnsAutosize - 1; j++) {
					string cell = nodeview1 [j, i];
					PestañaTrabajo.Cells [i + 2, j + 1];
				}
			}
			string Carpeta = "'C:\'";
			if (!Directory.Exists (Carpeta)) {
				Directory.CreateDirectory (Carpeta);

			}

			string archivo = Carpeta + "Alumnos.xls";
			try 
			{
				HojaTrabajo.SaveAs(archivo, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
				System.Diagnostics.Process.Start(Carpeta);
			}
			catch {
			}

		}
	}
}

