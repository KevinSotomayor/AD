using Gtk;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Serpis.Ad;
using System;
using System.Collections;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		Configuration configuration = new Configuration();
		configuration.Configure();
		configuration.SetProperty(NHibernate.Cfg.Environment.Hbm2ddlKeyWords, "none");
		configuration.AddAssembly(typeof(Categoria).Assembly);
		
		new SchemaExport(configuration).Execute(true, false, false);
		
		ISessionFactory sessionFactory = configuration.BuildSessionFactory();

		
		//updateCategoria(sessionFactory);
		
		//insertCategoria(sessionFactory);
		
		//loadArticulo(sessionFactory);


		ISession session = sessionFactory.OpenSession();
		ICriteria criteria = session.CreateCriteria(typeof(Articulo));
		criteria.SetFetchMode("Categoria", FetchMode.Join);
		IList  list = criteria.List();
		foreach (Articulo articulo in list)
			Console.WriteLine("Articulo Id={0} Nombre={1} Precio={2} Categoria={3}",
			                  articulo.Id, articulo.Nombre, articulo.Precio, articulo.Categoria);

		session.Close();

		sessionFactory.Close();
	}
	
	private void loadArticulo(ISessionFactory sessionFactory){
		using (ISession session = sessionFactory.OpenSession()){
			Articulo articulo = (Articulo)session.Load(typeof(Articulo), 2L);
			Console.Write("Articulo Id ={0} Nombre={1} Precio={2}", 
			                  articulo.Id, articulo.Nombre, articulo.Precio);
			if(articulo.Categoria == null)
				Console.WriteLine("Categoria = null");
			else
				Console.WriteLine("Categoria.Id={0} Categoria.Nombre={1}", articulo.Categoria.Id, articulo.Categoria.Nombre);
			//hace un select mas porque requerimos que nos muestre otra propiedad que no sea el id, como por ejempl nombre.
		}
			
	}
//	private void updateCategoria(ISessionFactory sessionFactory){
//		ISession session = sessionFactory.OpenSession();
//		try{
//			Categoria categoria = (Categoria)session.Load(typeof(Categoria), 2L);//2L constante = Long
//			Console.WriteLine("Categoria Id={0} Nombre{1}", categoria.Id, categoria.Nombre);
//			categoria.Nombre = DateTime.Now.ToString();
//			session.SaveOrUpdate(categoria);		
//			session.Flush();
//		} finally {
//			session.Close();
//		}
//	}
	
//	private void insertCategoria(ISessionFactory sessionFactory){
//		ISession session = sessionFactory.OpenSession();
//		try{ 
//			Categoria categoria = new Categoria();
//			categoria.Nombre = "Nueva " + DateTime.Now.ToString();
//			session.SaveOrUpdate(categoria);
//			session.Flush();
//		
//		} finally {
//			session.Close();
//	
//		}
//	}
//	
	private void updateCategoria(ISessionFactory sessionFactory){
		using(ISession session = sessionFactory.OpenSession()){
			Categoria categoria = (Categoria)session.Load(typeof(Categoria), 2L);//2L constante = Long
			Console.WriteLine("Categoria Id={0} Nombre{1}", categoria.Id, categoria.Nombre);
			categoria.Nombre = DateTime.Now.ToString();
			session.SaveOrUpdate(categoria);		
			session.Flush();
		}
	}
	
	private void insertCategoria(ISessionFactory sessionFactory){
		using (ISession session = sessionFactory.OpenSession() ){ //-> using == try + finally
			Categoria categoria = new Categoria();
			categoria.Nombre = "Nueva " + DateTime.Now.ToString();
			session.SaveOrUpdate(categoria);
			session.Flush();
		
		}
	}
	
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
		
	}
}
