using Microsoft.Data.SqlClient;
using MongoDB.Driver;
using PrimeiraAulaApi2;
using System.Diagnostics;

var lst = ReadFile.GetData("C:\\Users\\Gustavo Vono\\Documents\\TAREFAS\\api\\PrimeiraAulaAPI\\PrimeiraAulaApi2\\motoristas_habilitados.json");
Stopwatch sw = new();
SqlConnection conexaoSql = new(Banco.ConexaoSql());
MongoClient conexaoMongo = new(Banco.ConexaoMongo());

Console.WriteLine($"Quantidade de linhas: {TestFilter.GetCountRecords(lst)}");

int opcao = 0;
do
{
    Console.Clear();
    Console.WriteLine("SISTEMA PARA CONSULTAR MOTORISTAS\n" +
        "1 - Listar registros que tenham o cpf do documento iniciando com _____;\n" + //237
        "2 - Listar registros que tenham o ano de vigencia ____;\n" + //2021
        "3 - Quantas empresas tem _____ na razao socia;\n" +
        "4 - Mostrar lista de registros ordenada pela razao social;\n" +
        "5 - Inserir dados no banco SQL;\n" +
        "6 - Inserir dados no banco Mongo;\n" +
        "0 - Finalizar sistema.");
    opcao = int.Parse(Console.ReadLine());
    switch (opcao)
    {
        case 1:
            int numero;
            Console.Write("Informe o numero:");
            numero = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine($"Registros com o cpf do documento iniciando com -{numero}-");
            TestFilter.PrintData(TestFilter.FilterByCpf(lst, numero));
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            break;
        case 2:
            int ano;
            Console.Write("Informe o ano:");
            ano = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine($"Registros com o ano de vigencia -{ano}-");
            TestFilter.PrintData(TestFilter.FilterByYear(lst, ano));
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            break;
        case 3:
            string name;
            Console.Write("Pesquisar nome: ");
            name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"Empresas com -{name}- na razao social");
            Console.WriteLine(TestFilter.FilterBySocialReasonName(lst, name));
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            break;
        case 4:
            Console.Clear();
            Console.WriteLine("Lista de registros ordenada pela razao social");
            TestFilter.PrintData(TestFilter.OrderBySocialReason(lst));
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            break;
        case 5:
            Console.Clear();
            Console.WriteLine("Realizando inserção de dados no banco SQL...");
            sw = Stopwatch.StartNew();
            sw.Start();
            PenalidadesAplicadas.RegisterPenaltySql(conexaoSql, lst);
            sw.Stop();
            Console.WriteLine($"A insersão demorou {sw.ElapsedMilliseconds} milissegundos.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            sw.Restart();
            break;
        case 6:
            Console.Clear();
            Console.WriteLine("Realizando inserção de dados no banco Mongo...");
            sw = Stopwatch.StartNew();
            var ListaSqlData = Banco.GetSqlData(conexaoSql);
            sw.Start();
            Banco.ProcessDataToMongo(conexaoMongo, conexaoSql, ListaSqlData);
            sw.Stop();
            Console.WriteLine($"A insersão demorou {sw.ElapsedMilliseconds} milissegundos.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            sw.Restart();
            break;
        default:
            break;
    }
} while (opcao != 0);
