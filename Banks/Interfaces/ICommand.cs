namespace Banks.Interfaces
{
    public interface ICommand
    {
        string Name { get; }
        void Run();
    }
}