using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeiraAulaAPI
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }

        public override string ToString() => $"Id: {Id}, Nome: {Name} {LastName}, Age: {Age}, Phone: {Phone}";
    }
}
