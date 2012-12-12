using System;
using System.Data;

namespace Serpis.Ad
{
	public class AplicationContext
	{
		public AplicationContext (){
		}

		private static AplicationContext instance = new AplicationContext();

		public static AplicationContext Instance {
			get{return instance;}
		}

		private IDbConnection dbConnection;
		public IDbConnection DbConnection{
			get {return dbConnection;}
			set {dbConnection = value;}
		}

	}
}

