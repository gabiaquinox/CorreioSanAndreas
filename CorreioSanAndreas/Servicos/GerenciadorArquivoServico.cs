using CorreioSanAndreas.Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace CorreioSanAndreas.Servicos
{
    public class GerenciadorArquivoServico : IGerenciadorArquivoServico
    {
        public IList<object> LerTxtPorLinha(string caminho)
        {
            if (File.Exists(caminho))
            {
                string[] lines = File.ReadAllLines(caminho);
                return lines;
            }
            return null;
        }

        public void Salvar(string caminho, string[] resultado)
        {
            File.WriteAllLines(caminho, resultado);
        }
    }
}
