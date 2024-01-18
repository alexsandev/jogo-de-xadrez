using Xadrez.Tabuleiro;

namespace Xadrez.Xadrez
{
    public class Peao : Peca
    {
        private readonly Partida _partida;

        public Peao(Tabuleiro.Tabuleiro tabuleiro, Cor cor, Partida partida) : base(tabuleiro, cor)
        {
            _partida = partida;
        }

        private bool PodeMover(Posicao posicao)
        {
            return Tabuleiro.PosicaoValida(posicao) && Tabuleiro.GetPeca(posicao) == null;
        }

        private bool ExisteInimigo(Posicao posicao)
        {
            return Tabuleiro.PosicaoValida(posicao) && Tabuleiro.GetPeca(posicao) != null && Tabuleiro.GetPeca(posicao).Cor != Cor;
        }

        private bool PossivelEnPassant(Posicao posicao)
        {
            return Tabuleiro.PosicaoValida(posicao) && Tabuleiro.GetPeca(posicao) == _partida.vuneravelEnPassant;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao posicao = new Posicao(0,0);
            if(Cor == Cor.Branca)
            {
                bool posicaoAnteriorLivre = false;

                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if(PodeMover(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                    posicaoAnteriorLivre = true;
                }

                posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if(PodeMover(posicao) && QuantidadeDeMovimentos == 0 && posicaoAnteriorLivre)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if(ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if(ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                if(Posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if(PossivelEnPassant(esquerda))
                    {
                        matriz[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if(PossivelEnPassant(direita))
                    {
                        matriz[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
            }
            else
            {
                bool posicaoAnteriorLivre = false;

                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if(PodeMover(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                    posicaoAnteriorLivre = true;
                }

                posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if(PodeMover(posicao) && QuantidadeDeMovimentos == 0 && posicaoAnteriorLivre)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if(ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if(ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                if(Posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if(PossivelEnPassant(esquerda))
                    {
                        matriz[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if(PossivelEnPassant(direita))
                    {
                        matriz[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return matriz;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}