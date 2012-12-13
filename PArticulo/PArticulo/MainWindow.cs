using Gtk;
using Npgsql;
using PArticulo;
using Serpis.Ad;
using System;
using System.Collections.Generic;
using System.Data;


public partial class MainWindow: Gtk.Window
{	
	//private IDbConnection dbConnection;
	private IDbConnection dbConnection;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		string connectionString = "Server=localhost;Database=dbprueba;User Id=dbprueba;Password=root";
		//dbConnection = new NpgsqlConnection(connectionString);
		AplicationContext.Instance.DbConnection = new NpgsqlConnection(connectionString);
		dbConnection = AplicationContext.Instance.DbConnection;
		dbConnection.Open ();

		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = 
			"select a.id, a.nombre, a.precio, c.nombre as categoria " +
			"from articulo a left join categoria c " +
			"on a.categoria = c.id";
		
		IDataReader dataReader = dbCommand.ExecuteReader ();
		
		TreeViewExtensions.Fill (treeView, dataReader);
		dataReader.Close ();
		
		dataReader = dbCommand.ExecuteReader ();
		TreeViewExtensions.Fill (treeView, dataReader);
		dataReader.Close ();
		
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		AplicationContext.Instance.DbConnection.Close ();

		Application.Quit ();
		a.RetVal = true;

	}

	protected void OnClearActionActivated (object sender, System.EventArgs e)
	{
		ListStore listStore = (ListStore)treeView.Model;
		listStore.Clear ();
	}

	protected void OnEditActionActivated (object sender, System.EventArgs e){

		long id = getSelectedId();
		ArticuloView articuloView = new ArticuloView(id);
		articuloView.Show ();
	}
	
	private long getSelectedId() {
		TreeIter treeIter;
		treeView.Selection.GetSelected(out treeIter);
		
		ListStore listStore = (ListStore)treeView.Model;
		return long.Parse (listStore.GetValue (treeIter, 0).ToString ()); 
	}
	protected void OnNewActionActivated (object sender, EventArgs e)
	{
		showArticulo(0);
	}

	private void showArticulo(long id){
		ArticuloView articuloView = new ArticuloView(id);
		articuloView.Show ();
	}	


	protected void OnRefreshActionActivated (object sender, EventArgs e){
		Console.WriteLine("Actualizando... Espere... ");

		refresh();


	}

	private void refresh(){
	IDbCommand dbCommand = AplicationContext.Instance.DbConnection.CreateCommand ();
		dbCommand.CommandText = 
			"select a.id, a.nombre, a.precio, c.nombre as categoria " +
			"from articulo a left join categoria c " +
			"on a.categoria = c.id";
		
		IDataReader dataReader = dbCommand.ExecuteReader ();
		
		TreeViewExtensions.Fill (treeView, dataReader);
		dataReader.Close ();
		
		dataReader = dbCommand.ExecuteReader ();
		TreeViewExtensions.Fill (treeView, dataReader);
		dataReader.Close ();
	}


	protected void OnDeleteActionActivated (object sender, EventArgs e){
		long id = getSelectedId();

		IDbCommand dropCommand = AplicationContext.Instance.DbConnection.CreateCommand();
		dropCommand.CommandText="delete from articulo where id=:id" ;

		DbCommandExtensions.AddParameter (dropCommand, "id", id);

		Console.WriteLine("Articulo borrado correctamente");
		dropCommand.ExecuteNonQuery();

		refresh();
	}



}
