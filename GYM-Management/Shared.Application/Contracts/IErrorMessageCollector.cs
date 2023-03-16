namespace Shared.Application.Contracts;

public interface IErrorMessageCollector
{
    void AddError(string errorMessage);
}