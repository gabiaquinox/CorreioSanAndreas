using CorreioSanAndreas.Data;
using CorreioSanAndreas.Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorreioSanAndreas.Servicos
{
    public class RotasServico : IRotasServico
    {
        public string[] RetornarTrajetos(List<Encomenda> encomendas, List<Rota> rotas)
        {
            string[] trajetos = new string[encomendas.Count];
            int count = 0;

            foreach (var encomenda in encomendas)
            {
                var rotasTrajeto = EncontrarTrajeto(encomenda, rotas);

                if (rotasTrajeto.Count > 0)
                {

                    string cidades = "";
                    for (int i = rotasTrajeto.Count - 1; i >= 0; i--)
                    {
                        cidades = String.Concat(cidades, " ", rotasTrajeto[i].Origem);
                    }

                    trajetos[count] = String.Format("{0} {1} {2}", cidades.TrimStart(), rotasTrajeto[0].Destino, rotasTrajeto.Sum(t => t.DistanciaDias));

                }
                count++;
            }
            return trajetos;
        }

        private List<Rota> EncontrarTrajeto(Encomenda encomenda, List<Rota> rotas)
        {
            var rotasTrajeto = new List<Rota>();

            var rotasOrigem = rotas.Where(r => r.Origem == encomenda.Origem).OrderBy(r => r.DistanciaDias);
            var rotasDestino = rotas.Where(r => r.Destino == encomenda.Destino).OrderBy(r => r.DistanciaDias);

            if (!rotasOrigem.Where(r => r.Destino == encomenda.Destino).Any())
            {
                foreach (var destino in rotasDestino)
                {
                    var rAux = rotas.Where(r => r.Destino == destino.Origem).OrderBy(r => r.DistanciaDias);
                    var rt = rAux.Where(x => x.Origem == encomenda.Origem);

                    rotasTrajeto.Add(destino);

                    if (rt.Any())
                    {
                        rotasTrajeto.Add(rt.FirstOrDefault());
                    }
                    else
                    {
                        var x = rotasOrigem.Join(rAux, r => r.Destino, a => a.Origem, (r, a) => new { rotasOrigem = r, rAux = a }).Where(x => x.rotasOrigem.Origem == encomenda.Origem).ToList();
                        foreach (var item in x)
                        {
                            rotasTrajeto.Add(item.rAux);
                            rotasTrajeto.Add(item.rotasOrigem);
                        }
                    }

                }
            }
            else rotasTrajeto.Add(rotasOrigem.Where(r => r.Destino == encomenda.Destino).FirstOrDefault());

            return rotasTrajeto;
        }
    }
}
