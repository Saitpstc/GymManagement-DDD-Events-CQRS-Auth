namespace Shared.Core.Contracts;

public interface IBusinessRule
{

    string Message { get; }

    bool IsBroken();
}