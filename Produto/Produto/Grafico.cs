using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Produto
{
    public partial class Grafico : Form
    {
        public Grafico()
        {
            InitializeComponent();
            
        }
        private void mostrarGrafico(int codigo)
        {

            DataTable tabela;
            ProdutoDAO produtoDAO;

            try
            {
                produtoDAO = new ProdutoDAO();
                tabela = produtoDAO.buscar(codigo);

                double lucro = (Convert.ToDouble(tabela.Rows[0][2])) * (Convert.ToDouble(tabela.Rows[0][3]) / 100);
                var hoje = DateTime.Now;
                var data = (DateTime)tabela.Rows[0][1];
                var dias = data - hoje;

            

                chart1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
                chart1.ChartAreas[0].AxisY.Title = "Lucro em R$";
                chart1.ChartAreas[0].AxisY2.Title = "Dias até o Vencimento";
                 
                chart1.Titles.Add((string)tabela.Rows[0][0]);

                chart1.Series[0].Name = "Lucro";
                chart1.Series[0].IsVisibleInLegend = true;
                chart1.Series[0].YAxisType = AxisType.Primary;

                chart1.Series.Add(new Series());
                chart1.Series[1].Name = "Validade";
                chart1.Series[1].IsVisibleInLegend= true;
                chart1.Series[1].YAxisType = AxisType.Secondary;

                chart1.ApplyPaletteColors();//Cores
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = chart1.Series[0].Color;
                chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = chart1.Series[1].Color;
                chart1.ChartAreas[0].AxisY.LineColor = chart1.Series[0].Color;
                chart1.ChartAreas[0].AxisY2.LineColor = chart1.Series[1].Color;

                chart1.Series[1].Points.AddXY(0.5, dias.Days);//coluna dias
                
                chart1.Series[0].Points.AddXY("  ", lucro);//coluna lucro

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void limpar()
        {
           
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ProdutoDAO produtoDAO;
            DataTable tabela = new DataTable();
            Produto produto = new Produto();
            try
            {
                produtoDAO = new ProdutoDAO();
                int codigo = produtoDAO.buscaCodigo((string)txtBusca.Text);
                tabela = produtoDAO.buscarDesc((string)txtBusca.Text);
                produto = produtoDAO.preencher(codigo);
                double preco = produto.preco;
                double taxalucro = produto.taxaLucro;
                double precoFinal = preco + (preco*taxalucro/100);
                var hoje = DateTime.Now;
                var prazo = (DateTime)produto.dataValidade;
                var validade = prazo - hoje;
                if (produto != null)
                {
                    textBox1.Text = produto.codigo.ToString();
                    textBox2.Text = produto.descricao.ToString();
                    textBox3.Text = Convert.ToString(produto.dataValidade);
                    textBox4.Text = precoFinal.ToString();
                    textBox5.Text = Convert.ToString(validade.Days);
                }
                DataTable listadv = new DataTable();
                listadv = produtoDAO.popularCombo(txtBusca.Text);
                dgvDados2.DataSource = listadv;
                
              
                mostrarGrafico(codigo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void comboTxt_KeyUp(object sender, KeyEventArgs e)
        {
            

        }

        private void comboTxt_DropDown(object sender, EventArgs e)
        {
        }

        private void comboTxt_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void comboTxt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        private void txtBusca_KeyUp(object sender, KeyEventArgs e)
        {
            
            ProdutoDAO produtoDAO;
            try
            {
                
                produtoDAO = new ProdutoDAO();
                txtBusca.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtBusca.AutoCompleteSource = AutoCompleteSource.CustomSource;
                AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
                DataTable listadv = new DataTable();
                listadv = produtoDAO.popularCombo(txtBusca.Text);
                dgvDados2.DataSource = listadv;
                
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
