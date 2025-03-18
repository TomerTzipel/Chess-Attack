
namespace ChessOut.Utility
{
    
    //An int based Vector2, used for the map matrices
    public class Point
    {
        private Vector2 _vector;

        public int X 
        { 
            get 
            { 
                return (int)_vector.X; 
            } 
            set 
            { 
                _vector.X = value;
            } 
        }
        public int Y
        {
            get
            {
                return (int)_vector.Y;
            }
            set
            {
                _vector.Y = value;
            }
        }

        public Point()
        {
            _vector = new Vector2(0,0);
        }

        public Point(int x, int y)
        {
            _vector = new Vector2(x, y);
        }

        public Point(Point point)
        {
            _vector = new Vector2(point.X, point.Y);
        }

        public Point MovePointInDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    Y -= 1;
                    break;
                case Direction.Down:
                    Y += 1;
                    break;
                case Direction.Left:
                    X -= 1;
                    break;
                case Direction.Right:
                    X += 1;
                    break;
                case Direction.NW:
                    Y -= 1;
                    X -= 1;
                    break;
                case Direction.NE:
                    Y -= 1;
                    X += 1;
                    break;
                case Direction.SE:
                    Y += 1;
                    X += 1;
                    break;
                case Direction.SW:
                    Y += 1;
                    X -= 1;
                    break;
            }

            return this;
        }
       
        public static float Distance(Point pointA,Point pointB)
        {
            return Vector2.Distance(pointA._vector, pointB._vector);
        }

        public static bool Equals(Point p1, Point p2)
        {
            return p1._vector == p2._vector;
        }
        public static Point operator +(Point left, Point right)
        {
            return new Point(left.X + right.X, left.Y + right.Y);
        }
        public static Point operator -(Point left, Point right)
        {
            return new Point(left.X - right.X, left.Y - right.Y);
        }
        public static Point operator *(Point left, int right)
        {
            return new Point(left.X * right, left.Y * right);
        }
        public static Vector2 operator +(Point left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        public override string ToString()
        {
            return $"X:{X},Y:{Y}";
        }
    }
}
