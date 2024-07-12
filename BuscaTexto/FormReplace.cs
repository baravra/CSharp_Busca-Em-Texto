using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuscaTexto
{
    public partial class FormReplace : Form
    {
        public string padraoSubstituir;
        public bool aplicarPadrao;

        public FormReplace()
        {
            InitializeComponent();
            aplicarPadrao = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            padraoSubstituir = txtPadraoSubs.Text;
            aplicarPadrao = true;

            this.Close();
        }
    }
}
