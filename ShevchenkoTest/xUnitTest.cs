namespace ShevchenkoTest;

public class TestInit
{
    int Add(int x, int y)
    {
        return x + y;
    }
    
    [Fact]
    public void TestXUnit()
    {
        Assert.Equal(4, Add(2, 2));
    }
}