using Xadrez.Tabuleiro;

namespace Xadrez.Xadrez
{
    public class Rainha : Peca
    {
        public Rainha(Tabuleiro.Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
            
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            //Norte
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if(Tabuleiro.GetPeca(posicao) != null) break;
                posicao.Linha--;
            }

            //Leste
            posicao.DefinirValores(Posicao.Linha , Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if(Tabuleiro.GetPeca(posicao) != null) break;
                posicao.Coluna++;
            }

            //Sul
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if(Tabuleiro.GetPeca(posicao) != null) break;
                posicao.Linha++;
            }

            //Oeste
            posicao.DefinirValores(Posicao.Linha , Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if(Tabuleiro.GetPeca(posicao) != null) break;
                posicao.Coluna--;
            }

            //Nordeste
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if(Tabuleiro.GetPeca(posicao) != null) break;
                posicao.Linha--;
                posicao.Coluna++;
            }

            //Sudeste
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if(Tabuleiro.GetPeca(posicao) != null) break;
                posicao.Linha++;
                posicao.Coluna++;
            }

            //Sudoeste
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if(Tabuleiro.GetPeca(posicao) != null) break;
                posicao.Linha++;
                posicao.Coluna--;
            }

            //Noroeste
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if(Tabuleiro.GetPeca(posicao) != null) break;
                posicao.Linha--;
                posicao.Coluna--;
            }

            return matriz;
        }

        public override string ToString()
        {
            return "Q";
        }
    }
}