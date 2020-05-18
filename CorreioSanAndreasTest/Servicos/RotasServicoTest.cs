using CorreioSanAndreas.Data;
using CorreioSanAndreas.Servicos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
using Xunit;

namespace CorreioSanAndreasTest.Servicos
{

    [TestClass]
    public class RotasServicoTest
    {
        List<Rota> rotas = new List<Rota>
        {
            new Rota { Origem = "LS", Destino = "SF", DistanciaDias = 1 },
            new Rota { Origem = "SF", Destino = "LS", DistanciaDias = 2 },
            new Rota { Origem = "LS", Destino = "LV", DistanciaDias = 1 },
            new Rota { Origem = "LV", Destino = "LS", DistanciaDias = 1 },
            new Rota { Origem = "SF", Destino = "LV", DistanciaDias = 2 },
            new Rota { Origem = "LV", Destino = "SF", DistanciaDias = 2 },
            new Rota { Origem = "LS", Destino = "RC", DistanciaDias = 1 },
            new Rota { Origem = "RC", Destino = "LS", DistanciaDias = 2 },
            new Rota { Origem = "SF", Destino = "WS", DistanciaDias = 1 },
            new Rota { Origem = "WS", Destino = "SF", DistanciaDias = 2 },
            new Rota { Origem = "LV", Destino = "BC", DistanciaDias = 1 },
            new Rota { Origem = "BC", Destino = "LV", DistanciaDias = 1 }
        };

        [TestMethod]
        public void RetornarMenorTrajeto()
        {
            //arrange
            RotasServico rotasServico = new RotasServico();
            var encomendas = new List<Encomenda>
            {
                new Encomenda { Destino = "WS", Origem = "SF" },
                new Encomenda { Origem = "LS", Destino = "BC" },
                new Encomenda { Origem = "WS", Destino = "BC" }
            };

            //act
            var resultado = rotasServico.RetornarTrajetos(encomendas, rotas);

            //assert
            Assert.AreEqual("SF WS 1", resultado[0].ToString());
            Assert.AreEqual("LS LV BC 2", resultado[1].ToString());
            Assert.AreEqual("WS SF LV BC 5", resultado[2].ToString());
        }

        [TestMethod]
        public void TrechoNaoExistente()
        {
            //arrange
            RotasServico rotasServico = new RotasServico();
            var encomendas = new List<Encomenda>
            {
                new Encomenda { Destino = "GA", Origem = "SF" }
            };

            //act
            var resultado = rotasServico.RetornarTrajetos(encomendas, rotas);

            //assert
            Assert.AreEqual(null, resultado[0]);
        }

        [TestMethod]
        public void RetornarVazio()
        {
            //arrange
            RotasServico rotasServico = new RotasServico();
            var encomendas = new List<Encomenda>();
            
            //act
            var resultado = rotasServico.RetornarTrajetos(encomendas, rotas);

            //assert
            Assert.AreEqual(0, resultado.Count());
        }

    }
}
