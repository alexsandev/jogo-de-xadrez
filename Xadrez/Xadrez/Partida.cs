using Xadrez.Tabuleiro;
using System.Collections.Generic;
using System.Linq;

namespace Xadrez.Xadrez
{
    public class Partida
    {
        public Tabuleiro.Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        public bool Xeque { get; private set; }
        private ISet<Peca> _pecasEmJogo;
        private ISet<Peca> _pecasCapturadas;

        public Partida()
        {
            Tabuleiro = new Tabuleiro.Tabuleiro(8,8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            _pecasEmJogo = new HashSet<Peca>();
            _pecasCapturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.RetirarPeca(origem);
            peca.IncrementarQuantidadeDeMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(peca, destino);
            if(pecaCapturada != null)
            {
                _pecasCapturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = Tabuleiro.RetirarPeca(destino);
            peca.DecrementarQuantidadeDeMovimentos();
            Tabuleiro.ColocarPeca(peca, origem);
            if(pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                _pecasCapturadas.Remove(pecaCapturada);
            }
        }

        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);

            if(EstaEmXeque(JogadorAtual))
            {
                DesfazerMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Xeque = EstaEmXeque(Adversario(JogadorAtual)) ? true : false;

            if(Xequemate(Adversario(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                JogadorAtual = JogadorAtual == Cor.Branca ? Cor.Preta : Cor.Branca;
            }  
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
            if(!Tabuleiro.GetPeca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("A posicação de destino não é um movimento possivel!");
            }
        }

        private void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            _pecasEmJogo.Add(peca);
        }

        private void ColocarPecas()
        {
            //Pretas
            ColocarNovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preta));

            //Brancas
            ColocarNovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branca));
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
            return _pecasEmJogo.Where(x => x.Cor == cor).Except(PecasCapturadas(cor)).ToHashSet();
        }

        private Cor Adversario(Cor cor)
        {
            return cor == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        private Peca Rei(Cor cor)
        {
            foreach(Peca p in PecasEmJogo(cor))
            {
                if(p is Rei)
                {
                    return p;
                }
            }
            return null;
        }

        private bool EstaEmXeque(Cor cor)
        {
            Peca rei = Rei(cor);

            if(rei == null)
            {
                throw new TabuleiroException($"Não existe um rei {cor} no tabuleiro!");
            }

            foreach(Peca p in PecasEmJogo(Adversario(cor)))
            {   
                if(p.MovimentosPossiveis()[rei.Posicao.Linha, rei.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        private bool Xequemate(Cor cor)
        {
            if(!EstaEmXeque(cor))
            {
                return false;
            }
            foreach(Peca peca in PecasEmJogo(cor))
            {
                bool[,] movimentosPossiveis = peca.MovimentosPossiveis();
                for(int i = 0; i < Tabuleiro.Linhas; i++)
                {
                    for(int j = 0; j < Tabuleiro.Colunas; j++)
                    {
                        if(movimentosPossiveis[i,j])
                        {
                            Posicao origem = peca.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutarMovimento(origem, destino);
                            bool estaEmXeque = EstaEmXeque(cor);
                            DesfazerMovimento(origem, destino, pecaCapturada);
                            if(!estaEmXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}