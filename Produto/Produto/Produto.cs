using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produto
{
    public class Produto
    {
        public int codigo { get; private set; } //leitura livre, alteração privada

        public string descricao { get; private set; }

        public DateTime dataValidade { get; private set; }

        public double preco { get; private set; }

        public double taxaLucro { get; private set; }

        public void setCodigo(int c)
        {
            if (c < 0)
                throw new Exception("Código inválido");
            else
                this.codigo = c;
        }

        public void setCodigo(string c)
        {
            this.setCodigo(Convert.ToInt32(c));
        }

        public void setDescricao(string descricao)
        {
            this.descricao = descricao;
        }


        public void setDataValidade(DateTime dataValidade)
        {
            this.dataValidade = dataValidade;
        }
        public void setDataValidade(string dataValidade)
        {
            this.setDataValidade(Convert.ToDateTime(dataValidade));
        }
        public void setPreco(double p)
        {
            this.preco = p;
        }
        public void setPreco(string p)
        {
            this.setPreco(Convert.ToDouble(p));
        }

        public void setTaxaLucro(double l)
        {
            this.taxaLucro = l;
        }
        public void setTaxaLucro(string l)
        {
            this.setTaxaLucro(Convert.ToDouble(l));
        }
    }
}
