using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Produto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Produto produto;
            ProdutoDAO produtoDAO;

            try
            {
                produto = new Produto();
                produto.setDescricao(txtDescricao.Text);
                produto.setDataValidade(dateTimePicker1.Value);
                produto.setPreco(txtPreco.Text);
                produto.setTaxaLucro(txtLucro.Text);

                produtoDAO = new ProdutoDAO();
                if (produtoDAO.gravar(produto) > 0)
                {
                    MessageBox.Show("Salvo com sucesso");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            ProdutoDAO produtoDAO;
            try
            {
                produtoDAO = new ProdutoDAO();
                dgvDados.DataSource = produtoDAO.listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Produto produto;
            ProdutoDAO produtoDAO;
            DataTable tabela;

            try
            {
                produtoDAO = new ProdutoDAO();
                produto = new Produto();
                int codigo = Convert.ToInt32(txtCodigo.Text);
                tabela = produtoDAO.buscar(codigo);

                string descricao = (string)tabela.Rows[0][0];
                DateTime data = (DateTime)tabela.Rows[0][1];
                double preco = Convert.ToDouble(tabela.Rows[0][2]);
                double lucro = Convert.ToDouble(tabela.Rows[0][3]);

                if (txtDescricao.Text == "")
                {
                    produto.setDescricao(descricao);
                }
                else
                {
                    produto.setDescricao(txtDescricao.Text);
                }
                if(dateTimePicker1.Value == DateTime.Now)
                {
                    produto.setDataValidade(data);
                }
                else
                {
                    produto.setDataValidade(dateTimePicker1.Value);
                }
                if(txtPreco.Text == "")
                {
                    produto.setPreco(preco);
                }
                else
                {
                    produto.setPreco(txtPreco.Text);
                }
                if(txtLucro.Text == "")
                {
                    produto.setTaxaLucro(lucro);
                }
                else
                {
                    produto.setTaxaLucro(txtLucro.Text);
                }
                
                if ((produtoDAO.atualizar(produto, codigo)) > 0)
                {
                    MessageBox.Show("Atualizado com sucesso");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            ProdutoDAO produtoDAO;
            try
            {
                int codigo = Convert.ToInt32(txtCodigo.Text);
                produtoDAO = new ProdutoDAO();
                if (produtoDAO.deletar(codigo) > 0)
                {
                    MessageBox.Show("Deletado com sucesso");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGrafico_Click(object sender, EventArgs e)
        {
            Grafico f;
            f = new Grafico();
            f.ShowDialog();
        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            Produto produto;
            ProdutoDAO produtoDAO;
            int codigo;
            try
            {
                if (txtCodigo.Text.Trim().Length > 0)
                {
                    codigo = Convert.ToInt32(txtCodigo.Text);
                    produtoDAO = new ProdutoDAO();
                    produto = produtoDAO.preencher(codigo);
                    if (produto != null)
                    {
                        txtDescricao.Text = produto.descricao.ToString();
                       // txtValidade.Text = Convert.ToString(produto.dataValidade);
                        txtPreco.Text = Convert.ToString(produto.preco);
                        txtLucro.Text = Convert.ToString(produto.taxaLucro);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
