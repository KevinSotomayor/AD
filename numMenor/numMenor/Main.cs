using System;

namespace numMenor
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//nivel de metodo
        int[] lis = { 150, 3, 2, 14, 55, 19, 100};

			
        for(int i = 0; i < lis.Length; i++) {
            Console.WriteLine("Numeros que esta dentro: " + lis[i]);
        }
        /*******************************************/
         int me = menor(lis);
         Console.WriteLine("\nEl menor es: " + me);
       /*************************************************/ 
        
			int pos = posicion(lis, 1114);
        Console.WriteLine("La posicion es: " + pos);
        /**************************************************/
       //  int[] lisOrdenada = {4, 3, 2, 14, 55, 19, 33};
      //   int posEnListaOrdenada = posicionEnListaOrdenada(lisOrdenada, 14);
      //   Console.WriteLine("La posicion en lista ordenada es: " + pos);
       
         
        
        
    }
      public static int menor(int[] lista){
          int  min = lista[0];
          for(int i = 1; i < lista.Length; i ++) {
              if(lista[i] <  min) {
                  min = lista[i];
              }
          }
              return min;
    
        }

    /****************************************************************/
          //Guardando la posicion del menor
     /* public static int posocionMenor(int[] lista){
       int posicionMenor = 0;
          for (int i=0; i < lista.Length; i++){
           if(lista[i]<lista[posicionMenor]){
               posicionMenor = i;
           }
       }
          return lista[posicionMenor];
          
      }/************************************************************/
       
      
      
		
		
      
      public static int posicion(int[] lista, int x){
           int i = 0;
           while(i < lista.Length && lista[i] != x)
           i++;
           if(i == lista.Length)//NO ENCONTRADO. HA RECORRIDO TODO Y NO HA ENCONTRADO NADA
                 return -1;
           return i;
           //NO hace falta poner un else, porque los returns tienen nu else"incorporado"
           
      }
         		
    /****************************************************************/		
	/**	public static int posicion(int[] lista, int x){

			for(int i=0; i<lista.Length; i++){
			
				if(lista[i] == x){
					Console.WriteLine("La busqueda ha tenido éxito, el elemento a buscar esta en la posicion: " + i);
					return x;
				
				}
			}
				return -1;
	
		}*/
          /****************************************************************/
    
      
		
		
     /* public static int posicionEnListaOrdenada(int[] lista, int x){
           //aplicar la tecnica del centinela
          
          return -1;
        }*/

		}
	}