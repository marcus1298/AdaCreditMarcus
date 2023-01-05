using AdaCredit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTools;
using AdaCredit.Infra;

namespace AdaCredit
{
    public static class Menu
    {
        public static void Show()
        {
            String path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            var subClient = new ConsoleMenu(Array.Empty<string>(), level: 1)
                .Add("Cadastrar Novo Cliente", ClientActions.Execute)
                .Add("Consultar os Dados de um Cliente existente", ClientActions.searchClient)
                .Add("Alterar o Cadastro de um Cliente existente", ClientActions.Modify)
                .Add("Desativar Cadastro de um Cliente existente", ClientActions.Deactivate)
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
                .Add("Cadastrar um Novo Funcionário", EmployeeActions.Execute)
                .Add("Alterar Senha de um Funcionário", EmployeeActions.ChangePass)
                .Add("Desativar o Cadastro de um Funcionário", EmployeeActions.Deactivate)
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
                .Add("Processar Transações", ReconciliationActions.reconciliation)
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
                .Add("Exibir Todos os Clientes Ativos com seus Respectivos Saldos", ClientActions.ShowActiveClients)
                .Add("Exibir Todos os Clientes Inativos", ClientActions.ShowInativeClients)
                .Add("Exibir Todos os Funcionários Ativos e sua Última Data e Hora de Login", EmployeeActions.PointControl)
                .Add("Exibir Transações com Erro", ReconciliationActions.WrongTransactions)
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
    }
}
