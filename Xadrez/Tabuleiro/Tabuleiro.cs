namespace Xadrez.Tabuleiro
{
    public class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] _pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            _pecas = new Peca[Linhas, Colunas];
        }

        public Peca GetPeca(int linha, int coluna)
        {
            return _pecas[linha, coluna];
        }

        public Peca GetPeca(Posicao posicao)
        {
            return _pecas[posicao.Linha, posicao.Coluna];
        }

        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            if(ExistePeca(posicao))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            _pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }

        public Peca RetirarPeca(Posicao posicao)
        {
            if(GetPeca(posicao) == null)
            {
                return null;
            }
            
            Peca pecaRetirada = GetPeca(posicao);
            pecaRetirada.Posicao = null;
            _pecas[posicao.Linha, posicao.Coluna] = null;
            return pecaRetirada;
        }

        public bool ExistePeca(Posicao posicao)
        {
            ValidarPosicao(posicao);
            return GetPeca(posicao) != null;
        }

        public bool PosicaoValida(Posicao posicao)
        {
            return posicao.Linha < 0 || posicao.Linha > Linhas - 1 || posicao.Coluna < 0 || posicao.Coluna > Colunas - 1 ? false : true;
        }

        public void ValidarPosicao(Posicao posicao)
        {
            if(!PosicaoValida(posicao))
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }
    }
}