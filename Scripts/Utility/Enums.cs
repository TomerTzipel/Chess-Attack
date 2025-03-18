

namespace ChessOut.Utility
{
    public enum Asset
    {
        Player, PlayerCD,
        Rook, RookCD, Bishop, BishopCD, Queen, QueenCD,
        Chest, Vase,
        Button, Panel, HpBar, HpBarFill,
        KeyIcon, PotionIcon, DamageIcon, AttackSpeedIcon, SpeedIcon
    }
    public enum Direction
    {
        Up, Down, Left, Right, NW, NE, SE, SW
    }
    public enum SceneType
    {
        MainMenu, Pause, Game, GameOver
    }
    public enum Difficulity
    {
        Easy, Normal, Hard
    }
    public enum EnemyType
    {
        Bishop, Rook, Queen
    }
    public enum LootOrigin
    {
        Chest, Vase, Rook, Bishop, Queen, Level
    }
    public enum ItemType
    {
        Key, Potion, DamageToken, SpeedToken, AttackSpeedToken, MaxHealthToken
    }
    
    public enum TileVariant
    {
        Light, Dark
    }
    public enum SectionType
    {
        //Game Sections
        Enemy, Chest, Exit,

        //Generation Sections
        PsuedoStart, Discontinue, End, Inner, Outer, Start
    }
    public enum MouseButton
    {
        Right, Left, Middle
    }
    public enum HealthChangeMode
    {
        Current, Max
    }

}
