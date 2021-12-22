namespace Year2021
{
    internal class Util
    {
        internal static (int, int)[] GetGridPointsToCompareTo(int i, int j, int heightOuterBound, int widthOuterBound, bool includeDiagonal = false)
        {
            if (i == 0 && j == 0)
            {
                //Top left corner
                if (includeDiagonal)
                {
                    return new (int, int)[]
                    {
                        (i + 1, j),
                        (i + 1, j + 1),
                        (i, j + 1)
                    };
                }
                return new (int, int)[] { (i + 1, j), (i, j + 1) };
            }
            else if (i == heightOuterBound && j == widthOuterBound)
            {
                //Bottom right corner
                if (includeDiagonal)
                {
                    return new (int, int)[]
                    {
                        (i - 1, j),
                        (i - 1, j - 1),
                        (i, j - 1)
                    };
                }
                return new (int, int)[] { (i - 1, j), (i, j - 1) };
            }
            else if (i == 0 && j == widthOuterBound)
            {
                //Top right corner
                if (includeDiagonal)
                {
                    return new (int, int)[]
                    {
                        (i + 1, j),
                        (i + 1, j - 1),
                        (i, j - 1)
                    };
                }
                return new (int, int)[] { (i + 1, j), (i, j - 1) };
            }
            else if (i == heightOuterBound && j == 0)
            {
                //Bottom left corner
                if (includeDiagonal)
                {
                    return new (int, int)[]
                    {
                        (i - 1, j),
                        (i - 1, j + 1),
                        (i, j + 1)
                    };
                }
                return new (int, int)[] { (i - 1, j), (i, j + 1) };
            }
            else if (i == 0)
            {
                //Top row
                if (includeDiagonal)
                {
                    return new (int, int)[]
                    {
                        (i, j - 1),
                        (i + 1, j -1),
                        (i + 1, j),
                        (i + 1, j + 1),
                        (i, j + 1)
                    };
                }
                return new (int, int)[] { (i + 1, j), (i, j - 1), (i, j + 1) };
            }
            else if (i == heightOuterBound)
            {
                //Bottom row
                if (includeDiagonal)
                {
                    return new (int, int)[]
                    {
                        (i, j - 1),
                        (i - 1, j - 1),
                        (i - 1, j),
                        (i - 1, j + 1),
                        (i, j + 1)
                    };
                }
                return new (int, int)[] { (i - 1, j), (i, j - 1), (i, j + 1) };
            }
            else if (j == 0)
            {
                //Left side
                if (includeDiagonal)
                {
                    return new (int, int)[]
                    {
                        (i - 1, j),
                        (i - 1, j + 1),
                        (i, j + 1),
                        (i + 1, j + 1),
                        (i + 1, j)
                    };
                }
                return new (int, int)[] { (i - 1, j), (i + 1, j), (i, j + 1) };
            }
            else if (j == widthOuterBound)
            {
                //Right side
                if (includeDiagonal)
                {
                    return new (int, int)[]
                    {
                        (i - 1, j),
                        (i - 1, j - 1),
                        (i, j - 1),
                        (i + 1, j - 1),
                        (i + 1, j)
                    };
                }
                return new (int, int)[] { (i - 1, j), (i + 1, j), (i, j - 1) };
            }
            else
            {
                if (includeDiagonal)
                {
                    return new (int, int)[]
                    {
                        (i - 1, j - 1),
                        (i - 1, j),
                        (i - 1, j + 1),
                        (i, j - 1),
                        (i, j + 1),
                        (i + 1, j - 1),
                        (i + 1, j),
                        (i + 1, j + 1)
                    };
                }
                return new (int, int)[] { (i - 1, j), (i + 1, j), (i, j - 1), (i, j + 1) };
            }
        }
    }
}
