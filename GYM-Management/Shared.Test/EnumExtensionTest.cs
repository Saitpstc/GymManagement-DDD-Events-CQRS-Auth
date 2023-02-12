namespace Shared.Test;

using System.ComponentModel;
using Shared.Infrastructure;

public class EnumExtensionTest
{
    [Fact]
    public void Should_Return_String_Value_From_An_Enum()
    {
  
        var description = testEnum.TestEnum.GetDescription();

        description.Should().Be("TestEnumDesc");

    }
}

public enum testEnum
{
    [Description("TestEnumDesc")]
    TestEnum
}