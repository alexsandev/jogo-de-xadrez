using System;

namespace Xadrez
{
    public class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro.Tabuleiro tabuleiro)
        {
            for(int i = 0; i < tabuleiro.Linhas; i++)
            {
                for(int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if(tabuleiro.GetPeca(i,j) != null)
                    {
                        Console.Write($" {tabuleiro.GetPeca(i,j)} ");
                    }
                    else
                    {
                        Console.Write($" - ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}