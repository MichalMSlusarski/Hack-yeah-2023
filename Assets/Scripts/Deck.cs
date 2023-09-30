using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using DG.Tweening;

public class Deck : MonoBehaviour
{
    [SerializeField] List<GameObject> allPossibleCards = new List<GameObject>();
    public List<GameObject> allCards = new List<GameObject>();
    List<GameObject> deck = new List<GameObject>();
    [SerializeField] int[] cardCount;
    public List<GameObject> activeCards = new List<GameObject>();
    public static TileCard activeCard; //there can only be one active card in the scene!
    [SerializeField] Vector3[] cardPositions;
    public static Vector3 postitionToFill;
    [SerializeField] GameObject slot;
    [SerializeField] SO_Integer tilesLeft;

    void Start()
    {
        postitionToFill = new Vector3(0, 0, 0);
        BuildDeck();
        tilesLeft.Integer = allCards.Count;
    }

    void BuildDeck()
    {
        for (int i = 0; i < cardCount.Length; i++)
        {
            allCards.AddRange(Enumerable.Repeat(allPossibleCards[i], cardCount[i]));
        } 

        allCards = Shuffle(allCards);

        for (int i = 0; i < allCards.Count; i++)
        {
            var newCard = Instantiate(allCards[i], cardPositions[0], Quaternion.identity) as GameObject;
            newCard.transform.SetParent(this.gameObject.transform, true);
            newCard.name = newCard.name + newCard.transform.position.ToString();
            deck.Add(newCard);
            // float time = 1f / ((float)i + 1.5f);
            // newCard.transform.DOMove(cardPositions[i], time);
        }

        for (int i = 0; i <= 2; i++)
        {
            float time = 0.5f / ((float)i + .5f);
            deck[i].transform.SetParent(slot.transform, true);
            deck[i].transform.DOMove(cardPositions[i + 1], time);

            activeCards.Add(deck[i]);
            deck.RemoveAt(i);

            if (i == 1)
            {
                activeCards[i].GetComponent<TileCard>().defaultTag = "Card";
                activeCards[i].transform.tag = "Card";
            }
        }
    }

    public void DrawCard()
    {
        postitionToFill = activeCard.transform.position;

        if (deck.Count > 0)
        {
            deck[0].transform.SetParent(slot.transform, true);
            deck[0].transform.DOMove(cardPositions[1], 0.15f);
            activeCards.Insert(0, deck[0]); // the first card from deck, becomes the first active card
            deck.RemoveAt(0);
            activeCards.Remove(activeCard.gameObject);
            activeCard.DropDead();
            
            activeCards[1].transform.tag = "Card";
            activeCards[1].transform.DOMove(cardPositions[2], 0.14f);

            if (postitionToFill == cardPositions[3])
            {
                Debug.Log("2");
                activeCards[2].transform.DOMove(cardPositions[3], 0.15f);
            }
        }
        else
        {
            activeCards.Remove(activeCard.gameObject);
            activeCard.DropDead();
            activeCards[0].transform.tag = "Card";
            activeCards[0].transform.DOMove(cardPositions[1], 0.15f);

            if (postitionToFill == cardPositions[3])
            {
                Debug.Log("2");
                activeCards[2].transform.DOMove(cardPositions[3], 0.15f);
            }

            Debug.Log("'REACHED'");
        }
    }

    List<GameObject> Shuffle(List<GameObject> cards)
    {
        List<GameObject> shuffledCards = cards.OrderBy(a => Guid.NewGuid()).ToList();
        return shuffledCards;
    }
}
