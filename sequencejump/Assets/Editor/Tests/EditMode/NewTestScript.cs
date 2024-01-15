using NSubstitute;
using NUnit.Framework;

public interface IExample
{
}

public class NewTestScript
{
    [Test]
    public void NewTestScriptSimplePasses()
    {
        IExample example = Substitute.For<IExample>();
        Assert.Pass();
    }
}
