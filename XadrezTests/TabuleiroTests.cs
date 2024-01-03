using Xadrez.Tabuleiro;

namespace XadrezTests;

public class TabuleiroTests
{
    private Tabuleiro tabuleiro = new Tabuleiro(8,8);

    [Fact]
    public void PosicaoEValida()
    {
        Assert.True(tabuleiro.PosicaoValida(new Posicao(1,8)));
    }

    [Fact]
    public void PosicaoEInvalida()
    {
        Assert.False(tabuleiro.PosicaoValida(new Posicao(1,9)));
    }
}