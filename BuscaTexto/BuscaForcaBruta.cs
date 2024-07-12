using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaTexto {
    class BuscaForcaBruta {
        public static int forcaBruta(String p, String t) { // p => padraoBusca // t => texto
            int i, j, aux;
            int m = p.Length; // tamanho do padrao
            int n = t.Length; // tamanho do texto

            for (i = 0; i < n; i++)  // percorre letra por letra do texto
            {
                aux = i; // posição atual

                // percorre o padrao de busca
                // se for menor/enquanto for menor que texto 
                for (j = 0; j < m && aux < n; j++) 
                {
                    // compara a letra do texto da posicao atual com a letra do padrao de busca 
                    // ? => caractere coringa
                    if (t[aux] != p[j] && p[j] != '?') 
                        break;
                    aux++;
                }
                if (j == m)
                    return i;
            }

            return -1;  // nao encontrou o padrao de busca
        }
    }
}
