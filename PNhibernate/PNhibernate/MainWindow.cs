using System;
using Gtk;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Serpis.Ad;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		Configuration configuration = new Configuration();
		configuration.Configure();
		
		configuration.AddAssembly(typeof(Categoria).Assembly);
		
		new SchemaExport(configuration).Execute(true, false, false);
		
		ISessionFactoryConfiguration sessionFactory = configuration.BuildSessionFactory();
		
		ISession session = sessionFactory.OpenSession;
		
		
		Categoria categoria = (Categoria)session.Load(typeOf(Categoria),2L);
		//2L constante = Long
	
		
		session.Close();
		sessionFactory.Close();
	}
	
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
		
	}
}
