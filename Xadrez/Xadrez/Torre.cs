using Xadrez.Tabuleiro;

namespace Xadrez.Xadrez
{
    public class Torre : Peca
    {
        public Torre(Tabuleiro.Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
            
        }

        public override string ToString()
        {
            return "T";
        }
    }
}