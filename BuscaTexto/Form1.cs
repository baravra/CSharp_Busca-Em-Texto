using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuscaTexto {
    public partial class Form1 : Form {

        string ultPadraoBusca;
        public Form1() {
            InitializeComponent();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e) {
            texto.Text = "";
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show(this,
               "Busca em Texto - 2024/1\n\nDesenvolvido por:\n72300043 - Bárbara Leão \n72201207 - Kaio Maia\n\nProf. Virgílio Borges de Oliveira\n\nAlgoritmos e Estruturas de Dados II\nFaculdade COTEMIG\nSomente para fins didáticos.",
               "Sobre o trabalho...",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information);
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e) {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files (*.txt)|*.txt|Rich Text files (*.rtf)|*.rtf";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileInfo file = new FileInfo(ofd.FileName);
                string content;

                using(StreamReader sr = new StreamReader(file.FullName,Encoding.Default))
                {
                    content = sr.ReadToEnd();
                }
                texto.Text = content;
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void forçaBrutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string padrao = ReceberPadraoEValidar();
            if (padrao == null)
                return;

            texto.SelectAll();
            texto.SelectionBackColor = Color.White;

            int indexMatch = -padrao.Length;
            while(indexMatch != -1)
            {
                texto.DeselectAll();

                indexMatch += padrao.Length;
                int idx = BuscaForcaBruta.forcaBruta(padrao, texto.Text.Substring(indexMatch) );
                indexMatch = idx == -1 ? -1 : indexMatch + idx;

                if (idx == -1)
                    break;

                Thread.Sleep(1);
                ColorirMatches(indexMatch, padrao.Length );
            }
            ultPadraoBusca = padrao;
        }

        private void booyerMooreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string padrao = ReceberPadraoEValidar();
            if (padrao == null)
                return;

            texto.SelectAll();
            texto.SelectionBackColor = Color.White;

            int indexMatch = -padrao.Length;
            while (indexMatch != -1)
            {
                texto.DeselectAll();

                indexMatch += padrao.Length;
                int idx = BuscaBoyerMoore.BMSearch(padrao, texto.Text.Substring(indexMatch));
                indexMatch = idx == -1 ? -1 : indexMatch + idx;

                if (idx == -1)
                    break;

                Thread.Sleep(1);
                ColorirMatches(indexMatch, padrao.Length);
            }
            ultPadraoBusca = padrao;


        }

        private void kMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string padrao = ReceberPadraoEValidar();
            if (padrao == null)
                return;

            texto.SelectAll();
            texto.SelectionBackColor = Color.White;

            int indexMatch = -padrao.Length;
            while (indexMatch != -1)
            {
                texto.DeselectAll();

                indexMatch += padrao.Length;
                int idx = BuscaKMP.KMPSearch(padrao, texto.Text.Substring(indexMatch));
                indexMatch = idx == -1 ? -1 : indexMatch + idx;

                if (idx == -1)
                    break;

                Thread.Sleep(1);
                ColorirMatches(indexMatch, padrao.Length);
            }

            ultPadraoBusca = padrao;
        }

        private void rabinKarpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string padrao = ReceberPadraoEValidar();
            if (padrao == null)
                return;

            texto.SelectAll();
            texto.SelectionBackColor = Color.White;

            int indexMatch = -padrao.Length;
            while (indexMatch != -1)
            {
                texto.DeselectAll();

                indexMatch += padrao.Length;
                int idx = BuscaRabinKarp.RKSearch(padrao, texto.Text.Substring(indexMatch));
                indexMatch = idx == -1 ? -1 : indexMatch + idx;

                if (idx == -1)
                    break;

                Thread.Sleep(1);
                ColorirMatches(indexMatch, padrao.Length);
            }
            ultPadraoBusca = padrao;
        }

        private string ReceberPadraoEValidar()
        {
            string padrao = Interaction.InputBox("Informe o padrão de busca a ser encontrado no texto", "Padrão de Busca");
            if (string.IsNullOrEmpty(padrao))
            {
                MessageBox.Show("Preencha corretamente. Padrão vazio!", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            if (texto.Text.Length == 0)
            {
                MessageBox.Show("Informe o texto para realizar a busca!", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            if (padrao.Length > texto.Text.Length)
            {
                MessageBox.Show("O padrão deve ser menor que o texto para realizar a busca!", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return padrao;
        }

        private void ColorirMatches(int indexMatch, int tamanhoPadrao )
        {
            try
            {
                // Aleatorizando cor de fundo
                int red = 0, green = 0, blue = 0;
                Random aleatorio = new Random();
                red = aleatorio.Next(255);
                green = aleatorio.Next(255);
                blue = aleatorio.Next(255);

                Color corFundo = Color.FromArgb(red, green, blue);

                // Colorindo padrao encontrado
                texto.SelectionStart = indexMatch;
                texto.SelectionLength = tamanhoPadrao;
                texto.SelectionBackColor = corFundo;
                texto.DeselectAll();
            }
            catch (Exception)
            {
            }
        }

        private void Replace(string padraoBusca)
        {
            FormReplace frm = new FormReplace();
            frm.ShowDialog();

            if (frm.aplicarPadrao)
            {
                texto.Text = texto.Text.Replace(padraoBusca, frm.padraoSubstituir);
                MessageBox.Show("Substituição concluída com sucesso!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ultPadraoBusca))
            {
                MessageBox.Show("Padrão de busca vazio. Não é possivel realizar a substituicao","ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Replace(ultPadraoBusca);
        }
    }
}
