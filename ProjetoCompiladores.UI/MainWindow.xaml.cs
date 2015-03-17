using System;
using System.Collections.Generic;
using System.Windows;
using ProjetoCompiladores.Principal;

namespace ProjetoCompiladores.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<string, string> dados;
 
        public MainWindow()
        {
            InitializeComponent();

            //var users = new List<User>
            //{
            //    new User() {Id = 1, Name = "John Doe", Birthday = new DateTime(1971, 7, 23)},
            //    new User() {Id = 2, Name = "Jane Doe", Birthday = new DateTime(1974, 1, 17)},
            //    new User() {Id = 3, Name = "Sammy Doe", Birthday = new DateTime(1991, 9, 2)}
            //};

            var sintatico = new AnalisadorSintatico("Teste.txt");
            dados = sintatico.AnalisarCodigo();
            //dados.Add("teste","1");
            //dados.Add("teste2","3");
            //dados.Add("teste3","4");
            //dados.Add("teste4","5");
            //dados.Add("teste5","6");
            //dados.Add("teste6","7");

            DgResultado.ItemsSource = dados;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }
    }
}
