using AdaCredit.UI.UseCases;
using AdaCredit.UI.Repositories;
using ConsoleTools;
using System.Security.Cryptography.X509Certificates;

namespace AdaCredit.UI
{
    public class Menu
    {
        public void Show()
        {
            String path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            var subClient = new ConsoleMenu(Array.Empty<string>(), level: 1)
                .Add("Cadastrar Novo Cliente", AddNewClient.Execute)
                .Add("Consultar os Dados de um Cliente existente", ClientRepository.Query)
                .Add("Alterar o Cadastro de um Cliente existente", ClientRepository.Modify)
                .Add("Desativar Cadastro de um Cliente existente", ClientRepository.Deactivate)
                .Add("Voltar", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = false;
                    config.Title = "Clientes";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                    config.SelectedItemForegroundColor = ConsoleColor.Black;
                    config.SelectedItemBackgroundColor = ConsoleColor.White;
                });

            var subEmployee = new ConsoleMenu(Array.Empty<string>(), level: 1)
                .Add("Cadastrar um Novo Funcionário", AddNewEmployee.Execute)
                .Add("Alterar Senha de um Funcionário", EmployeeRepository.ChangePass)
                .Add("Desativar o Cadastro de um Funcionário", EmployeeRepository.Deactivate)
                .Add("Voltar", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = false;
                    config.Title = "Funcionários";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                    config.SelectedItemForegroundColor = ConsoleColor.Black;
                    config.SelectedItemBackgroundColor = ConsoleColor.White;
                });

            var subTransaction = new ConsoleMenu(Array.Empty<string>(), level: 1)
                .Add("Processar Transações", Reconciliation.reconciliation)
                .Add("Voltar", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = false;
                    config.Title = "Transações";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                    config.SelectedItemForegroundColor = ConsoleColor.Black;
                    config.SelectedItemBackgroundColor = ConsoleColor.White;
                });

            var subReports = new ConsoleMenu(Array.Empty<string>(), level: 1)
                .Add("Exibir Todos os Clientes Ativos com seus Respectivos Saldos", AddNewClient.Execute)
                .Add("Exibir Todos os Clientes Inativos", () => SomeAction("Sub_Two"))
                .Add("Exibir Todos os Funcionários Ativos e sua Última Data e Hora de Login", () => SomeAction("Sub_Three"))
                .Add("Exibir Transações com Erro", () => SomeAction("Sub_Four"))
                .Add("Voltar", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = false;
                    config.Title = "Relatórios";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                    config.SelectedItemForegroundColor = ConsoleColor.Black;
                    config.SelectedItemBackgroundColor = ConsoleColor.White;
                });

            var menu = new ConsoleMenu(Array.Empty<string>(), level: 0)
                .Add("Clientes", subClient.Show)
                .Add("Funcionários", subEmployee.Show)
                .Add("Transações", subTransaction.Show)
                .Add("Relatórios", subReports.Show)
                .Add("Sair", () => Environment.Exit(0))
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = false;
                    config.Title = "Ada Credit";
                    config.EnableWriteTitle = false;
                    config.EnableBreadcrumb = true;
                    config.SelectedItemForegroundColor = ConsoleColor.Black;
                    config.SelectedItemBackgroundColor = ConsoleColor.White;
                });

            menu.Show();
        }

        private static void SomeAction(string subOne)
        {
        }
    }
}
