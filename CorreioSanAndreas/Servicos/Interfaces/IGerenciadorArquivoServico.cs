using CorreioSanAndreas.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorreioSanAndreas.Servicos.Interfaces
{
    public interface IGerenciadorArquivoServico
    {
        IList<object> LerTxtPorLinha(string caminho);
        void Salvar(string caminho, string[] resultado);
    }
}
