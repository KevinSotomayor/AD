using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	//http://nhforge.org/wikis/howtonh/your-first-nhibernate-based-application.aspx
	//http://nhforge.org/doc/nh/en/index.html
	}
}
