namespace Nyala.Core
{
    public interface IRover
    {
        List<(int, int)> Obtacles { get; set; }
        string GetCurrentCoordinates();
        void Command(string setOfCommands);
    }
}
