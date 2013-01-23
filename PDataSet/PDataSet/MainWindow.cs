using Npgsql;
using Gtk;
using System;
using System.Data;

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
	}

	protected void OnExecuteActionActivated (object sender, System.EventArgs e)
	{
		String stringConnection = "Server=localhost;Database=dbprueba;User Id=dbprueba;Password=root";
		IDbConnection dbConnection = new NpgsqlConnection(stringConnection);
		IDbCommand selectCommand = dbConnection.CreateCommand();
		selectCommand.CommandText = "select * from categoria";
		IDbDataAdapter dbDataAdapter = new NpgsqlDataAdapter();
		new NpgsqlCommandBuilder((NpgsqlDataAdapter)dbDataAdapter);
		
		dbDataAdapter.SelectCommand = selectCommand;
		DataSet dataSet = new DataSet();
			
		dbDataAdapter.Fill (dataSet);
		
		Console.WriteLine("Tables.count={0}", dataSet.Tables.Count);
		foreach(DataTable dataTable in dataSet.Tables)
			show(dataTable);
		
		DataRow dataRow = dataSet.Tables[0].Rows[0];
		dataRow["nombre"] = DateTime.Now.ToString();
		
		Console.WriteLine("\nTabla con los cambios");
		show(dataSet.Tables[0]);
		
		
//		((NpgsqlDataAdapter)dbDataAdapter).RowUpdating += delegate(object dbDataAdapterSender, NpgsqlRowUpdatingEventArgs eventArgs){
//		 
//			Console.WriteLine("RowUpdating command.Commandtext={0}", eventArgs.Command.CommandText);
//			
//			foreach(IDataParameter dataParameter in eventArgs.Command.Parameters)
//				Console.WriteLine("{0} = {1}", dataParameter.ParameterName, dataParameter.Value);
//		};
//		lo de arriba es para saber porque daba error...
		
		//TO-DO fallaba el update lanzando system.invalidOperationException
		//dbDataAdapter.Update(dataSet);
		
		((NpgsqlDataAdapter)dbDataAdapter).Update(dataSet.Tables[0]);
	}
	
	private void show(DataTable dataTable){
//		foreach(DataColumn dataColumn in dataTable.Columns)
//			Console.WriteLine("Column.Name={0}", dataColumn.ColumnName);
		
		foreach(DataRow dataRow in dataTable.Rows){
			foreach(DataColumn dataColumn in dataTable.Columns)
				Console.Write(" [{0}={1}]", dataColumn, dataRow[dataColumn]);
				Console.WriteLine();
		}
	}

	
}
