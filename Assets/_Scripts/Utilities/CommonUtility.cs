using Unity.Mathematics;


public static class CommonUtility
{
    public static bool NearlyEqual(float a, float b, float epsilon = 0.001f)
    {
        return math.abs(a - b) < epsilon;
    }
}
