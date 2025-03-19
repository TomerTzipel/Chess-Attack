
namespace ChessOut.Utility
{
    public static class MathUtil
    {
        //A chance that is key to the procedural generator, Do not touch!
        public const int CONTINUE_CHANCE = 69;

        private static readonly Random numberGenerator = new Random();

        public static int RandomIndex(int length)
        {
            return numberGenerator.Next(0, length);
        }

        //Max Inclusive
        public static int RandomRange(int min, int max)
        {
            return numberGenerator.Next(min, max + 1);
        }

        public static bool RollChance(int chance)
        {
            int roll = RandomPrecent();

            if (chance >= roll)
            {
                return true;
            }

            return false;
        }

        //Will throw exception if the array doesn't sum to 100!
        public static int RandomIndexFromPercentArray(int[] chances)
        {
            int chanceAdder = 0;
            int chance;
            int result = RandomPrecent();

            for (int i = 0; i < chances.Length; i++)
            {
                chance = chances[i];

                if (chance + chanceAdder >= result)
                {
                    return i;
                }

                chanceAdder += chance;

            }

            throw new Exception();
        }

        //Return how much value is of max in percent
        public static int GetPercent(int value, int max)
        {
            return (int)(((float)value / max) * 100f);
        }

        //we do it from 1-100, so if the chance is zero it will never happen, as it can't be >= to these values
        private static int RandomPrecent()
        {
            return numberGenerator.Next(1, 101);
        }
    }
}
