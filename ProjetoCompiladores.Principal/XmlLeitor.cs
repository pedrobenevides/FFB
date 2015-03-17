using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ProjetoCompiladores.Principal.Interfaces;

namespace ProjetoCompiladores.Principal
{
    public class XmlLeitor : ILeitor
    {
        private readonly XmlReader xmlReader;

        public XmlLeitor()
        { }

        public XmlLeitor(string caminho)
        {
            this.xmlReader = new XmlTextReader(caminho);
        }

        public IList<Elemento> LerStrings()
        {
            var dados = new List<Elemento>();
            var palavra = string.Empty;

            while (xmlReader.Read())
            {
                if (xmlReader.Name == "ns29:palavra")
                {
                    palavra = xmlReader.ReadString();
                    continue;
                }

                if (xmlReader.Name == "ns29:siginificado")
                    dados.Add(new Elemento(palavra, xmlReader.ReadString()));
            }

            return dados;
        }

        public IList<Elemento> LerChars()
        {
            var dados = new List<Elemento>();
            var caractere = ' ';

            while (xmlReader.Read())
            {
                if (xmlReader.Name == "ns29:palavra")
                {
                    caractere = xmlReader.ReadString()[0];
                    continue;
                }

                if (xmlReader.Name == "ns29:siginificado")
                    dados.Add(new Elemento(caractere, xmlReader.ReadString()));
            }

            return dados;
        }
    }
}
