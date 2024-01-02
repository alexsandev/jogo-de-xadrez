using System;

namespace Xadrez
{
    class Program
    {
        static void Main()
        {
            Tabuleiro.Tabuleiro tabuleiro = new Tabuleiro.Tabuleiro(8,8);
            Console.WriteLine(tabuleiro.GetPecas().Length);
        }
    }
}
