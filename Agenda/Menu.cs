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
    public partial class Menu : Form
    {
        //==================================================================================
        public Menu()
        {
            InitializeComponent();

            //Carrega a lista de contatos
            Geral.CarregarListaContatos();

            //Apresenta versao do programa
            this.lblVersion.Text = Geral.versao;
        }

        //==================================================================================
        private void BtnSair_Click(object sender, EventArgs e)
        {
            //Sair da aplicação

            //Confirma se realmente deseja sair
            DialogResult result =  MessageBox.Show("Deseja sair da aplicação?", "Tem certeza?", 
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //Encerra o método
            if (result == DialogResult.No) return;

            //Fecha a aplicação
            Application.Exit();
        }

        //==================================================================================
        private void BtnInserir_Click(object sender, EventArgs e)
        {
            InserirEditar menuInserirEditar = new InserirEditar();
            menuInserirEditar.ShowDialog();
        }

        private void BtnPesquisar_Click(object sender, EventArgs e)
        {
            //Abrir o quadro de pesquisa
            FormularioPesquisa formPesquisa = new FormularioPesquisa();
            formPesquisa.ShowDialog();
        }
    }
}
