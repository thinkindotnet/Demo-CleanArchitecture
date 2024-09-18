namespace MyApp.xUnitTests.Common;


[CollectionDefinition(nameof(MyTestQueryCollection))]
public class MyTestQueryCollection
    : ICollectionFixture<MyTestFixture>
{
}
