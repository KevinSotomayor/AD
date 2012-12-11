using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		//Seleccion multiple:
		treeView.Selection.Mode = SelectionMode.Multiple;
		
		
		treeView.AppendColumn("Colmuna uno", new CellRendererText (), "text", 0);
		treeView.AppendColumn("Colmuna dos", new CellRendererText (), "text", 1);
		
		ListStore listStore = new ListStore(typeof(string), typeof(string));
		
		treeView.Model=listStore;
		
		listStore.AppendValues("clave uno", "valor uno");
		listStore.AppendValues("clave dos", "valor dos");
		listStore.AppendValues("clave tres", "valor tres");
		listStore.AppendValues("clave cuatro", "valor cuatro");

		/// cuando esta seleccionado el treeview, evento.
		treeView.Selection.Changed += delegate {
			int count = treeView.Selection.CountSelectedRows();
			Console.WriteLine("treeView.Selection.Changed Counts.SelectedRows={0}",count);
			
			
			treeView.Selection.SelectedForeach(delegate(TreeModel model, TreePath path, TreeIter iter){
				object value = listStore.GetValue(iter, 0); //item seleccionado.
				Console.WriteLine("value={0}", value);
			
			});                                  
			//TreeIter treeIter;
		//	if ( treeView.Selection.GetSelected(out treeIter)){
		//	}
		};
		
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
