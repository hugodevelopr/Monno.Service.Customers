namespace Monno.Core.Commands;

public interface ICommand
{
}

public interface ICommand<TResult> : ICommand
{
}