namespace Nyala.Domain
{
    public interface IRover
    {
        string GetCurrentCoordinates();
        void Command(string setOfCommands);
    }
}
