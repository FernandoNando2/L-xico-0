using System;

namespace Léxico_0{
    public class Program{

        static void Main(string[] args){

            Lexico a = new Lexico();
            
            while(!a.FinArchivo()){
                a.NextToken();
            }
            a.Cerrar();
        }
    }
}