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

            while (!partida.Terminada)
            {
                try
                {
                    Tela.ImprimirPartida(partida);

                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().toPosicao();
                    partida.ValidarPosicaoDeOrigem(origem);

                    Tela.ImprimirPartida(partida, origem);

                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().toPosicao();
                    partida.ValidarPosicaoDeDestino(origem, destino);

                    partida.RealizarJogada(origem, destino);
                }
                catch (TabuleiroException e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }

            Tela.ImprimirPartida(partida);
        }
    }
}
