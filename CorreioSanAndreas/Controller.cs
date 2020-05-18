using CorreioSanAndreas.Data;
using CorreioSanAndreas.Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorreioSanAndreas
{
    public class Controller
    {
        static readonly string pastaRaiz = @"C:\temp\";
        static readonly string arquivoTrechos = @"\trechos.txt";
        static readonly string arquivoEncomendas = @"\encomendas.txt";

        private readonly IGerenciadorArquivoServico _gerenciadorArquivoServico;
        private readonly IRotasServico _rotasServico;
        public Controller(IGerenciadorArquivoServico gerenciadorArquivoServico, IRotasServico rotasServico)
        {
            _gerenciadorArquivoServico = gerenciadorArquivoServico ??
            throw new ArgumentNullException(nameof(gerenciadorArquivoServico));

            _rotasServico = rotasServico ??
            throw new ArgumentNullException(nameof(rotasServico));
        }

        public void Executar(string caminhoLeitura, string caminhoResultado)
        {
            var retornoTrechos = LerTxt(string.Format("{0}{1}", String.IsNullOrEmpty(caminhoLeitura) ? pastaRaiz : caminhoLeitura, arquivoTrechos));
            var retornoEncomendas = LerTxt(string.Format("{0}{1}", String.IsNullOrEmpty(caminhoResultado) ? pastaRaiz : caminhoResultado, arquivoEncomendas));
            
            var trajetos = CalcularTrajetos(MapearEncomendas(retornoEncomendas), MapearRotas(retornoTrechos));

            SalvarResultado(caminhoResultado, trajetos);
        }

        private List<Rota> MapearRotas(IList<object> retornoTrechos)
        {
            var rotas = new List<Rota>();
            int a;
            foreach (string linha in retornoTrechos)
            {
                if (!string.IsNullOrEmpty(linha))
                {
                    var rota = new Rota
                    {
                        Origem = linha.Split(' ')[0].ToString(),
                        Destino = linha.Split(' ')[1].ToString(),
                        DistanciaDias = int.TryParse(linha.Split(' ')[2], out a) ? Convert.ToInt32(linha.Split(' ')[2]) : 0
                    };
                    rotas.Add(rota);
                }
            }
            return rotas;
        }

        private List<Encomenda> MapearEncomendas(IList<object> retornoEncomendas)
        {
            var encomendas = new List<Encomenda>();
            foreach (string linha in retornoEncomendas)
            {
                if (!string.IsNullOrEmpty(linha))
                {
                    var encomenda = new Encomenda
                    {
                        Origem = linha.Split(' ')[0].ToString(),
                        Destino = linha.Split(' ')[1].ToString()
                    };
                    encomendas.Add(encomenda);
                }
            }
            return encomendas;
        }
        private IList<object> LerTxt(string caminho) => _gerenciadorArquivoServico.LerTxtPorLinha(caminho);
        private void SalvarResultado(string caminho, string[] resultado) => _gerenciadorArquivoServico.Salvar(caminho, resultado);

        private string[] CalcularTrajetos(List<Encomenda> encomendas, List<Rota> rotas) => _rotasServico.RetornarTrajetos(encomendas, rotas);

    }
}
