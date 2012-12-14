using Gtk;
using System;
using System.Data;

namespace Serpis.Ad
{
	public static class TreeViewExtensions
	{
		public static void AppendColumns(TreeView treeView, IDataReader dataReader)
		{
			for (int index = 0; index < dataReader.FieldCount; index++) 
				treeView.AppendColumn (dataReader.GetName (index), new CellRendererText(), "text", index);
		}
		
		public static void Fill(TreeView treeView, IDataReader dataReader) 
		{
			TreeViewExtensions.ClearColumns (treeView);
			TreeViewExtensions.AppendColumns (treeView, dataReader);		
			Type[] types = TypeExtensions.GetTypes (typeof(string), dataReader.FieldCount);
			
			ListStore listStore = new ListStore(types);
			treeView.Model = listStore;
			ListStoreExtensions.Fill (listStore, dataReader);
		}
		
		public static void ClearColumns(TreeView treeView)
		{
			treeView.Model = null;	//Para evitar el Gtk-CRITICAL **: IA__gtk_tree_view_get_cell_area: assertion `!column || column->tree_view == (GtkWidget *) tree_view' failed
						//que lanza al llamar a treeView.RemoveColumn(...)
			TreeViewColumn[] treeViewColumns = treeView.Columns;
			foreach (TreeViewColumn treeViewColumn in treeViewColumns)
				treeView.RemoveColumn (treeViewColumn);
		}
		
	}
}

