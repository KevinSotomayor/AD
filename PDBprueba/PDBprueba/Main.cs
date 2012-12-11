using System;
using System.Data;
using Npgsql;

namespace Npgsql{
	public class PDbprueba{
		public static void Main(string[] args){
			NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;" +
				"Database=dbprueba;" +
				"User Id= dbprueba;"+
				"Password = root;");
	
			connection.Open();
			Console.WriteLine("Empezamos a abrir la conexion.");
			IDbCommand command = connection.CreateCommand();
			command.CommandText=("select * from prueba");
			IDataReader dataReader = command.ExecuteReader();
			
			showColumnNames(dataReader);
			
			Console.WriteLine("\nVa a entrar al bucle\n");
	 		while(dataReader.Read()) {
				showValues(dataReader);
			};
	
			dataReader.Close();
			connection.Close();
			Console.WriteLine("Fin de la conexion.");		
		}
		
	 				/*** ****** MÉTODOS ***** ***/
		private static void showValues(IDataReader dataReader){
			for ( int i =0; i < dataReader.FieldCount; i ++){
			    object value = dataReader.GetValue(i);
				Console.WriteLine("Index={0} value={1} type={2}", i, value, value.GetType());
			}
		}
	
		private static void showColumnNames(IDataReader dataReader){
			string[] columnNames = getColumnNames(dataReader);
			foreach ( string name in columnNames)
				Console.WriteLine("Column={0}", name);
		}	
		
		private static string[] getColumnNames(IDataReader dataReader){
			string[] names = new string[ dataReader.FieldCount ];
			for(int i = 0; i < dataReader.FieldCount; i++)
				names[ i ] = dataReader.GetName(i);
		return names;
		
		}
	}
};