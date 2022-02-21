using System;

namespace Léxico_0
{
    public class Token
    {
        private string Contenido;
        private tipos Clasificacion;
        public enum tipos{
            identificador, numero, caracter,asignacion, inicializacion,
            operador_logico, operador_relacional, operador_ternario,
            operador_termino, operador_factor, incremento_termino, incremento_factor,
            fin_sentencia, cadena, comentario
        }
        public void setContenido(string Contenido){
            this.Contenido = Contenido;
        }
        public void setClasificacion(tipos Clasificacion){
            this.Clasificacion = Clasificacion;
        }
        public string getContenido(){
            return this.Contenido;
        }
        public tipos getClasificacion(){
            return this.Clasificacion;
        }
    }
}