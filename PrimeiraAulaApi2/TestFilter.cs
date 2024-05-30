using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrimeiraAulaApi2
{
    public class TestFilter
    {
        public static int GetCountRecords(List<PenalidadesAplicadas> lista) => lista.Count;

        public static List<PenalidadesAplicadas> FilterByCpf(List<PenalidadesAplicadas> lista, int cpf) => lista
            .Where(m => m.CPF.StartsWith(cpf.ToString()))
            .ToList();

        public static List<PenalidadesAplicadas> FilterByYear(List<PenalidadesAplicadas> lista, int year) => lista
            .Where(m => m.VigenciaCadastro.Year.Equals(year))
            .ToList();

        public static int FilterBySocialReasonName(List<PenalidadesAplicadas> lista, string name) => lista
            .Where(m => m.RazaoSocial.Contains(name, StringComparison.OrdinalIgnoreCase))
            .Count();

        public static List<PenalidadesAplicadas> OrderBySocialReason(List<PenalidadesAplicadas> lista) => lista
            .OrderBy(m => m.RazaoSocial)
            .ToList();

        public static string? GenerateXml(List<PenalidadesAplicadas> lista)
        {
            if (lista.Count == 0) return null;

            return new XElement(
                    "Root",
                    from m in lista
                    select
                        new XElement("motorista",
                        new XElement("razao_social", m.RazaoSocial),
                        new XElement("cnpj", m.CNPJ),
                        new XElement("nome_motorista", m.NomeMotorista),
                        new XElement("cpf", m.CPF),
                        new XElement("data_de_vigencia", m.VigenciaCadastro)
                        )
                    ).ToString();
        }

        public static void PrintData(List<PenalidadesAplicadas> lista) { foreach (var m in lista) Console.WriteLine(m); }
    }
}
