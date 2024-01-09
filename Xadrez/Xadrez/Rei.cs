using Xadrez.Tabuleiro;

namespace Xadrez.Xadrez
{
    public class Rei : Peca
    {
        public Rei(Tabuleiro.Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
            
        }

        public override string ToString()
        {
            return "R";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0,0);

            //Norte
            posicao.DefinirValores(Posicao.Linha - 1 , Posicao.Coluna);
            if(Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //Nordeste
            posicao.DefinirValores(Posicao.Linha - 1 , Posicao.Coluna + 1);
            if(Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //Leste
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if(Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //Sudeste
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if(Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //Sul
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if(Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //Sudoeste
            posicao.DefinirValores(Posicao.Linha + 1 , Posicao.Coluna - 1);
            if(Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //Oeste
            posicao.DefinirValores(Posicao.Linha , Posicao.Coluna - 1);
            if(Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //Noroeste
            posicao.DefinirValores(Posicao.Linha - 1 , Posicao.Coluna - 1);
            if(Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            return matriz;
        }
    }
}