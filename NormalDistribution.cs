using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class is used to make some difference to common enemies' stats
 * Creatures of the same type (species) will have different power and dimensions
 */

/// <summary>
///  <para> Class for generating random numbers of normal distribution. </para>
///  </summary>
public class NormalDistribution
{
    /// <summary>
    ///  <para> Returns a random number from standard normal distribution. </para>
    ///  </summary>
    public static float GetNum()
    {
        float uniform1 = Random.Range(0f, 1f);
        float uniform2 = Random.Range(0f, 1f);

        return Mathf.Sqrt(-2 * Mathf.Log(uniform1)) * Mathf.Cos(2 * Mathf.PI * uniform2);
    }

    /// <summary>
    ///  <para> Returns a random number from normal distribution. </para>
    ///  </summary>
    public static float GetNum(float expected_value = 0, float variance_sqrt = 1)
    {
        return variance_sqrt * GetNum() + expected_value;
    }
}
