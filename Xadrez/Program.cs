using System;
using Xadrez.Tabuleiro;
using Xadrez.Xadrez;

namespace Xadrez
{
    class Program
    {
        static void Main()
        {
            Partida partida = new Partida();

            while(!partida.Terminada)
            {
                Console.Clear();
                Tela.ImprimirTabuleiro(partida.Tabuleiro);

                Console.WriteLine();
                
                Console.Write("Origem: ");
                Posicao origem = Tela.LerPosicaoXadrez().toPosicao();

                Console.Write("Destino: ");
                Posicao destino = Tela.LerPosicaoXadrez().toPosicao();

                partida.ExecutarMovimento(origem, destino);
            }
        }
    }
}
