namespace Shared.Core;

public interface IBusinessRule
{
    bool IsBroken();

    string Message { get; }
}