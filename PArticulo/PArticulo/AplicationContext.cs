using System;
using System.Data;

namespace Serpis.Ad
{
	public class AplicationDbContext
	{
		public AplicationDbContext (){
		}

		private static AplicationDbContext instance = new AplicationDbContext();

		public static AplicationDbContext Instance {
			get{return instance;}
		}

		private IDbConnection dbConnection;
		public IDbConnection DbConnection{
			get {return dbConnection;}
			set {dbConnection = value;}
		}

	}
}

