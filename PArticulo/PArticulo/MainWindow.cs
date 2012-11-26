using Gtk;
using Npgsql;
using PArticulo; //-> Utilizara las clases de Particulo
using Serpis.Ad;
using System;
using System.Data;
using System.Collections.Generic; 

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		string connectionString = "Server=localhost;Database=dbprueba;User Id=dbprueba;Password=root";
		IDbConnection dbConnection = new NpgsqlConnection(connectionString);
		dbConnection.Open();
		
		IDbCommand dbCommand = dbConnection.CreateCommand();
		dbCommand.CommandText="select * from articulo";
		IDataReader dataReader = dbCommand.ExecuteReader();
		
		TreeViewExtensions.Fill(treeView, dataReader);
		dataReader.Close();
		
		dataReader = dbCommand.ExecuteReader();
		TreeViewExtensions.Fill(treeView, dataReader);
		dataReader.Close();
		dbConnection.Close();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}


	protected void OnClearActionActivated (object sender, System.EventArgs e)
	{
		ListStore listStore = (ListStore)treeView.Model;
		listStore.Clear();
	}
}