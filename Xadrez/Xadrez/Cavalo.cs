using Xadrez.Tabuleiro;

namespace Xadrez.Xadrez
{
    public class Cavalo : Peca
    {
        public Cavalo(Tabuleiro.Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
            
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao posicao = new Posicao(0,0);

            posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if(Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
            if(Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if(Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
            if(Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
            if(Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if(Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
            if(Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if(Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            return matriz;
        }

        public override string ToString()
        {
            return "C";
        }
    }
}