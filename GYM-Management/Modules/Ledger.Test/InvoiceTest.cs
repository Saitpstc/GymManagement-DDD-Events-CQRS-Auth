namespace Ledger.Test;

using FluentAssertions;

public class InvoiceTest
{
    [Fact]
    public void Throws_If_TotalAmount_Is_LowerThanZero()
    {

        Action action = () =>
        {
            TotalAmount totalAmount = new TotalAmount(-6);
        };

        action.Should().Throw<Exception>();
    }

    [Fact]
    public void NotThrows_If_TotalAmount_Is_GreaterThanZero()
    {

        Action action = () =>
        {
            TotalAmount totalAmount = new TotalAmount(6);
        };

        action.Should().NotThrow<Exception>();
    }

    [Theory]
    [InlineData(1.5)]
    [InlineData(56)]
    [InlineData(34.5673)]
    public void ReturnTotalAmount_If_Creation_Is_Sucessfull(double testValue)
    {
        TotalAmount totalAmount = new TotalAmount(testValue);


        totalAmount.amount.Should().Be(testValue);
    }

    [Fact]
    public void DueDate_Will_Be_Created_During_Object_Creation()
    {

        Invoice invoice = new Invoice(new TotalAmount(12));

        invoice.DueDate.Should().NotBe(null);
    }



    [Fact]
    public void Description_Cannot_Be_More_Than_255_Characters()
    {
        // Act
        Action act = () => new Description(new string('x', 256));

        // Assert
        act.Should().Throw<Exception>();
    }

    [Fact]
    public void Pass_If_Description_Lower_Than_255_Characters()
    {
        // Act
        Action act = () =>
        {
            var Description = new Description(new string('x', 254));
        };

        Description description = new Description(new string('x', 254));

        // Assert
        act.Should().NotThrow();
        description.Value.Should().Be(new string('x', 254));
    }

    [Fact]
    public void Set_Description_After_Invoice_Is_Created()
    {
        // Act
        var invoice = new Invoice(new TotalAmount(12));

        Description description = new Description(new string('x', 254));

        invoice.SetDescription(description);

        invoice.Description.Value.Should().Be(new string('x', 254));
    }

    [Fact]
    public void Invoice_Status_Cannot_Be_Paid_If_PayerUserId_Is_Null()
    {
        // Act
        var invoice = new Invoice(new TotalAmount(12));

        Action act = () => invoice.ChangeStatus(InvoiceStatus.Paid);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void Invoice_Status_Cannot_Be_Canceled_If_It_Is_Paid()
    {
        // Act
        var invoice = new Invoice(new TotalAmount(12));

        Action act = () => invoice.ChangeStatus(InvoiceStatus.Canceled);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void Invoice_Status_Should_Be_Changed_To_Different_Value()
    {
        // Act
        var invoice = new Invoice(new TotalAmount(12));

        Action act = () => invoice.ChangeStatus(InvoiceStatus.Canceled);

        act.Should().NotThrow<Exception>();
        invoice.Status.Should().Be(InvoiceStatus.Canceled);
    }

    [Fact]
    public void PostponeDueDate_ShouldPostponeDueDateByOneWeek()
    {
        // Arrange
        var invoice = new Invoice(new TotalAmount(12));
        var dueDate = invoice.DueDate;

        // Act
        invoice.PostponeDueDate();

        // Assert
        invoice.DueDate.Should().Be(dueDate.AddDays(7));
        invoice.Status.Should().Be(InvoiceStatus.Postponed);
    }


    /*[Fact]
    public void ProcessedDate_Cannot_Be_Higher_Than_DueDate()
    {
        // Arrange
        Action action = () => new Invoice(new TotalAmount(12), DateTime.Now);

        // Act
        Action act = () => invoice.ValidateProcessedDate(); // This method will throw an exception if the processed date is higher than due date

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }*/
}