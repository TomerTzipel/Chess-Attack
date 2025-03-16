

namespace Slay_The_Basilisk_MonoGame
{
    public class EmptyElement : MapElement
    {
        private static EmptyElement _outerInstance;
        private static EmptyElement _innerInstance;

        public bool IsInner { get; private set; }

        private static bool _areInstancesInit = false;
       
        protected EmptyElement(bool isInner)
        {
            IsInner = isInner;
        }

        public static EmptyElement InnerInstance
        {
            get
            {
                if (!_areInstancesInit)
                {
                    InitializeEmptyElements();
                }

                return _innerInstance;
            }
        }
        public static EmptyElement OuterInstance
        {
            get
            {
                if (!_areInstancesInit)
                {
                    InitializeEmptyElements();
                }

                return _outerInstance;
            }
        }

        private static void InitializeEmptyElements()
        {
            if (!_areInstancesInit)
            {
                _innerInstance = new EmptyElement(true);
                _outerInstance = new EmptyElement(false);
                _areInstancesInit = true;
            }
        }
    }
}
