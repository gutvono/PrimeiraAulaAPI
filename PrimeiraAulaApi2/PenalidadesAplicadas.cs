using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace PrimeiraAulaApi2
{
    public class PenalidadesAplicadas
    {
        [JsonProperty("razao_social")]
        public string RazaoSocial { get; set; }

        [JsonProperty("cnpj")]
        public string CNPJ { get; set; }

        [JsonProperty("nome_motorista")]
        public string NomeMotorista { get; set; }

        [JsonProperty("cpf")]
        public string CPF { get; set; }

        [JsonProperty("vigencia_do_cadastro")]
        public DateTime VigenciaCadastro { get; set; }

        public PenalidadesAplicadas(string razaoSocial, string cNPJ, string nomeMotorista, string cPF, DateTime vigenciaCadastro)
        {
            RazaoSocial = razaoSocial;
            CNPJ = cNPJ;
            NomeMotorista = nomeMotorista;
            CPF = cPF;
            VigenciaCadastro = vigenciaCadastro;
        }

        public override string ToString() => $"Razao social: {RazaoSocial.PadRight(65)}, CPF: {CPF.PadRight(15)}, Nome motorista: {NomeMotorista}, Data vigencia: {VigenciaCadastro}";

        public static void RegisterPenaltySql(SqlConnection sqlConnection, List<PenalidadesAplicadas> lista)
        {
            int count = lista.Count;
            SqlCommand sqlCommand = new();
            for (int i = 0; i <= (int)Math.Floor((double)lista.Count / 1000); i++)
            {
                string insert = "INSERT INTO Penalidade (RazaoSocial, Cnpj, NomeMotorista, Cpf, VigenciaCadastro) VALUES";
                foreach (var item in lista.Skip(1000 * i).Take(1000))
                {
                    if (item.CPF != null)
                    {
                        insert +=
                            $"('{item.RazaoSocial.Replace("'", "''")}', " +
                            $"'{item.CNPJ}', " +
                            $"'{item.NomeMotorista.Replace("'", "''")}', " +
                            $"'{item.CPF}', " +
                            $"'{item.VigenciaCadastro:yyyy-MM-dd}'),";
                    }
                }

                sqlConnection.Open();
                sqlCommand.CommandText = insert.Substring(0, insert.Length - 1);
                sqlCommand.Connection = sqlConnection;

                try { sqlCommand.ExecuteNonQuery(); }
                catch (SqlException e)
                {
                    Console.WriteLine($"--ERRO AO INSERIR QUERY SQL:\n" +
                        $"cod.: {e.ErrorCode}\n" +
                        $"msg: {e.Message}\n");
                }
                finally { sqlConnection.Close(); }
            }
        }
    }
}