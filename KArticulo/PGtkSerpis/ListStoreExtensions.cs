using Gtk;
using System;
using System.Data;
using System.Collections.Generic;

namespace Serpis.Ad
{
	public static class ListStoreExtensions
	{
		public static void Fill(ListStore listStore, IDataReader dataReader)
		{
			while(dataReader.Read()){
			List<string> values = new List<string>();
			for(int index = 0; index < dataReader.FieldCount; index++)
				values.Add(dataReader[index].ToString());
			
			listStore.AppendValues(values.ToArray());
			}
		}
	}
}

