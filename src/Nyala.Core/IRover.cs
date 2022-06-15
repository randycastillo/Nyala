namespace Nyala.Core
{
    public interface IRover
    {
        List<(int, int)> Obtacles { get; set; }
        string GetCurrentLocation();
        void Command(string setOfCommands);
    }
}
