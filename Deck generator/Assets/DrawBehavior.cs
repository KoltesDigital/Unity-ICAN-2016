using UnityEngine;

public class DrawBehavior : MonoBehaviour
{
    public int CardCount = 10;

    private DeckGenerator<int> generator = new DeckGenerator<int>();
    
    void Start()
    {
        // Create the cards once
        for (int i = 0; i < CardCount; ++i)
            generator.Add(i);
    }

	void Update()
    {
	    if (Input.anyKeyDown)
        {
            int card = generator.Draw();
            Debug.Log(card);
        }
	}
}
