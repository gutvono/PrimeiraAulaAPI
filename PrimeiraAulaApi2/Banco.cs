using Microsoft.Data.SqlClient;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeiraAulaApi2
{
    internal class Banco
    {
        public static string ConexaoSql() => $"Data Source = 127.0.0.1; Initial Catalog=Infracoes; User Id=sa; Password=SqlServer2019!; TrustServerCertificate=True";
        public static string ConexaoMongo() => $"mongodb://root:Mongo%402024%23@localhost:27017/";

        public static List<PenalidadesAplicadas> GetSqlData(SqlConnection SqlConnection)
        {
            List<PenalidadesAplicadas> lista = new();
            SqlCommand comando = new();
            comando.CommandText = "SELECT RazaoSocial, Cnpj, NomeMotorista, Cpf, VigenciaCadastro FROM Penalidade";
            comando.Connection = SqlConnection;
            SqlConnection.Open();
            try
            {
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new PenalidadesAplicadas(dr.GetString(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetDateTime(4)));
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
            finally { SqlConnection.Close(); }
            return lista;
        }



        public static void ProcessDataToMongo(MongoClient MongoConnection, SqlConnection SqlConnection, List<PenalidadesAplicadas> lista)
        {
            var db = MongoConnection.GetDatabase("Infracoes");
            var collection = db.GetCollection<BsonDocument>("Penalidade");
            SqlCommand comando = new();
            db.DropCollection("Penalidade");
            List<BsonDocument> ListaBson = new();

            foreach (var item in lista)
            {
                ListaBson.Add(new BsonDocument{
                            {"razao_social", item.RazaoSocial },
                            {"cnpj", item.CNPJ },
                            {"nome_motorista", item.NomeMotorista },
                            {"cpf", item.CPF },
                            {"vigencia_do_cadastro", item.VigenciaCadastro.ToString("dd/MM/yyyy")}});
            }
            collection.InsertMany(ListaBson);

            comando.CommandText = "INSERT INTO Controle_Processamento (Description, Date, NumberOfRecords) VALUES" +
                $"('Cópia de dados do SQL para o MongoDB', '{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}', {lista.Count})";

            SqlConnection.Open();
            comando.Connection = SqlConnection;

            try { comando.ExecuteNonQuery(); }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
            finally { SqlConnection.Close(); }
        }
    }
}
