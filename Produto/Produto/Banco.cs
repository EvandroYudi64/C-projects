using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produto
{
    public class Banco
    {
        public static NpgsqlConnection conexao;
        public NpgsqlCommand comando;
        public NpgsqlDataReader reader; //Tabela no formato Postgres
        public DataTable tabela;

        public Banco()
        {
            try
            {
                if ((conexao == null) || (conexao.State != ConnectionState.Open))
                {
                    conexao = new NpgsqlConnection("Server=;Port=;User Id=;Password=;Database=");
                    conexao.Open();
                }
                comando = new NpgsqlCommand();
                comando.Connection = conexao;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na conexão com o banco: " + ex.Message); //Dispara uma exceção
            }
        }
    }
}
