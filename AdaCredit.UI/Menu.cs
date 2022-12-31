using AdaCredit.UI.UseCases;
using ConsoleTools;

namespace AdaCredit.UI
{
    public class Menu
    {
        public void Show()
        {
            var subClient = new ConsoleMenu(Array.Empty<string>(), level: 1)
                .Add("Cadastrar um Novo Cliente", AddNewClient.Execute)
                .Add("Consultar Dados de um Cliente", () => SomeAction("Sub_Two"))
                .Add("Alterar Cadastro de um Cliente", () => SomeAction("Sub_Three"))
                .Add("Desativar Cadastro de um Cliente", () => SomeAction("Sub_Four"))
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
                .Add("Alterar Senha de um Funcionário", () => SomeAction("Sub_Two"))
                .Add("Desativar o Cadastro de um Funcionário", () => SomeAction("Sub_Three"))
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

            var menu = new ConsoleMenu(Array.Empty<string>(), level: 0)
                .Add("Clientes", subClient.Show)
                .Add("Funcionários", subEmployee.Show)
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
