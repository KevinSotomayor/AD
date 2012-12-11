using Gtk;
using Npgsql;
using PArticulo; //-> Utilizara las clases de Particulo
using Serpis.Ad;
using System;
using System.Data;
using System.Collections.Generic; 

public partial class MainWindow: Gtk.Window
{	
	
	//private IDbConnection dbConnection;
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		string connectionString = "Server=localhost;Database=dbprueba;User Id=dbprueba;Password=root";
	//	IDbConnection dbConnection = new NpgsqlConnection(connectionString);
		AplicationDbContext.Instance.DbConnection = new NpgsqlConnection(connectionString);
		AplicationDbContext.Instance.DbConnection.Open();

		//dbConnection.Open();
		
		IDbCommand dbCommand = AplicationDbContext.Instance.DbConnection.CreateCommand();
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
		
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		AplicationDbContext.Instance.DbConnection.Close();
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
		long id = getSelectedId();
		
		ArticuloView articuloView = new ArticuloView(id);
		articuloView.Show ();
	//	Console.WriteLine=("id={0}", id);
		
//		IDbCommand dbCommand = dbConnection.CreateCommand();
//		//dbCommand.CommandText ="select * from articulo where id ="+id; //MAL
//		dbCommand.CommandText ="select * from articulo where id=:id"; //sql parameter con nombre,el nombre es lo que hay a la derecha de los :
//		IDbDataParameter dbDataParameter= dbCommand.CreateParameter();
//		dbDataParameter.ParameterName="id";
//		dbCommand.Parameters.Add(dbDataParameter);
//		dbDataParameter.Value=id;
//		
//		
//		IDataReader dataReader = dbCommand.ExecuteReader();
//		
//		ArticuloView articuloView = new ArticuloView();
//		articuloView.Nombre= dataReader["nombre"].ToString();
//		articuloView.Precio= double.Parse (dataReader["precio"].ToString());
//		articuloView.Show();
//		dataReader.Close();

	

	}
	
	private long getSelectedId(){
		TreeIter treeIter;
		treeView.Selection.GetSelected(out treeIter);
			
		ListStore listStore = (ListStore)treeView.Model;
		return long.Parse (listStore.GetValue (treeIter, 0).ToString ()); 
//		object id = listStore.GetValue(treeIter, 0);// columna donde tengo el id
//		return long.Parse(id.ToString()); //conversion (cast) ya que id es object
	}
}