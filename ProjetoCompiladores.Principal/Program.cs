using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace ProjetoCompiladores.Principal
{
    class Program
    {
        static void Main(string[] args)
        {
            //var linha = "public void main \n { \n Console.WriteLine(\"ddsfd\"); int a = 12; \n \n }";

            //VerificadorDeCodigo(linha);
            //Console.ReadKey();

            var sintatico = new AnalisadorSintatico("Teste.txt");
            var aa = sintatico.AnalisarCodigo();
        }

        public static void VerificadorDeCodigo(string linha)
        {
            var alfabeto = Gramatica.Alfabeto();
            var simbolos = Gramatica.Palavras("caracteresEspeciais.xml");
            var expressoes = new List<char>();
            var separadores = new List<char>();
            var numerico = new List<char>();
            var comandos = Gramatica.Palavras("stringsDeImpressao.xml");
            var palavrasReservadas = Gramatica.Palavras("palavrasReservadas.xml");
            var numeroDaLinha = 1;

            //LerStrings() ler arquivo e joga em uma variavel;
            //var linha = "dfd";//string.Empty; //Na verdade vai ser a linha lida do arquivo;
            var resultado = string.Empty;

            while (linha != null)
            {
                var caractereInvalido = ' ';
                var posicao = 0;
                var casoTenhaErro = 1;

                if (linha.Length > 0)
                {
                    foreach (var caractere in linha)
                    {
                        //Procura esse carctere em todos os tipos conhecidos, caso encontre
                        if (VerificaTodosOsCaracteres(alfabeto, caractere, ref casoTenhaErro)) break;
                        //if (PossuiCaractere(simbolos, caractere, ref casoTenhaErro)) break;
                        if (VerificaTodosOsCaracteres(expressoes, caractere, ref casoTenhaErro)) break;
                        if (VerificaTodosOsCaracteres(separadores, caractere, ref casoTenhaErro)) break;
                        if (VerificaTodosOsCaracteres(numerico, caractere, ref casoTenhaErro)) break;
                        
                        caractereInvalido = caractere;
                        if (casoTenhaErro == 1) break;
                    }
                }
                else
                {
                    casoTenhaErro = 3;
                    resultado += string.Format("Linha {0} : Espaço vazio", linha);
                }

                if (casoTenhaErro == 1)
                    resultado += string.Format("Linha '{0}' - Caractere inválido", numeroDaLinha);
                else
                {
                    if (casoTenhaErro != 3)
                    {
                        AnalisaStrings(linha, comandos);
                        AnalisaStrings(linha, palavrasReservadas);
                        AnalisaStrings(linha, simbolos);
                    }
                }

                linha = null; //readLine();
                numeroDaLinha++;
            }
        }

        public static void AnalisaStrings(string linha, IList<string> strings)
        {
            var lista = linha.Split(' ').ToList();
            var resultado = lista.Where(strings.Contains).ToList();

            foreach (var r in resultado)
            {
                Console.WriteLine(r);
            }
        }
        public static void AnalisaCaracteres(string linha, IList<char> caracteres)
        {
            var lista = linha.ToArray();
            var resultado = lista.Where(caracteres.Contains).ToList();

            foreach (var r in resultado)
            {
                Console.WriteLine(r);
            }
        }
        public static bool VerificaTodosOsCaracteres(IList<char> universo, char caractere, ref int erro)
        {
            erro = 1;

            if (!universo.Contains(caractere)) return false;

            erro = 0;
            return true;
        }
    }
}
