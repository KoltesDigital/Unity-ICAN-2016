using UnityEngine;
using System.Collections.Generic;

public class DeckGenerator<Type>
{
    private List<Type> deck = new List<Type>();
    private List<Type> discard = new List<Type>();
    
    public void Add(Type card)
    {
        deck.Add(card);
    }

    public Type Draw()
    {
        if (deck.Count == 0)
        {
            deck = discard;
            discard = new List<Type>();

            // or:
            // deck.InsertRange(0, discard);
            // discard.Clear();
        }

        int n = Random.Range(0, deck.Count);
        Type card = deck[n];
        deck.RemoveAt(n);
        // or: shuffle the deck once and always take the first card

        discard.Add(card);

        return card;
    }
}
