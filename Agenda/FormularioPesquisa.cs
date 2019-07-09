using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenda
{
    public partial class FormularioPesquisa : Form
    {
        private List<Contato> listaPequisa = new List<Contato>();

        public FormularioPesquisa()
        {
            InitializeComponent();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnPesquisar_Click(object sender, EventArgs e)
        {

            //Pega os dados dos campos
            String nomePesq = this.txtNome.Text;
            String telPesq = this.txtTelefone.Text;

            //verificar se um dos campos foi preenchido
            if (nomePesq.Equals("") && telPesq.Equals(""))
            {
                MessageBox.Show("Você deve preencher pelo menos um dos campos!", "Nenhum campo preenchido",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Verificar qual será o tipo de pesquisa
            if (!nomePesq.Equals("") && !telPesq.Equals(""))
                PesquisarPorAmbos(nomePesq, telPesq);

            else if (nomePesq.Equals(""))
                PesquisarPorTelefone(telPesq);

            else
                PesquisarPorNome(nomePesq);
            
        }

        private void PesquisarPorAmbos(String nomeSearch, String telSearch)
        {
            foreach(Contato c in Geral.listaContatos)
            {
                if (String.Equals(c.nome, nomeSearch, StringComparison.OrdinalIgnoreCase) && 
                    c.telefone.Equals(telSearch))
                        this.listaPequisa.Add(c);
            }
            ExibirResultadoPesquisa();
        }

        private void PesquisarPorTelefone(String telSearch)
        {
            foreach (Contato c in Geral.listaContatos)
            {
                if (c.telefone.Equals(telSearch))
                    this.listaPequisa.Add(c);
            }
            ExibirResultadoPesquisa();
        }

        private void PesquisarPorNome(String nomeSearch)
        {
            foreach (Contato c in Geral.listaContatos)
            {
                if (String.Equals(c.nome, nomeSearch, StringComparison.OrdinalIgnoreCase))
                    this.listaPequisa.Add(c);
            }
            ExibirResultadoPesquisa();
        }

        private void ExibirResultadoPesquisa()
        {
            if(this.listaPequisa.Count == 0)
            {
                MessageBox.Show("Nenhum registro encontrado!", "Nada encontrado.",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            String pesquisa = "NOME          TELEFONE\n\n";

            foreach (Contato c in this.listaPequisa) {
                pesquisa += "\n" + c.nome + "           " + c.telefone;
            }

            MessageBox.Show(pesquisa);
            this.listaPequisa.Clear();
        }
    }
}
