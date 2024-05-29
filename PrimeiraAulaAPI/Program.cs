// See https://aka.ms/new-console-template for more information
using PrimeiraAulaAPI;

Console.WriteLine("Início do processamento!");

var people = Adm.LoadData();

Adm.PrintData(people);

Console.WriteLine("\nPessoas com mais de 18 anos:");
Adm.PrintData(Adm.FilterByAge(people));

var name = "j";
Console.WriteLine($"\nPessoas no qual o nome começa com {name}");
Adm.PrintData(Adm.FilterByName(people, name));

Console.WriteLine("\nPessoas ordenada pelo nome (ordem alfabetica ascendente)");
Adm.PrintData(Adm.OrderByName(people));

Console.WriteLine("\nPessoas ordenada pelo nome (ordem alfabetica descendente)");
Adm.PrintData(Adm.OrderByNameDesc(people));


char letter = 'A';
Console.WriteLine("\nPessoas que possuem a letra {letra} e que o nome tenha mais de 3 caracteres");
Adm.PrintData(Adm.FilterByChar(people, letter));