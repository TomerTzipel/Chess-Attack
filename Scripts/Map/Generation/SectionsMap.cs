
using System.Linq;


namespace ChessOut.MapSystem
{
    //The procedural generation class, results in a map of sections
    public class SectionsMap
    {
        protected readonly int _size;
        private readonly int _numberOfInnerSections;

        public Section[,] Sections { get; protected set; }

        public Point StartSectionPosition { get; protected set; }

        //Results in a section map sizeXsize with:
        //numberOfInnerSectionsToGenerate inner sections
        //one start section
        //one exit section
        //everything else is outer section  
        public SectionsMap(int size, int numberOfInnerSectionsToGenerate)
        {
            _size = size;
            _numberOfInnerSections = numberOfInnerSectionsToGenerate;

            int numberOfInnerSectionsGenerated;

            do
            {
                Sections = new Section[_size, _size];
                GenerateStartSection();
                numberOfInnerSectionsGenerated = 1;
                GenerateAdjacentSections(StartSectionPosition, ref numberOfInnerSectionsGenerated);
            } while (numberOfInnerSectionsGenerated > numberOfInnerSectionsToGenerate);

            CleanUp();
        }

        //Allocates to inner sections the type
        public void AllocateSectionsType(int numberOfSectionsToAllocate, SectionType type)
        {
            int count = 0;

            while(count < numberOfSectionsToAllocate)
            {
                foreach (var section in Sections)
                {
                    if (section.Type != SectionType.Inner) continue;

                    int chance = MathUtil.GetPercent(numberOfSectionsToAllocate, _numberOfInnerSections - 2);

                    if (MathUtil.RollChance(chance))
                    {
                        section.Type = type;
                        count++;
                        if (count == numberOfSectionsToAllocate) break;
                    }
                } 
            }
        }
        public void GenerateSectionsLayout()
        {
            foreach (var section in Sections)
            {
                section.GenerareLayout();
            }
        }
        private void GenerateStartSection()
        {
            int x = MathUtil.RandomIndex(_size);
            int y = MathUtil.RandomIndex(_size);
            StartSectionPosition = new Point(x, y);
            GenerateSectionAt(StartSectionPosition, SectionType.Start);
        }

        //Makes Sure every null turns into an outer section, and every generation type becomes an inner section
        private void CleanUp()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    Section section = Sections[i, j];
                    if (section == null)
                    {
                        GenerateSectionAt(j, i, SectionType.Outer);
                    }
                    else if(section.Type != SectionType.Start && section.Type != SectionType.Exit)
                    {
                        section.Type = SectionType.Inner;
                    }
                }
            }
        }

        //Read the algorithm explanation in the documantation
        private void GenerateAdjacentSections(Point sectionPosition, ref int numberOfSectionsGenerated)
        {
            //As long as we still have sections to generate and we aren't a discontinue section we contine to generate sections from the current section
            while (SectionAt(sectionPosition).Type != SectionType.Discontinue && numberOfSectionsGenerated < _numberOfInnerSections)
            {
                //Looks for a direction to generate a new section
                Direction chosenDirection;
                bool wasDirectionFound = ChooseDirection(sectionPosition, out chosenDirection);

                if (!wasDirectionFound)
                {
                    //There are no legal direction left
                    SectionType sectionType = SectionAt(sectionPosition).Type;

                    //If we aren't a start or psuedo we are marked discontinue
                    if (sectionType != SectionType.Start && sectionType != SectionType.PsuedoStart)
                    {
                        SectionAt(sectionPosition).Type = SectionType.Discontinue;
                        return;
                    }

                    //We need to attempt to break free
                    Point psuedoStartSectionPosition;

                    if (!TryBreakFree(sectionPosition, out psuedoStartSectionPosition))
                    {
                        //We failed to break free so we abort this generation attempt
                        numberOfSectionsGenerated = _numberOfInnerSections + 1;
                    }
                    else
                    {
                        //We Broke free and continue generation
                        GenerateAdjacentSections(psuedoStartSectionPosition, ref numberOfSectionsGenerated);
                    }

                    return;
                }

                //We found a direction to generate a new section
                Point newSectionPosition = new Point(sectionPosition.X, sectionPosition.Y);
                newSectionPosition.MovePointInDirection(chosenDirection);

                GenerateSectionAt(newSectionPosition);
                numberOfSectionsGenerated++;

                //Check if it was the last section
                if (numberOfSectionsGenerated >= _numberOfInnerSections)
                {
                    SectionAt(newSectionPosition).Type = SectionType.Exit;
                    return;
                }

                //Decide if we generate more sections from the generated section
                DecideIfEndSection(newSectionPosition);
                if (SectionAt(newSectionPosition).Type != SectionType.End)
                {
                    GenerateAdjacentSections(newSectionPosition, ref numberOfSectionsGenerated);
                }

                //Decide if we choose to stop generating sections from the current section
                if (SectionAt(sectionPosition).Type != SectionType.Start && SectionAt(sectionPosition).Type != SectionType.PsuedoStart)
                {
                    DecideIfDiscontinuedSection(sectionPosition);
                }
            }
        }

        //Tries to break free from the situation where start/psuedoStart is circled by end or discontinue sections
        private bool TryBreakFree(Point sectionPosition, out Point nextPosition)
        {
            List<Direction> directions = new List<Direction>
            {
                Direction.Right,
                Direction.Up,
                Direction.Down,
                Direction.Left
            };

            int chosenDirectionIndex;
            Direction chosenDirection;

            while (directions.Count > 0)
            {
                chosenDirectionIndex = MathUtil.RandomIndex(directions.Count);
                chosenDirection = directions[chosenDirectionIndex];

                Point directedSectionPosition = new Point(sectionPosition);
                directedSectionPosition.MovePointInDirection(chosenDirection);

                if (directedSectionPosition.X >= _size || directedSectionPosition.Y >= _size)
                {
                    directions.RemoveAt(chosenDirectionIndex);
                    continue;
                }

                if (directedSectionPosition.X < 0 || directedSectionPosition.Y < 0)
                {
                    directions.RemoveAt(chosenDirectionIndex);
                    continue;
                }

                SectionType directedSectionType = SectionAt(directedSectionPosition).Type;
                if (directedSectionType != SectionType.Start && directedSectionType != SectionType.PsuedoStart)
                {
                    SectionAt(directedSectionPosition).Type = SectionType.PsuedoStart;
                    nextPosition = directedSectionPosition;
                    return true;
                }

                directions.RemoveAt(chosenDirectionIndex);
            }

            if (SectionAt(sectionPosition).Type == SectionType.Start)
            {
                nextPosition = null;
                return false;
            }

            nextPosition = StartSectionPosition;
            return true;
        }

        private void DecideIfEndSection(Point sectionPosition)
        {
            if (!DecideIfToContinueFromSection())
            {
                SectionAt(sectionPosition).Type = SectionType.End;
            }
        }

        private void DecideIfDiscontinuedSection(Point sectionPosition)
        {
            if (!DecideIfToContinueFromSection())
            {
                SectionAt(sectionPosition).Type = SectionType.Discontinue;
            }
        }

        private bool DecideIfToContinueFromSection()
        {
            return MathUtil.RollChance(MathUtil.CONTINUE_CHANCE);
        }

        //Looks for a legal direction from the section position
        private bool ChooseDirection(Point sectionPosition,out Direction direction)
        {
            List<Direction> directions = new List<Direction>
            {
                Direction.Right,
                Direction.Up,
                Direction.Down,
                Direction.Left
            };

            int chosenDirectionIndex;
            Direction chosenDirection;
            bool isDirectionAvailable;

            while (directions.Count > 0)
            {
                chosenDirectionIndex = MathUtil.RandomIndex(directions.Count);
                chosenDirection = directions.ElementAt(chosenDirectionIndex);
                isDirectionAvailable = IsDirectionAvailable(sectionPosition, chosenDirection);
                if (isDirectionAvailable)
                {
                    direction = chosenDirection;
                    return true;
                }
                directions.RemoveAt(chosenDirectionIndex);
            }

            direction = Direction.Up;
            return false;

        }

        private bool IsDirectionAvailable(Point sectionPosition, Direction direction)
        {
            Point directedSectionPosition = new Point(sectionPosition.X, sectionPosition.Y);
            directedSectionPosition.MovePointInDirection(direction);

            return IsSectionAvailable(directedSectionPosition);
        }

        private bool IsSectionAvailable(Point sectionPosition)
        {
            if (sectionPosition.X >= _size || sectionPosition.Y >= _size)
            {
                return false;
            }

            if (sectionPosition.X < 0 || sectionPosition.Y < 0)
            {
                return false;
            }

            if (SectionAt(sectionPosition) != null)
            {
                return false;
            }

            return true;
        }

        private Section SectionAt(Point position)
        {
            return Sections[position.Y, position.X];
        }

        //Section Generation Methods - For better reading of the algorithm itself
        private void GenerateSectionAt(int x,int y, SectionType type)
        {
            Sections[y, x] = new Section(x, y, type);
        }

        private void GenerateSectionAt(Point position)
        {
            Sections[position.Y, position.X] = new Section(position.X, position.Y);
        }

        private void GenerateSectionAt(Point position, SectionType type)
        {
            Sections[position.Y, position.X] = new Section(position.X, position.Y, type);
        }
    }
}
