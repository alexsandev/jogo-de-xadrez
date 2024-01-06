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
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tabuleiro);
                    Console.WriteLine($"\nTurno: {partida.Turno}");
                    Console.WriteLine($"Jogador atual: {partida.JogadorAtual}");

                    Console.WriteLine();

                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().toPosicao();
                    partida.ValidarPosicaoDeOrigem(origem);

                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tabuleiro, partida.Tabuleiro.GetPeca(origem).MovimentosPossiveis());
                    Console.WriteLine($"\nTurno: {partida.Turno}");
                    Console.WriteLine($"Jogador atual: {partida.JogadorAtual}");

                    Console.WriteLine();

                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().toPosicao();
                    Console.WriteLine(destino);
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
        }
    }
}
