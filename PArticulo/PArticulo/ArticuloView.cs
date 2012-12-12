using Gtk;
using System;
using System.Data;
using Serpis.Ad;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		private IDbConnection dbConnection;
		public ArticuloView (long id) : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();

			dbConnection = AplicationContext.Instance.DbConnection;

				IDbCommand dbCommand = dbConnection.CreateCommand();
				//dbCommand.CommandText = "select * from articulo where id="+id;
				dbCommand.CommandText = string.Format ("select * from articulo where id={0}", id);
		//		dbCommand.CommandText = "select * from articulo where id=:id";
		//		IDbDataParameter dbDataParameter = dbCommand.CreateParameter();
		//		dbCommand.Parameters.Add (dbDataParameter);
		//		dbDataParameter.ParameterName = "id";
		//		dbDataParameter.Value = id;
				
				IDataReader dataReader = dbCommand.ExecuteReader ();
				dataReader.Read ();
				
				entryNombre.Text = (string)dataReader["nombre"];
				spinButtonPrecio.Value = Convert.ToDouble((decimal)dataReader["precio"]);
				
				Show ();
				
				dataReader.Close ();

				saveAction.Activated += delegate {
			
					Console.WriteLine("articuloView.SaveAction.Activated");
					
					IDbCommand dbUpdateCommand = AplicationContext.Instance.DbConnection.CreateCommand ();
					dbUpdateCommand.CommandText = "update articulo set nombre=:nombre, precio=:precio where id=:id";
					
					DbCommandExtensions.AddParameter (dbUpdateCommand, "nombre", entryNombre.Text);
					DbCommandExtensions.AddParameter (dbUpdateCommand, "precio", Convert.ToDecimal(spinButtonPrecio.Value));
					DbCommandExtensions.AddParameter (dbUpdateCommand, "id", id);
					
		//			Si usamos sustitución de cadenas tendremos problemas con:
		//			los "'" en los string, las "," en los decimal y el formato de las fechas
		//			dbUpdateCommand.CommandText = 
		//				String.Format ("update articulo set nombre='{0}', precio={1} where id={2}", 
		//				               articuloView.Nombre, articuloView.Precio, id);

					dbUpdateCommand.ExecuteNonQuery ();
					
					this.Destroy ();
				};

		}
			


		
		public string Nombre { 
			get {return entryNombre.Text;}
			set {entryNombre.Text = value;} 
		}
		
		public decimal Precio {
			get {return Convert.ToDecimal (spinButtonPrecio.Value);}
			set {spinButtonPrecio.Value = Convert.ToDouble(value);}
		}
		
		public long Categoria {
			set {
				//TODO implementar...
			}
		}
		
		public Gtk.Action SaveAction {
			get {return saveAction;}
		}
	}
}

