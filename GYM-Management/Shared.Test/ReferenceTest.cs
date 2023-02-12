namespace Shared.Test;

using ArchUnitNET.Domain;
using ArchUnitNET.Domain.Extensions;
using ArchUnitNET.Loader;
using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using Customer.Application.Contracts;
using Customer.Host;
using GymManagement.API;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

public class ReferenceTest


{
    private static readonly Architecture Architecture =
        new ArchLoader().LoadAssemblies(typeof(WeatherForecast).Assembly).Build();

    [Fact]
    public void Customer_Reference_Check()
    {
         var namespacew = Architecture.Namespaces.First(x => x.Name == "GymManagement.API");

         namespacew.OnlyDependsOn("Customer.Host","Customer.Application");
namespacew.s
                    a.Should().Be(true);

    }
}