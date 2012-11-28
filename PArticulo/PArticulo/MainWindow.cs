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
		dbCommand.CommandText=	"select a.id, a.nombre, a.precio, c.nombre as Categoria " +
								"from articulo a left join categoria c " +
								"on a.categoria = c.id";
				//Inner join = union interna 
				//left join, muestra todos los articulos de la tabla de la izquierda y los de la derecha solo los que cumplen la condicion ON
				//right join, muestra todos los articulos de la tabla de la derecha y los de la izquierda solo los que cumplen la condicion ON
				//full join, muestra todos los de la derecha y todos los de la izquierda.
				//el full lo tiene solo postgreesql, mysql NO!
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
	protected void OnEditActionActivated (object sender, EventArgs e)
	{
		ArticuloView articuloView = new ArticuloView();
		articuloView.Show();
	}

}