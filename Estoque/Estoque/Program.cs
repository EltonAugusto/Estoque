using System;
using System.Collections.Generic;
using static System.Console;

namespace EstoqueDeProdutos
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Digite o código do Produto");
            var codigoProduto = ReadLine();

            WriteLine($"Selecione o Tipo do Produto\n 1: {nameof(TipoProduto.Perecivel)} \n 2: {nameof(TipoProduto.NaoPerecivel)}");
            var tipoProduto = ReadKey();

            var produto = new Produto(codigoProduto, converterEnumTipoProduto(tipoProduto));

            WriteLine("Confirmar entrada no Estoque? S/N");

            if(ReadKey().Key == ConsoleKey.S)
            {
                var estoque = new Estoque();
                estoque.Entrada(produto);
                WriteLine("Sucesso!");
            }
            else
            {
                WriteLine("Operação Cancelada!");
            }

            ReadKey();

        }

        private static TipoProduto converterEnumTipoProduto(ConsoleKeyInfo keyPressed)
        {
            switch (keyPressed.Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    return (TipoProduto)1;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    return (TipoProduto)2;
                default:
                    throw new Exception("Opção Inválida");
            }

        }

        public class Produto
        {
            public Produto(string nome, TipoProduto tipoProduto)
            {
                this.Nome = nome;
                this.TipoProduto = tipoProduto;
            }

            public string Nome { get; private set; }
            public TipoProduto TipoProduto { get; private set; }
            public int DiasValidade { get; private set; } = 360;

            public DateTime? calcularValidade(DateTime fabricacao) =>
                TipoProduto.Equals(TipoProduto.Perecivel) ? fabricacao.AddDays(DiasValidade) : new DateTime?();
        }

        public class Estoque
        {
            public Produto Produto { get; private set; }
            public DateTime? Validade { get; private set; }
            public DateTime Fabricacao { get; private set; }


            public void Entrada(Produto produto)
            {
                this.Produto = produto;
                this.Fabricacao = DateTime.Now;
                this.Validade = Produto.calcularValidade(this.Fabricacao);
            }

            public void Entrada(List<Produto> produtos)
            {
                produtos.ForEach(a => Entrada(a));
            }
        }

        public enum TipoProduto
        {
            Perecivel = 1,
            NaoPerecivel = 2
        }


    }
}
