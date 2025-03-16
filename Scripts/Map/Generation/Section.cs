

namespace Slay_The_Basilisk_MonoGame
{
    public enum SectionType
    {
        //Game Sections  (Must be 0-3)
        Enemy, Chest, Exit,

        //Generation Sections
        PsuedoStart, Discontinue, End, Inner, Outer, Start
    }
    internal class Section
    {
        private int Size { get {  return GameData.SectionSize; } }

        public Point MapPosition { get; private set; } //top left

        public Point SectionMapPosition { get; private set; }

        public MapElement[,] SectionLayout { get; private set; }
        public SectionType Type { get; set; }
        
        public Section(int x, int y) : this(x,y, SectionType.Inner) { }
        
        public Section(int x, int y, SectionType type)
        {
            Type = type;

            SectionMapPosition = new Point(x, y);
            MapPosition = new Point(x, y) * Size;
            SectionLayout = new MapElement[Size, Size];

            EmptyElement elementToFill = Type == SectionType.Outer ? EmptyElement.OuterInstance : EmptyElement.InnerInstance;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    SectionLayout[i, j] = elementToFill;
                }
            }
        }

        public void GenerareLayout()
        {
            switch (Type)
            {
                case SectionType.Enemy:
                    GenerateEnemySectionLayout();
                    break;
                case SectionType.Chest:
                    GenerateChestSectionLayout();
                    break;
                case SectionType.Exit:
                    GenerateExitSectionLayout();
                    break;
                case SectionType.Inner:
                    GenerateInnerSectionLayout();
                    break;
                case SectionType.Start:
                    GenerateStartSectionLayout();
                    break;
                default:
                    break;
            }

        }

        private void GenerateInnerSectionLayout()
        {
            
        }
        private void GenerateChestSectionLayout()
        {
            Point mapPosition = MapPosition + new Point(Size / 2, Size / 2);
            SectionLayout[Size / 2, Size / 2] = new Chest(mapPosition);
        }
        private void GenerateEnemySectionLayout()
        {
            Point mapPosition = MapPosition + new Point(Size / 2, Size / 2);
            SectionLayout[Size / 2, Size / 2] = new Rook(mapPosition);
        }
        private void GenerateExitSectionLayout()
        {
            Point mapPosition = MapPosition + new Point(Size / 2, Size / 2);
            SectionLayout[Size / 2, Size / 2] = new Exit(mapPosition);
        }
        private void GenerateStartSectionLayout()
        {
            SectionLayout[Size / 2, Size / 2] = RunManager.Player;
            RunManager.Player.MapPosition = MapPosition + new Point(Size / 2, Size / 2);
        }
    }
}
