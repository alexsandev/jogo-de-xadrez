using System;
using Xadrez.Tabuleiro;
using Xadrez.Xadrez;

namespace Xadrez
{
    class Program
    {
        static void Main()
        {
            Tabuleiro.Tabuleiro tabuleiro = new Tabuleiro.Tabuleiro(8,8);
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(0,0));
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(1,3));
            tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Preta), new Posicao(2,4));
            Tela.ImprimirTabuleiro(tabuleiro);

            PosicaoXadrez px = new PosicaoXadrez(1, 'a');

            Console.WriteLine(px);
            Console.WriteLine(px.toPosicao());
        }
    }
}
