using Xadrez.Tabuleiro;

namespace Xadrez.Xadrez
{
    public class Rei : Peca
    {
        private readonly Partida _partida;

        public Rei(Tabuleiro.Tabuleiro tabuleiro, Cor cor, Partida partida) : base(tabuleiro, cor)
        {
            _partida = partida;
        }

        public override string ToString()
        {
            return "R";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            //Norte
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //Nordeste
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //Leste
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //Sudeste
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //Sul
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //Sudoeste
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //Oeste
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //Noroeste
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && (Tabuleiro.GetPeca(posicao) == null || Tabuleiro.GetPeca(posicao).Cor != Cor))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //Jogada especial: Roque
            if(ReiDisponivelParaRoque())
            {
                Posicao posicaoTorreDireita = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (TorreDisponivelParaRoque(posicaoTorreDireita))
                {
                    matriz[Posicao.Linha, Posicao.Coluna + 2] = true;
                }

                Posicao posicaoTorreEsquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (TorreDisponivelParaRoque(posicaoTorreEsquerda))
                {
                    matriz[Posicao.Linha, Posicao.Coluna - 2] = true;
                }
            }

            return matriz;
        }

        private bool TorreDisponivelParaRoque(Posicao posicaoDaTorre)
        {
            return Tabuleiro.PosicaoValida(posicaoDaTorre) && Tabuleiro.GetPeca(posicaoDaTorre) != null &&
                    Tabuleiro.GetPeca(posicaoDaTorre) is Torre && Tabuleiro.GetPeca(posicaoDaTorre).QuantidadeDeMovimentos == 0 && CaminhoLivreAteORei(posicaoDaTorre);
        }

        private bool ReiDisponivelParaRoque()
        {
            return QuantidadeDeMovimentos == 0 && !_partida.Xeque;
        }

        private bool CaminhoLivreAteORei(Posicao posicaoDaTorre)
        {   
            Posicao ladoDireitoDoRei = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
            Posicao ladoEsquerdoDoRei = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
            return Tabuleiro.GetPeca(posicaoDaTorre).MovimentoPossivel(ladoEsquerdoDoRei) || Tabuleiro.GetPeca(posicaoDaTorre).MovimentoPossivel(ladoDireitoDoRei);
        }
    }
}