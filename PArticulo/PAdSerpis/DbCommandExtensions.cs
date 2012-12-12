using System;
using System.Data;

namespace Serpis.Ad
{
	/// <summary>
	/// Db command extensions.
	/// </summary>
	public class DbCommandExtensions
	{
		/// <summary>
		/// Adds the parameter.
		/// </summary>

		public static void AddParameter(IDbCommand dbCommand, string name, object value)
		{
			IDbDataParameter dbDataParameter = dbCommand.CreateParameter();
			dbDataParameter.ParameterName = name;
			dbDataParameter.Value = value;
			dbCommand.Parameters.Add (dbDataParameter);
		}
	}
}

