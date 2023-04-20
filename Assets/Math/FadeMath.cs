namespace Fade.Math
{
    public static class FadeMath
    {
        public static float Sum(float v1, float v2)
        {
            float temp = v1 + v2;
            return temp;
        }
        public static int Sum(int v1, int v2)
        {
            int temp = v1 + v2;
            return temp;
        }
        public static float Multiplication(float v1, float v2)
        {
            float temp = v1 * v2;
            return temp;
        }
        public static int Multiplication(int v1, int v2)
        {
            int temp = v1 * v2;
            return temp;
        }
        public static float Division(float v1, float v2)
        {
            float temp = v1 / v2;
            return temp;
        }
        public static int Division(int v1, int v2)
        {
            int temp = v1 / v2;
            return temp;
        }
        public static float Extraction(float v1, float v2, bool isAbsoluteValue)
        {
            float temp = v1 - v2;
            if (isAbsoluteValue) temp = Absolute(temp);
            return temp;
        }
        public static int Extraction(int v1, int v2, bool isAbsoluteValue)
        {
            int temp = v1 - v2;
            if (isAbsoluteValue) temp = Absolute(temp);

            return temp;
        }
        public static float Absolute(float v1)
        {
            float temp = v1 >= 0 ? v1 : v1 * -1;
            return temp;
        }
        public static int Absolute(int v1)
        {
            int temp = v1 >= 0 ? v1 : v1 * -1;
            return temp;
        }
        public static float Min(float v1, float v2)
        {
            float min = v1 > v2 ? v1 : v1 < v2 ? v2 : 0;
            return min;
        }
        public static int Min(int v1, int v2)
        {
            int min = v1 > v2 ? v1 : v1 < v2 ? v2 : 0;
            return min;
        }
        public static float Max(float v1, float v2)
        {
            float max = v1 > v2 ? v1 : v1 < v2 ? v2 : 0;
            return max;
        }
        public static int Max(int v1, int v2)
        {
            int max = v1 > v2 ? v1 : v1 < v2 ? v2 : 0;
            return max;
        }
        public static float SquareRoot(float v1)
        {
            float x = v1;
            float y = 1;
            float e = 0.0001f;

            while (x - y > e)
            {
                x = (x + y) / 2;
                y = v1 / x;
            }
            return x;

        }
        public static int SquareRoot(int v1)
        {
            int x = v1;
            int y = 1;
            float e = 0.0001f;

            while (x - y > e)
            {
                x = (x + y) / 2;
                y = v1 / x;
            }
            return x;
        }

        public static float Power(float v1, float v2)
        {
            float temp = v1;
            float x = 0;

            while (x <= v2)
            {
                temp *= v2;
            }

            return temp;
        }
        public static int Power(int v1, int v2)
        {
            int temp = 1;
            int x = 0;

            while (x < v2)
            {
                temp *= v1;
                x++;
            }

            return temp;
        }

        public static float Ceil(float v1)
        {
            int result = (int)v1;
            return v1 > result ? result + 1 : result;

        }
        public static float Floor(float v1)
        {
            int result = (int)v1;
            return v1 > result ? result : result - 1;
        }
        public static float ArithmeticAverage(float[] v)
        {
            float sum = 0;
            int iteratorCount = 0;

            while (iteratorCount < v.Length)
            {
                sum += v[iteratorCount];
                iteratorCount++;
            }
            float average = sum / v.Length;
            return average;

        }
        public static int ArithmeticAverage(int[] v)
        {
            int sum = 0;
            int iteratorCount = 0;

            while (iteratorCount < v.Length)
            {
                sum += v[iteratorCount];
                iteratorCount++;
            }
            int average = sum / v.Length;
            return average;

        }

        public static int Factorial(int v)
        {
            int result = 1;
            for (int i = 1; i <= v; i++) result *= i;
            return result;
        }
    }
}