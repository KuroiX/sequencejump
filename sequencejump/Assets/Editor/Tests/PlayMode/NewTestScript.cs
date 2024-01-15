using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine.TestTools;

public interface IExample
{
}

public class NewTestScript
{
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        IExample example = Substitute.For<IExample>();
        yield return null;
        Assert.Pass();
    }
}
