using CorreioSanAndreas.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorreioSanAndreas.Servicos.Interfaces
{
    public interface IRotasServico
    {
        string[] RetornarTrajetos(List<Encomenda> encomendas, List<Rota> rotas);
    }
}
