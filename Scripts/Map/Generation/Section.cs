

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slay_The_Basilisk_MonoGame
{
    public enum SectionType
    {
        //Game Sections
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
        private void GenerateEnemySectionLayout()
        {
            Stack<EnemyType> enemies = null;
            switch (RunManager.CurrentDifficulity)
            {
                case Difficulity.Easy:
                    enemies = GenerateEasyEnemySectionLayout();
                    break;
                case Difficulity.Normal:
                    enemies = GenerateNormalEnemySectionLayout();
                    break;
                case Difficulity.Hard:
                    enemies = GenerateHardwEnemySectionLayout();
                    break;
            }

            while(enemies.Count > 0)
            {
                EnemyType enemyType = enemies.Pop();
                bool wasPlaced = false;
                while (!wasPlaced)
                {
                    int x = MathUtil.RandomIndex(Size);
                    int y = MathUtil.RandomIndex(Size);

                    if (SectionLayout[y,x] == EmptyElement.InnerInstance)
                    {
                        switch (enemyType)
                        {
                            case EnemyType.Bishop:
                                SectionLayout[y, x] = new Bishop(MapPosition + new Point(x,y));
                                break;
                            case EnemyType.Rook:
                                SectionLayout[y, x] = new Rook(MapPosition + new Point(x, y));
                                break;
                            case EnemyType.Queen:
                                SectionLayout[y, x] = new Queen(MapPosition + new Point(x, y));
                                break;
                        }

                        wasPlaced = true;
                    }
                }
            }
        }
        private Stack<EnemyType> GenerateEasyEnemySectionLayout()
        {
            Stack <EnemyType> enemies = new Stack<EnemyType>(4);    
            int chosenLayout = MathUtil.RandomRange(0, 1);
            if(chosenLayout == 0)
            {
                enemies.Push(EnemyType.Bishop);
                enemies.Push(EnemyType.Bishop);
            }
            else
            {
                enemies.Push(EnemyType.Rook);
            }

            return enemies;

        }
        private Stack<EnemyType> GenerateNormalEnemySectionLayout()
        {
            Stack<EnemyType> enemies = new Stack<EnemyType>(4);
            int chosenLayout = MathUtil.RandomRange(0, 2);
            if (chosenLayout == 0)
            {
                enemies.Push(EnemyType.Bishop);
                enemies.Push(EnemyType.Bishop);
                enemies.Push(EnemyType.Bishop);
            }
            else if (chosenLayout == 1)
            {
                enemies.Push(EnemyType.Rook);
                enemies.Push(EnemyType.Bishop);
            }
            else
            {
                enemies.Push(EnemyType.Queen);
            }
            return enemies;
        }
        private Stack<EnemyType> GenerateHardwEnemySectionLayout()
        {
            Stack<EnemyType> enemies = new Stack<EnemyType>(4);
            int chosenLayout = MathUtil.RandomRange(0, 2);
            if (chosenLayout == 0)
            {
                enemies.Push(EnemyType.Bishop);
                enemies.Push(EnemyType.Bishop);
                enemies.Push(EnemyType.Rook);
            }
            else if (chosenLayout == 1)
            {
                enemies.Push(EnemyType.Rook);
                enemies.Push(EnemyType.Rook);
            }
            else
            {
                enemies.Push(EnemyType.Queen);
                enemies.Push(EnemyType.Bishop);
            }

            return enemies;
            
        }
    }
}
