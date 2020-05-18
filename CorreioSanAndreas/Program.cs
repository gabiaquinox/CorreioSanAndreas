using CorreioSanAndreas.Data;
using CorreioSanAndreas.Servicos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CorreioSanAndreas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bem vindo!");
            Console.WriteLine("");
            Console.Write("Favor informar o caminho onde estão os arquivos: ");
            string caminhoLeitura = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Deseja salvar o resultado na mesma pasta? S/N");
            string caminhoSalvar = Console.ReadLine().Substring(0);

            if (caminhoSalvar == "S")
                caminhoSalvar = caminhoLeitura + @"rotas.txt";
            else
            {
                Console.Write("Informar, por gentileza, o caminho onde deseja salvar o resultado: ");
                caminhoSalvar = Console.ReadLine() + @"rotas.txt";
            }

            var container = new Controller(new GerenciadorArquivoServico(), new RotasServico());
            container.Executar(caminhoLeitura, caminhoSalvar);

            Console.WriteLine("Concluído!");

            Console.ReadKey();
        }
    }
}