using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoCompiladores.Principal
{
    public static class Gramatica
    {
        public static IList<char> Alfabeto()
        {
            var alfabeto = new List<char>();

            for (var i = 65; i < 91; i++)
                alfabeto.Add(Convert.ToChar(i));

            for (var i = 97; i < 122; i++)
                alfabeto.Add(Convert.ToChar(i));

            return alfabeto;
        }

        public static IList<char> Caracteres(string caminho)
        {
            var xmlLeitor = new XmlLeitor(caminho);
            return xmlLeitor.LerChars().Select(c => c.Caractere).ToList();
        }
        public static IList<string> Palavras(string caminho)
        {
            var xmlLeitor = new XmlLeitor(caminho);
            return xmlLeitor.LerStrings().Select(s => s.Palavra).ToList();
        }

        public static IList<char> Expressoes()
        {
            return new List<char>{'+','-','*','/'};
        }
    }
}
