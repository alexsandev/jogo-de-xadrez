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
        public Peca vuneravelEnPassant { get; private set; }

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

            //Roque pequeno
            if(peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao posicaoInicialTorreDireita = new Posicao(origem.Linha, origem.Coluna + 3);
                Peca torreDireita = Tabuleiro.RetirarPeca(posicaoInicialTorreDireita);
                torreDireita.IncrementarQuantidadeDeMovimentos();
                Tabuleiro.ColocarPeca(torreDireita, new Posicao(origem.Linha, origem.Coluna + 1));
            }

            //Roque grande
            if(peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao posicaoInicialTorreEsquerda = new Posicao(origem.Linha, origem.Coluna - 4);
                Peca torreEsquerda = Tabuleiro.RetirarPeca(posicaoInicialTorreEsquerda);
                torreEsquerda.IncrementarQuantidadeDeMovimentos();
                Tabuleiro.ColocarPeca(torreEsquerda, new Posicao(origem.Linha, origem.Coluna - 1));
            }

            //En passant
            if(peca is Peao && destino.Coluna != origem.Coluna && pecaCapturada == null)
            {
                if(peca.Cor == Cor.Branca)
                {
                    pecaCapturada = Tabuleiro.RetirarPeca(new Posicao(destino.Linha + 1, destino.Coluna));
                    _pecasCapturadas.Add(pecaCapturada);
                }
                else
                {
                    pecaCapturada = Tabuleiro.RetirarPeca(new Posicao(destino.Linha - 1, destino.Coluna));
                    _pecasCapturadas.Add(pecaCapturada);
                }
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

            //Roque pequeno
            if(peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao posicaoInicialTorreDireita = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao posicaoTorreDireitaAposRoque = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torreDireita = Tabuleiro.RetirarPeca(posicaoTorreDireitaAposRoque);
                torreDireita.DecrementarQuantidadeDeMovimentos();
                Tabuleiro.ColocarPeca(torreDireita, posicaoInicialTorreDireita);
            }

            //Roque grande
            if(peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao posicaoInicialTorreEsquerda = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao posicaoTorreEsquerdaAposRoque = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torreEsquerda = Tabuleiro.RetirarPeca(posicaoTorreEsquerdaAposRoque);
                torreEsquerda.DecrementarQuantidadeDeMovimentos();
                Tabuleiro.ColocarPeca(torreEsquerda, posicaoInicialTorreEsquerda);
            }

            //En passant
            if(peca is Peao && destino.Coluna != origem.Coluna && pecaCapturada == vuneravelEnPassant)
            {
                Peca peaoCapturado = Tabuleiro.RetirarPeca(destino);
                if(peca.Cor == Cor.Branca)
                {
                    
                    Tabuleiro.ColocarPeca(peaoCapturado, new Posicao(destino.Linha + 1, destino.Coluna));
                }
                else
                {
                    Tabuleiro.ColocarPeca(peaoCapturado, new Posicao(destino.Linha - 1, destino.Coluna));
                }
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

            //En passant
            Peca peca = Tabuleiro.GetPeca(destino);
            if(peca is Peao && peca.QuantidadeDeMovimentos == 1 && (destino.Linha == origem.Linha + 2 || destino.Linha == origem.Linha - 2))
            {
                vuneravelEnPassant = peca;
            }
            else
            {
                vuneravelEnPassant = null;
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
            ColocarNovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preta)); 
            ColocarNovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rainha(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));

            //Brancas
            ColocarNovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('a', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(Tabuleiro, Cor.Branca)); 
            ColocarNovaPeca('c', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rainha(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branca));
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