


namespace ChessOut.MapSystem.Elements
{
    //Any empty outer touching an empty inner will turn into a world border to stop characters from leaving the map
    public class WorldBorder : EmptyElement,IObstacle
    {
        public WorldBorder() : base(false) { }


    }
}
