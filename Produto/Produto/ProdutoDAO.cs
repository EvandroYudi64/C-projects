using NpgsqlTypes;
using Produto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Produto
{
    public class ProdutoDAO
    {
        public int gravar(Produto produto)
        {
            Banco banco;
            try
            {
                banco = new Banco();
                banco.comando.CommandText = "INSERT INTO Produto(descricao, dataValidade, preco, taxaLucro) VALUES (@descricao, @dataValidade, @preco, @taxaLucro);";
                banco.comando.Parameters.Add("@descricao", NpgsqlDbType.Varchar).Value = produto.descricao;
                banco.comando.Parameters.Add("@dataValidade", NpgsqlDbType.Date).Value = produto.dataValidade;
                banco.comando.Parameters.Add("@preco", NpgsqlDbType.Double).Value = produto.preco;
                banco.comando.Parameters.Add("@taxaLucro", NpgsqlDbType.Double).Value = produto.preco;
                banco.comando.Prepare();

                int qtde = banco.comando.ExecuteNonQuery();
                Banco.conexao.Close();
                return qtde;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar cliente: " + ex.Message);
            }
        }

        public void gravarGetCodigo(Produto produto)
        {
            Banco banco;
            try
            {
                banco = new Banco();
                banco.comando.CommandText = "INSERT INTO Produto(descricao, dataValidade, preco, taxaLucro) VALUES (@descricao, @dataValidade, @preco, @taxaLucro);";
                banco.comando.Parameters.Add("@descricao", NpgsqlDbType.Varchar).Value = produto.descricao;
                banco.comando.Parameters.Add("@dataValidade", NpgsqlDbType.Date).Value = produto.dataValidade;
                banco.comando.Parameters.Add("@preco", NpgsqlDbType.Double).Value = produto.preco;
                banco.comando.Parameters.Add("@taxaLucro", NpgsqlDbType.Double).Value = produto.taxaLucro;
                banco.comando.Prepare();

                int codigo = (int)banco.comando.ExecuteScalar();
                produto.setCodigo(codigo);
                Banco.conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar cliente com código: " + ex.Message);
            }
        }

        public DataTable listar()
        {
            Banco banco;
            try
            {
                banco = new Banco();
                banco.comando.CommandText = "SELECT codigo, descricao, dataValidade, preco, taxaLucro  FROM Produto ORDER BY 1;";
                banco.reader = banco.comando.ExecuteReader();
                banco.tabela = new DataTable();
                banco.tabela.Load(banco.reader);

                Banco.conexao.Close();
                return banco.tabela;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar produto: " + ex.Message);
            }
        }
        public DataTable buscar(int codigo)
        {
            Banco banco;
            try
            {
                banco = new Banco();
                banco.comando.CommandText = "SELECT descricao, dataValidade, preco, taxaLucro  FROM Produto WHERE codigo = @codigo;";
                banco.comando.Parameters.Add("@codigo", NpgsqlDbType.Integer).Value = codigo;
                banco.reader = banco.comando.ExecuteReader();
                banco.tabela = new DataTable();
                banco.tabela.Load(banco.reader);

                Banco.conexao.Close();
                return banco.tabela;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar produto: " + ex.Message);
            }
        }
        public DataTable buscarDesc(string descricao)
        {
            Banco banco;
            try
            {
                banco = new Banco();
                banco.comando.CommandText = "SELECT codigo, dataValidade, preco, taxaLucro  FROM Produto WHERE descricao = @descricao;";
                banco.comando.Parameters.Add("@descricao", NpgsqlDbType.Varchar).Value = descricao;
                banco.reader = banco.comando.ExecuteReader();
                
                banco.tabela = new DataTable();
                banco.tabela.Load(banco.reader);

                Banco.conexao.Close();
                return banco.tabela;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar produto: " + ex.Message);
            }
        }
        public int atualizar(Produto produto, int codigo)
        {
            Banco banco;
            try
            {
              
                banco = new Banco();
                banco.comando.CommandText = "UPDATE Produto SET descricao = @descricao, dataValidade = @dataValidade, preco = @preco, taxaLucro = @taxaLucro WHERE codigo = @codigo";
                banco.comando.Parameters.Add("@descricao", NpgsqlDbType.Varchar).Value = produto.descricao;
                banco.comando.Parameters.Add("@dataValidade", NpgsqlDbType.Date).Value = produto.dataValidade;
                banco.comando.Parameters.Add("@preco", NpgsqlDbType.Double).Value = produto.preco;
                banco.comando.Parameters.Add("@taxaLucro", NpgsqlDbType.Double).Value = produto.taxaLucro;
                banco.comando.Parameters.Add("@codigo", NpgsqlDbType.Integer).Value = codigo;
                banco.comando.Prepare();

                int qtde = banco.comando.ExecuteNonQuery();
                Banco.conexao.Close();
                return qtde;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar produto: " + ex.Message);
            }
        }
        public int deletar(int codigo)
        {
            Banco banco;
            try
            {
                banco = new Banco();
                banco.comando.CommandText = "DELETE FROM Produto WHERE codigo = @codigo";
                banco.comando.Parameters.Add("@codigo", NpgsqlDbType.Integer).Value = codigo;
                banco.comando.Prepare();

                int qtde = banco.comando.ExecuteNonQuery();
                Banco.conexao.Close();
                return qtde;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar produto: " + ex.Message);
            }
        }
        public Produto preencher(int codigo)
        {
            Banco banco;
            try
            {
                Produto produto = null;
                banco = new Banco();
                banco.comando.CommandText = "SELECT codigo, descricao, dataValidade, preco, taxaLucro FROM Produto WHERE codigo=@codigo;";
                banco.comando.Parameters.Add("@codigo", NpgsqlDbType.Integer).Value = codigo;
                banco.comando.Prepare();
                banco.reader = banco.comando.ExecuteReader();

                if (banco.reader.Read())
                {
                    produto = new Produto();
                    produto.setCodigo(codigo);
                    produto.setDescricao((string)banco.reader[1]);
                    
                    produto.setDataValidade((DateTime)banco.reader[2]);
                    produto.setPreco((double)banco.reader[3]);
                    produto.setTaxaLucro((double)banco.reader[4]);
                }
                Banco.conexao.Close();
                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar cliente: " + ex.Message);
            }
        }
        public int buscaCodigo(string descricao)
        {
            Banco banco;
            try
            {
                banco = new Banco();
                banco.comando.CommandText = "SELECT codigo FROM Produto WHERE descricao = @descricao;";
                banco.comando.Parameters.Add("@descricao", NpgsqlDbType.Varchar).Value = descricao;
                banco.reader = banco.comando.ExecuteReader();
                banco.reader.Read();
                int codigo = Convert.ToInt32(banco.reader[0]);
                Banco.conexao.Close();
                
                return codigo;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar produto: " + ex.Message);
            }
        }

        public DataTable popularCombo(string query)
        {
            Banco banco;
            try
            {
                banco = new Banco();
                banco.comando.CommandText = "SELECT descricao FROM Produto ORDER by 1";
                banco.reader = banco.comando.ExecuteReader();
                AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
                banco.tabela = new DataTable();
                banco.tabela.Load(banco.reader);
                DataView listadv = banco.tabela.DefaultView;
                listadv.RowFilter = $"descricao LIKE '%{query}%'";
               // int i = 0;
               // MessageBox.Show(Convert.ToString(banco.tabela.Rows[i][1]));
              
                //while (banco.tabela.Rows.Count>0)
                //{
                    //lista.Add(Convert.ToString(banco.tabela.Rows[i][1]));
                   // i++;
                //}
           
                Banco.conexao.Close();
                return listadv.ToTable();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar produto: " + ex.Message);
            }
        }

    }
}
