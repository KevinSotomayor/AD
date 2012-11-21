using System;
using Gtk;


public partial class MainWindow: Gtk.Window
{	
	private ListStore listStore;
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		//comboBox.Active = 0; -> Para establecer como predeterminado un item del combo
		CellRenderer  cellRenderer = new CellRendererText();
		comboBox.PackStart(cellRenderer, false); //expand = false
		comboBox.AddAttribute(cellRenderer, "text", 0);
		
		CellRenderer  cellRenderer2 = new CellRendererText();
		comboBox.PackStart(cellRenderer2, false); //expand = false
		comboBox.AddAttribute(cellRenderer2, "text", 1);
		
		listStore = new ListStore(typeof(string), typeof(string));
		comboBox.Model = listStore;
		
		listStore.AppendValues("1", "uno");
		listStore.AppendValues("2", "dos");
	

		comboBox.Changed += comboBoxChanged;
		
		comboBox.Changed += delegate {showItemSelected(listStore);};
		
		comboBox.Changed += delegate {
			Console.WriteLine("Evento activado");
			TreeIter treeIter;
			if (comboBox.GetActiveIter(out treeIter) ){ // item seleccionado.
				object value =listStore.GetValue(treeIter, 0);
				Console.WriteLine("ComboBox.changed id={0}", value);
			}
		};
	}
		
	
	private void showItemSelected(ListStore listStore){
			Console.WriteLine("Evento activado");
			TreeIter treeIter;
			if (comboBox.GetActiveIter(out treeIter) ){
				object value =listStore.GetValue(treeIter, 0);
				Console.WriteLine("ComboBoxChanged id={0}", value);
   			}
	}
	

	//Tiene que tener esos parametros para que funcione al añadirlo como evento:
	private void comboBoxChanged(object obj, EventArgs args){
			Console.WriteLine("Evento activado");
			TreeIter treeIter;
			if (comboBox.GetActiveIter(out treeIter) ){
				object value =listStore.GetValue(treeIter, 0);
				Console.WriteLine("ComboBoxChanged id={0}", value);
   			}
		
	}
	
	
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
