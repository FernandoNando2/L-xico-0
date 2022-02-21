using System;

namespace Léxico_0{
    /*
    Requerimiento 1: Agregar la notacion matematica a los numeros, ejemplo: 3.5e-8.
    Requerimiento 2: Programar el reconocimeinto de comentarios de linea, sin que sea considerado como token,
                     Ejemplo: x26 = 5 //Hola Mundo
                     ;
    */
    public class Lexico : Token{
    StreamReader archivo;
    StreamWriter log;
        public Lexico(){
            archivo = new StreamReader("C:\\Users\\Fernando Hernández\\Desktop\\ITQ\\4to Semestre\\Lenguajes y Autómatas 1\\Léxico 0\\Prueba.cpp");
            log = new StreamWriter("C:\\Users\\Fernando Hernández\\Desktop\\ITQ\\4to Semestre\\Lenguajes y Autómatas 1\\Léxico 0\\Prueba.log");
            log.AutoFlush = true;
        }

        public void Cerrar(){
            archivo.Close();
            log.Close();
        }

        public void NextToken(){
            string buffer= "";
            char c;

            while(char.IsWhiteSpace(c = (char) archivo.Read()));
            
            if (char.IsLetter(c)){
                buffer += c;
                while(char.IsLetterOrDigit(c= (char) archivo.Peek())){
                    buffer += c;
                    archivo.Read();
                }
            setClasificacion(tipos.identificador);
            }

            else if(char.IsDigit(c)) {
                buffer+=c;
                while(char.IsDigit(c= (char)archivo.Peek())){
                    buffer += c;
                    archivo.Read();
                }
                if (c=='.') {
                    buffer+=c;
                    archivo.Read();
                    if(char.IsDigit(c= (char)archivo.Peek())){
                        while(char.IsDigit(c= (char)archivo.Peek())){
                            buffer += c;
                            archivo.Read();
                        }
                    }
                    else{
                        Console.WriteLine("Error Léxico: Se espera un digito");
                        log.WriteLine("Error Léxico: Se espera un digito");
                    }
                }
                if (c == 'E' || c== 'e'){
                    buffer +=c;
                    archivo.Read();
                    if ((c= (char)archivo.Peek())=='+' || (c= (char)archivo.Peek())=='-' || char.IsDigit(c)){
                        buffer+=c;
                        archivo.Read();
                        if(char.IsDigit(c= (char)archivo.Peek())){
                            while(char.IsDigit(c= (char)archivo.Peek())){
                                buffer += c;
                                archivo.Read();
                            }
                        }
                    }
                }    
                setClasificacion(tipos.numero);
            }

            else if(c == '='){
                buffer += c;
                setClasificacion(tipos.asignacion);
                if ((c= (char)archivo.Peek())=='='){
                    buffer+=c;
                    setClasificacion(tipos.operador_relacional);
                    archivo.Read();
                }
            }

            else if(c == '+'){
                buffer += c;
                setClasificacion(tipos.operador_termino);
                if ((c= (char)archivo.Peek())=='+' || (c= (char)archivo.Peek())=='=' ){
                    buffer+=c;
                    setClasificacion(tipos.incremento_termino);
                    archivo.Read();
                }
            }

            else if(c == '-'){
                buffer += c;
                setClasificacion(tipos.operador_termino);
                if ((c= (char)archivo.Peek())=='-'|| (c= (char)archivo.Peek())=='='){
                    buffer+=c;
                    setClasificacion(tipos.incremento_termino);
                    archivo.Read();
                }
            }

            else if(c == '*'  || c == '%'){
                buffer += c;
                setClasificacion(tipos.operador_factor);
                if ((c= (char)archivo.Peek())=='='){
                    buffer+=c;
                    setClasificacion(tipos.incremento_factor);
                    archivo.Read();
                }
            }

            bool huboDiagonal = false;
            if (c == '/'){
                buffer += c;
                huboDiagonal = true;
                setClasificacion(tipos.operador_factor);
                if ((c= (char)archivo.Peek())=='='){
                    buffer+=c;
                    setClasificacion(tipos.incremento_factor);
                    archivo.Read();
                }
                else if ((c= (char)archivo.Peek())== '/'){
                    buffer = "";
                    huboDiagonal = false;
                }
            }

            if(huboDiagonal){
                
            }

            else if (c == ';'){
                buffer+=c;
                setClasificacion(tipos.fin_sentencia);
            }

            else if (c == '&'){
                buffer+=c;
                setClasificacion(tipos.caracter);
                if ((c= (char)archivo.Peek())=='&'){
                    buffer+=c;
                    setClasificacion(tipos.operador_logico);
                    archivo.Read();
                }
            }

            else if (c == '|'){
                buffer+=c;
                setClasificacion(tipos.caracter);
                if ((c= (char)archivo.Peek())=='|'){
                    buffer+=c;
                    setClasificacion(tipos.operador_logico);
                    archivo.Read();
                }
            }

            else if(c == '!'){
                buffer+=c;
                setClasificacion(tipos.operador_logico);
                if ((c= (char)archivo.Peek())=='='){
                    buffer+=c;
                    setClasificacion(tipos.operador_relacional);
                    archivo.Read();
                }
            }

            else if(c == '<' || c=='>' ){
                buffer+=c;
                setClasificacion(tipos.operador_relacional);
                if ((c= (char)archivo.Peek())=='='){
                    buffer+=c;
                    setClasificacion(tipos.operador_relacional);
                    archivo.Read();
                }
            }

            else if(c == '"'){
                buffer+=c;
                setClasificacion(tipos.cadena);
                while((c = (char)archivo.Read()) != '"'){
                    buffer += c;
                }
                buffer += c;
            }

            else if(c == '?' ){
                buffer += c;
                setClasificacion(tipos.operador_ternario);
            }

            else if(c == ':' ){
                buffer += c;
                setClasificacion(tipos.caracter);
                if ((c= (char)archivo.Peek())=='='){
                    buffer+=c;
                    setClasificacion(tipos.inicializacion);
                    archivo.Read();
                }
            }

            else {
                buffer+=c;
                setClasificacion(tipos.caracter);
            }
            setContenido(buffer);
            log.WriteLine(getContenido() +" | " + getClasificacion());
        }
        public bool FinArchivo(){
            return archivo.EndOfStream;
        }
    }
}