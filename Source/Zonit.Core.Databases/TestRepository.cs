using Microsoft.Extensions.DependencyInjection;

namespace Zonit.Core.Databases;

internal interface IntId { }
internal interface GuidId { }

internal class TestIntModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

internal class TestGuidModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

internal interface ITest<T>
{
    public bool TestFunction();

    // Tutaj zwróć ID, w zależnosci od modelu. Jedem może mieć int a inny Guid
    public T TestFunction2();

    public object GetById();
}

internal class TestAbstract<T> : ITest<T>
{
    public bool TestFunction()
    {
        return true;
    }

    public T TestFunction2() 
    {         
        return default!;
    }

    public object GetById()
    {
        return default!;
    }
}

internal interface ITestRepository { }
internal class TestRepository : TestAbstract<TestIntModel>, ITestRepository { }


internal interface ITestGuidRepository { }
internal class TestGuidRepository : TestAbstract<TestGuidModel>, ITestGuidRepository { }


internal interface ITestRepository2 : ITest<TestIntModel> { }
internal class TestRepository2 : TestAbstract<TestIntModel>, ITestRepository2 { }


internal class TestProgram
{
    public static void Main()
    {
        IServiceCollection service = new ServiceCollection();

        service.AddTransient<ITest<TestIntModel>, TestAbstract<TestIntModel>>();
        service.AddTransient<ITest<TestIntModel>, TestRepository>();
        service.AddTransient<ITestRepository, TestRepository>();
        service.AddTransient<ITestRepository2, TestRepository2>();
    }
}