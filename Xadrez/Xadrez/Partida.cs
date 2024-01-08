using Xadrez.Tabuleiro;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Xadrez.Xadrez
{
    public class Partida
    {
        public Tabuleiro.Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> _pecas;
        private HashSet<Peca> _pecasCapturadas;

        public Partida()
        {
            Tabuleiro = new Tabuleiro.Tabuleiro(8,8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            ColocarPecas();
            _pecas = new HashSet<Peca>();
            _pecasCapturadas = new HashSet<Peca>();
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.RetirarPeca(origem);
            peca.IncrementarQuantidadeDeMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(peca, destino);
            if(pecaCapturada != null)
            {
                _pecasCapturadas.Add(pecaCapturada);
            }
        }

        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            ExecutarMovimento(origem, destino);
            Turno++;
            JogadorAtual = JogadorAtual == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        public void ValidarPosicaoDeOrigem(Posicao origem)
        {   
            if(!Tabuleiro.PosicaoValida(origem))
            {
                throw new TabuleiroException("A posição escolhida não existe!");
            }
            if(Tabuleiro.GetPeca(origem) == null)
            {
                throw new TabuleiroException("Não há peças na posição escolhida!");
            }
            if(Tabuleiro.GetPeca(origem).Cor != JogadorAtual)
            {
                throw new TabuleiroException("A peça escolhida não é sua!");
            }
            if(!Tabuleiro.GetPeca(origem).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não existem movimentos possiveis para está peça!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if(!Tabuleiro.GetPeca(origem).PodeMover(destino))
            {
                throw new TabuleiroException("A posicação de destino não é um movimento possivel!");
            }
        }

        private void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            //_pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('a', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branca));
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            return _pecasCapturadas.Where(x => x.Cor == cor).ToHashSet();
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            return _pecas.Where(x => x.Cor == cor).Except(PecasCapturadas(cor)).ToHashSet();
        }
    }
}