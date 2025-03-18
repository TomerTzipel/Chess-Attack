

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessOut.MapSystem
{
    
    internal class Section
    {
        private int Size { get {  return GameData.SectionSize; } }

        public Point MapPosition { get; private set; } //top left (0,0) of the section

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
            
            //Filling the layout with either inner elements if the section is walkable and outer if it isn't
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
            if (MathUtil.RollChance(5))
            {
                int position = MathUtil.RandomIndex(Size * Size);
                int x = position % Size;
                int y = position / Size;
                SectionLayout[y, x] = new Vase(MapPosition + new Point(x, y));
            }
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

        //According to the difficulity of the level (based on run completion) a stack of predetermined enemy options
        //will be chosen randomly (equall odds) and placed on the layout
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
                List<int> positions = new List<int>(Size * Size);

                for (int i = 0; i < Size * Size; i++)
                {
                    positions.Add(i);
                }

                while (!wasPlaced && positions.Count>0)
                {
                    int position = positions[MathUtil.RandomIndex(positions.Count)];
                    positions.Remove(position);
                    int x = position % Size;
                    int y = position / Size;

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
