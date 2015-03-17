using System.Runtime.CompilerServices;

namespace ProjetoCompiladores.Principal
{
    public class Elemento
    {
        public Elemento(string palavra, string significado)
        {
            this.Palavra = palavra;
            this.Significado = significado;
        }

        public Elemento(char caractere, string significado)
        {
            this.Caractere = caractere;
            this.Significado = significado;
        }

        public string Palavra { get; set; }
        public char Caractere { get; set; }
        public string Significado { get; set; }

    }
}
