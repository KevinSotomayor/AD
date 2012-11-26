using System;
using Gtk;
using System.Data;
using Npgsql;
using System.Collections.Generic; 
using PArticuloMejorado;

namespace PArticuloMejorado
{
	public partial class VentanaEditar : Gtk.Window
	{
		public VentanaEditar () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

