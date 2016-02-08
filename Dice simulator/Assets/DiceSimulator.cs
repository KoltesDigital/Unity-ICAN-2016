using UnityEngine;
using System.Collections;

public class DiceSimulator : MonoBehaviour
{
    public int SideCount = 6; // dices go from 1 to SideCount
    public int DiceCount = 2; // in a single throw
    public int ThrowCount = 10000;

    // Throws DiceCount dices, and records the results over ThrowCount throws
    private void ThrowDices()
    {
        // minimum sum is DiceCount (when each dice is 1)
        // maximum sum is SideCount * DiceCount (when each dice is SideCount)
        int maxSum = SideCount * DiceCount;

        int[] resultCounts = new int[maxSum + 1]; // "+ 1" because maxSum is the highest possible value

        for (int throwIndex = 0; throwIndex < ThrowCount; ++throwIndex)
        {
            int sum = 0; // result for this throw

            for (int diceIndex = 0; diceIndex < DiceCount; ++diceIndex)
            {
                int dice = Mathf.FloorToInt(Random.value * SideCount) + 1;
                // or:
                // int dice = Random.Range(0, SideCount) + 1;

                sum = sum + dice;
                // or:
                // sum += dice;
            }

            resultCounts[sum] = resultCounts[sum] + 1;
            // or:
            // results[sum] += 1;
            // ++results[sum];
        }

        Debug.LogFormat("Results for {0} throws with {1} dices, {2} sides:", ThrowCount, DiceCount, SideCount);
        for (int resultIndex = 0; resultIndex <= maxSum; ++resultIndex) // note the "<= maxSum", again because maxSum is the latest possible value
        {
            int sum = resultCounts[resultIndex];
            Debug.LogFormat("{0}: {1}", resultIndex, sum);
        }
    }

	void Update ()
    {
        if (Input.anyKeyDown)
            ThrowDices();
	}
}
