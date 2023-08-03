using UnityEngine;

/// <summary>
/// Static class for utility maths functions
/// </summary>
public static class MathsFunctions
{
    /// <summary>
    /// Remap value base on the to Range
    /// </summary>
    /// <param name="value">value to remap</param>
    /// <param name="from">the range of value</param>
    /// <param name="to">to be remaped</param>
    /// <returns>ramapped value</returns>
    public static float Remap(float value, Vector2 from, Vector2 to)
    {
        return (((value - from.x) * (to.y - to.x)) / (from.y - from.x)) + to.x;
    }
}
