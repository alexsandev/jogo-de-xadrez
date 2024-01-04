using System;
using Xadrez.Tabuleiro;
using Xadrez.Xadrez;

namespace Xadrez
{
    public class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro.Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i);
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    ImprimirPeca(tabuleiro.GetPeca(i, j), false);
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A  B  C  D  E  F  G  H");
        }

        public static void ImprimirTabuleiro(Tabuleiro.Tabuleiro tabuleiro, bool[,] movimentosPossiveis)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i);
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    ImprimirPeca(tabuleiro.GetPeca(i, j), movimentosPossiveis[i,j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A  B  C  D  E  F  G  H");
        }

        public static void ImprimirPeca(Peca peca, bool movimento)
        {
            ConsoleColor corPadrao = Console.BackgroundColor;
            if(movimento)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
            }

            if (peca != null)
            {
                if (peca.Cor == Cor.Branca)
                {
                    Console.Write($" {peca} ");
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($" {peca} ");
                    Console.ForegroundColor = aux;
                }
            }
            else
            {
                Console.Write($" - ");
            }
            Console.BackgroundColor = corPadrao;
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string entrada = Console.ReadLine();
            char coluna = entrada[0];
            int linha = int.Parse(entrada[1].ToString());
            return new PosicaoXadrez(coluna, linha);
        }
    }
}