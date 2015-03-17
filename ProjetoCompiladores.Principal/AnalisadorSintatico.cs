using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjetoCompiladores.Principal
{
    public class AnalisadorSintatico
    {
        private readonly string caminhoDoArquivo;
        private readonly Dictionary<string, string> analise;
        private readonly IList<char> alfabeto;
        private readonly IList<char> expressoes;
        private readonly IList<char> separadores;
        private readonly IList<char> numerico;
        private IList<string> comandos;
        private IList<string> palavrasReservadas;
        private IList<string> simbolos;

        public AnalisadorSintatico(string caminhoDoArquivo)
        {
            this.caminhoDoArquivo = caminhoDoArquivo;
            analise = new Dictionary<string, string>();
            alfabeto = Gramatica.Alfabeto();
            expressoes = Gramatica.Expressoes();
            separadores = Gramatica.Caracteres("stringsDeImpressao.xml");
            numerico = Gramatica.Caracteres("stringsDeImpressao.xml");
            comandos = Gramatica.Palavras("stringsDeImpressao.xml");
            palavrasReservadas = Gramatica.Palavras("palavrasReservadas.xml");
            simbolos = Gramatica.Palavras("caracteresEspeciais.xml");
        }

        public Dictionary<string, string> AnalisarCodigo()
        {
            var codigoFonteInline = LerArquivo();
            var codigoEmLinhas = codigoFonteInline.Split('\n').ToList();

            var casoTenhaErro = 1;
            var caractereInvalido = ' ';
            var temErro = false;

            foreach (var linha in codigoEmLinhas)
            {
                if (linha.Length > 0)
                {
                    foreach (var caractere in linha)
                    {
                        //Procura esse carctere em todos os tipos conhecidos, caso encontre
                        if (PossuiCaractere(alfabeto, caractere, ref casoTenhaErro)) continue;
                        if (PossuiCaractere(expressoes, caractere, ref casoTenhaErro)) continue;
                        if (PossuiCaractere(separadores, caractere, ref casoTenhaErro)) continue;
                        if (PossuiCaractere(numerico, caractere, ref casoTenhaErro)) continue;

                        caractereInvalido = caractere;
                        break;
                    }

                    foreach (var palavra in linha.Split(' '))
                    {
                        AnalisaStrings(linha, comandos);
                        AnalisaStrings(linha, palavrasReservadas);
                        AnalisaStrings(linha, simbolos);
                    }
                }
                else
                {
                    casoTenhaErro = 3;
                    //resultado += string.Format("Linha {0} : Espaço vazio", linha);
                }

                if (casoTenhaErro == 1)
                    continue;
                    //resultado += string.Format("Linha '{0}' - Caractere inválido", numeroDaLinha);
                else
                {
                    if (casoTenhaErro != 3)
                    {
                        AnalisaStrings(linha, comandos);
                        AnalisaStrings(linha, palavrasReservadas);
                        AnalisaStrings(linha, simbolos);
                    }
                }
            }

            return analise;
        }

        public string LerArquivo()
        {
            string resultado;

            using (var sr = new StreamReader("Teste.txt"))
            {
                resultado = sr.ReadToEnd();
            }

            return resultado;
        }

        public void AnalisaStrings(string linha, IList<string> strings)
        {
            var lista = linha.Split(' ').ToList();
            var resultado = lista.Where(strings.Contains).ToList();

            foreach (var r in resultado)
            {
                if(!analise.ContainsKey(r))
                    analise.Add(r, null);
            }
        }
        public void AnalisaCaracteres(string linha, IList<char> caracteres)
        {
            var lista = linha.ToArray();
            var resultado = lista.Where(caracteres.Contains).ToList();

            foreach (var r in resultado)
            {
                analise.Add(r.ToString(), null);
            }
        }
        public bool PossuiCaractere(IList<char> universo, char caractere, ref int erro)
        {
            erro = 1;

            if (!universo.Contains(caractere)) return false;

            erro = 0;
            return true;
        }
        
        public bool PossuiCaractere2(IList<char> universo, char caractere)
        {
            return universo.Contains(caractere);
        }
    }
}
