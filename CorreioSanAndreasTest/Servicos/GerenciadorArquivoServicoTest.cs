using CorreioSanAndreas.Servicos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorreioSanAndreasTest.Servicos
{
    [TestClass]
    public class GerenciadorArquivoServicoTest
    {
        [TestMethod]
        public void LerTxtComSucesso()
        {
            
        }

        [TestMethod]
        public void CaminhoNaoEncontrado()
        {
            GerenciadorArquivoServico gerenciadorArquivo = new GerenciadorArquivoServico();
            var retorno = gerenciadorArquivo.LerTxtPorLinha(@"C:\User\teste.txt");
            Assert.IsNull(retorno);
        }

        [TestMethod]
        public void SalvarTxt()
        {
            GerenciadorArquivoServico gerenciadorArquivo = new GerenciadorArquivoServico();
            gerenciadorArquivo.Salvar(@"C:\temp\teste.txt", new string[] { "teste", "teste"});

            var retorno = gerenciadorArquivo.LerTxtPorLinha(@"C:\temp\teste.txt");

            Assert.AreEqual("teste", retorno[0].ToString());
        }
    }
}
