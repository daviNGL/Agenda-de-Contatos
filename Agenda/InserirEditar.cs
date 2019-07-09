using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Agenda
{
    public partial class InserirEditar : Form
    {

        private bool isEdicao = false;

        //==================================================================================
        public InserirEditar()
        {
            InitializeComponent();
            this.ConstruirLista();
        }

        //==================================================================================
        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        //==================================================================================
        private void Button1_Click(object sender, EventArgs e)
        {
   
            //Pegar os dados nos campos de texto
            String nome = this.txtNome.Text;
            String tel = this.txtNumero.Text;

            //Verifica se algum campo não foi inserido, caso sim exibe um erro e encerra o metodo
            if (CamposEmBranco())
            {
                MessageBox.Show("Você não pode deixar informações em branco.", "Dados inválidos!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            //Criar uma nova instancia de Contato
            Contato novoContato = new Contato(nome, tel);

            //Diretorios do arquivo
            String caminhoDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            String arquivoContato = @"\meus_contatos.txt";

            //Abre o arquivo meus_contatos.txt se existir, ou cria se não existir
            StreamWriter arquivo = new StreamWriter(caminhoDocumentos+arquivoContato, true, Encoding.Default);

            //** VERIFICA SE O PROCESSO É UMA EDICAO, SE FOR EXCLUI O CONTATO SELECIONADO PARA A EDICAO ANTES DE SALVAR
            if (this.isEdicao)
                RemoverContatoSelecionado();

            //Verifica se já existe um registro igual na lista
            foreach(Contato c in Geral.listaContatos)
            {
                if(c.nome.Equals(novoContato.nome) && c.telefone.Equals(novoContato.telefone))
                {
                    MessageBox.Show("Esse contato já está salvo em sua lista!", "Contato duplicado.", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //Insere o contato no arquivo
            arquivo.WriteLine(novoContato.nome);
            arquivo.WriteLine(novoContato.telefone);
            

            //Avisa que o contato foi inserido/editado com sucesso
            if(this.isEdicao)
            MessageBox.Show("Contato atualizado com sucesso!", "Contato salvo.", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Novo contato salvo com sucesso!", "Contato salvo.",
                MessageBoxButtons.OK, MessageBoxIcon.Information);


            //Atualiza a lista de contatos
            Geral.listaContatos.Add(novoContato);
            this.ConstruirLista();

            //Fecha o arquivo
            arquivo.Dispose();

            //Limpa os campos
            LimparCampos();

            //Seta o atributo isEdicao
            this.isEdicao = false;
        }


        //==================================================================================
        private void Button2_Click(object sender, EventArgs e)
        {
            //Pega o numero da linha selecionada
            int index = this.listBoxContatos.SelectedIndex;

            //Verifica se alguma linha foi selecionada, caso não exibe erro e encerra o metodo
            if(index < 0)
            {
                MessageBox.Show("Selecione um contato para remover!", "ERROR", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Confirma a exclusao
            if (MessageBox.Show("Deseja mesmo excluir o contato?", "Tem certeza?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            //Remove o contato da lista de contatos
            Geral.listaContatos.RemoveAt(index);

            //Atualiza o arquivo de texto (mmeus_contatos.txt)
            Geral.GravarFicheiro();

            //Mensagem de OK
            MessageBox.Show("Contato removido com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            //Atualizar listBox
            this.ConstruirLista();

        }


        private void BtnEditar_Click(object sender, EventArgs e)
        {
            //Altera o atributo para informar que é uma edicao
            this.isEdicao = true;

            //Pega o index da linha
            int index = this.listBoxContatos.SelectedIndex;

            //Carregar os dados do contato nos campos de texto
            this.txtNome.Text = Geral.listaContatos[index].nome;
            this.txtNumero.Text = Geral.listaContatos[index].telefone;
        }

        //==================================================================================
        private bool CamposEmBranco()
        {
            return (this.txtNome.Text.Equals("") || this.txtNumero.Text.Equals(""));
        }

        //==================================================================================
        private void LimparCampos()
        {
            this.txtNome.Text = "";
            this.txtNumero.Text = "";
            this.txtNome.Focus();
        }

        //==================================================================================
        private void ListBoxContatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnApagar.Enabled = true;
            this.btnEditar.Enabled = true;
        }

        //==================================================================================
        private void ConstruirLista()
        {
            //if (Geral.listaContatos == null) return;

            //Limpa a listBox
            this.listBoxContatos.Items.Clear();

            //Adiciona os contatos na ListBox
            foreach (Contato c in Geral.listaContatos)
            {
                this.listBoxContatos.Items.Add(c.nome + " | " + c.telefone);
            }

            //Atualiza o total de contatos salvos
            int totContatos = Geral.listaContatos.Count;
            this.lblTotContatos.Text = "Salvos: " + totContatos;
        }

        //==================================================================================
        private void RemoverContatoSelecionado()
        {
            Geral.listaContatos.RemoveAt(this.listBoxContatos.SelectedIndex);
        }
    }
}
