using Gtk;
using Npgsql;
using System;
using System.Data;
using System.Collections.Generic; 

public partial class MainWindow: Gtk.Window
{	
		private ListStore listStore;
		TextBuffer buffer ;
		

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		////////////////////////////////

		//Conexion a la base de datos.
		String connectionString = "Server=localhost;Database=dbprueba;User Id=dbprueba;Password=root";
		IDbConnection dbConnection = new NpgsqlConnection(connectionString);
		dbConnection.Open();

		IDbCommand dbCommand = dbConnection.CreateCommand();
		dbCommand.CommandText="select * from categoria";
		IDataReader dataReader = dbCommand.ExecuteReader();


		for(int index = 0; index < dataReader.FieldCount; index ++){
			comboBox.PackStart(new CellRendererText(), false);
			comboBox.AddAttribute(new CellRendererText(), "text", index);
		}; // -> Lo mismo que :
		/*
		CellRenderer  cellRenderer = new CellRendererText();
		comboBox.PackStart(cellRenderer, false); //expand = false
		comboBox.AddAttribute(cellRenderer, "text", 0);
		
		CellRenderer  cellRenderer2 = new CellRendererText();
		comboBox.PackStart(cellRenderer2, false); //expand = false
		comboBox.AddAttribute(cellRenderer2, "text", 1);*/


//		Type[] types = GetTypes(typeof(string), dataReader.FieldCount);
//		ListStore listStore = new ListStore(types);
//		comboBox.Model = listStore;

		listStore = new ListStore(typeof(string), typeof(string));
		comboBox.Model = listStore;

		////////////////////////////
//		 List<string> values = new List<string>();
//		 while(dataReader.Read()){
//			for(int index = 0; index < dataReader.FieldCount; index++)
//				values.Add(dataReader[index].ToString());
//		}
		///////////////////////////

		listStore.AppendValues("1", "Uno");
		listStore.AppendValues("2", "Dos");
		
		comboBox.Changed += delegate {showItemSelected(listStore);};

		dataReader.Close();
		dbConnection.Close();
	
	}
		
	
	private void showItemSelected(ListStore listStore){
			Console.WriteLine("Evento activado");
			TreeIter treeIter;
			if (comboBox.GetActiveIter(out treeIter) ){
				object value =listStore.GetValue(treeIter, 0);
				buffer = textView.Buffer;
				buffer.Text="Acabas de seleccionar:\n" + "La opcion " + value;
				Console.WriteLine("ComboBoxChanged id={0}", value);
   			}
	}

	public static Type[] GetTypes(Type type, int count){

			List<Type> types = new List<Type>();
			for(int index = 0; index < count; index ++)
				types.Add(type);
			return types.ToArray();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
