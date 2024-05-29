using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeiraAulaAPI
{
    public class Adm
    {
        public static List<Person> LoadData()
        {
            var people = new List<Person>();
            people.Add(new() { Id = 3, Name = "Ana", LastName= "Cunha", Age = 58, Phone = "14996862222" });
            people.Add(new() { Id = 1, Name = "Gustavo", LastName= " Vono", Age = 27, Phone = "14996866621" });
            people.Add(new() { Id = 2, Name = "Joao", LastName= " Silve", Age = 16, Phone = "14996861111" });
            people.Add(new() { Id = 4, Name = "Maria", LastName= " Antonia", Age = 37, Phone = "14996863333" });
            people.Add(new() { Id = 1, Name = "Andre", LastName= " Silva", Age = 27, Phone = "14996866621" });
            people.Add(new() { Id = 5, Name = "Josue", LastName= " Tonon", Age = 43, Phone = "14996864444" });
            people.Add(new() { Id = 6, Name = "Moises", LastName= " Silva", Age = 10, Phone = "14996865555" });
            return people;
        }

        public static List<Person> FilterByAge(List<Person> people) => people.Where(p => p.Age >= 18).ToList();

        public static List<Person> FilterByName(List<Person> people, string name) => people
            .Where(p => p.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        public static List<Person> OrderByName(List<Person> people) => people
            .OrderBy(p => p.Name)
            .ToList();

        public static List<Person> OrderByNameDesc(List<Person> people) => people
            .OrderByDescending(p => p.Name)
            .ToList();

        public static List<Person> FilterByChar(List<Person> people, char letter) => people
            .Where(p => p.Name.Contains(letter, StringComparison.OrdinalIgnoreCase) && p.Name.Length > 3)
            .ToList();

        public static void PrintData(List<Person> people) { foreach (var p in people) Console.WriteLine(p); }
    }
}
