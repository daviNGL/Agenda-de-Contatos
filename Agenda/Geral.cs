using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Agenda
{
    public static class Geral
    {
        public static String versao = "v1.0.0";

        //==================================================================================
        //Lista de contatos
        public static List<Contato> listaContatos;

        //==================================================================================
        public static void CarregarListaContatos()
        {

            //Especifica caminho e nome do arquivo a ser manipuladp.
            String caminho_documentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            String arquivo_contatos = @"\meus_contatos.txt";

            //Inicializa a lista
            listaContatos = new List<Contato>();

            //Se o arquivo txt não existir, encerra o método.
            if (!File.Exists(caminho_documentos + arquivo_contatos)) return;

            //Arquivo existe

            //Abre o arquivo.
            StreamReader reader = new StreamReader(caminho_documentos + arquivo_contatos, Encoding.Default);

            //Carrega os dados do arquivo na Lista de Contatos (listaContatos).
            while (!reader.EndOfStream)
            {
                String nome = reader.ReadLine();
                String tel = reader.ReadLine();
                listaContatos.Add(new Contato(nome, tel));
            }

            //Fecha o arquivo de texto.
            reader.Dispose();
        }

        //==================================================================================
        public static void GravarFicheiro()
        {
            //Espefifica o diretorio do arquivo de texto
            String diretorio = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + 
                @"\meus_contatos.txt";

            //Abrir o arquivo de texto em modo sobrescrita(append = false)
            StreamWriter arquivo = new StreamWriter(diretorio, false, Encoding.Default);

            //Percorre a lista atualizando o arquivo de texto
            foreach(Contato c in listaContatos)
            {
                arquivo.WriteLine(c.nome);
                arquivo.WriteLine(c.telefone);
            }

            //Encerra o arquivo
            arquivo.Dispose();
        }
    }
}