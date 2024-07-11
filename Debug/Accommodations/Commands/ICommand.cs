namespace Accommodations.Commands;

public interface ICommand
{
    void Execute();
    void Undo();
}
