using System;
using Gtk;
using System.Data;
using Npgsql;
using System.Collections.Generic; 
using PArticuloMejorado;

namespace PArticuloMejorado
{
	public partial class MainWindow: Gtk.Window
	{	
		ListStore listStore;
		public MainWindow (): base (Gtk.WindowType.Toplevel)
		{
			Build ();
		
		string connectionString = "Server=localhost;Database=dbprueba;User Id=dbprueba;Password=root";
			IDbConnection dbConnection = new NpgsqlConnection(connectionString);
			dbConnection.Open();

			IDbCommand dbCommand = dbConnection.CreateCommand();
			dbCommand.CommandText="select * from articulo";
			IDataReader dataReader = dbCommand.ExecuteReader();

			//lista dinamica:
			List<Type> types = new List<Type>();
			for (int index =0; index < dataReader.FieldCount; index++){
				treeView.AppendColumn(dataReader.GetName(index), new CellRendererText(), "text", index);
				types.Add(typeof(string));
			}
			
			listStore = new ListStore(types.ToArray());

			treeView.Model = listStore;
		
			while(dataReader.Read()){
				//solucion dinamica 
				List<string> values = new List<string>();
				for(int index = 0; index < dataReader.FieldCount; index++)
					values.Add(dataReader[index].ToString());
				
				listStore.AppendValues(values.ToArray());
			}
			dataReader.Close();
			dbConnection.Close();
		
		}
		
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}


		protected void OnClearActionActivated (object sender, EventArgs e){
			listStore = (ListStore)treeView.Model;
			listStore.Clear();
		}	


		protected void OnEditActionActivated (object sender, EventArgs e){
			throw new System.NotImplementedException ();
		}	

		protected void OnRefreshActionActivated (object sender, EventArgs e){
			throw new System.NotImplementedException ();
		}


		protected void OnAddActionActivated (object sender, EventArgs e){
			VentanaEditar ventanaEditar = new VentanaEditar();
			ventanaEditar.Activate();
		}


	}

}
