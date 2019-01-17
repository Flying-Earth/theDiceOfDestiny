using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {

    public static CardManager instance;
    //Use this to store all the cards
    private List<int> cardSequence=new List<int>();
    private List<int> seedSequence=new List<int>();
    private int currentIndex = 0;
    private int[] begin = { 0, 21, 22 };
    private int[] transitionA = { 6, 23, 24 };
    private int[] transitionB = { 25, 26 };
    private int[] endA = { 27, 28, 29 };
    private int[] endB = { 30, 31, 32 };
    private int[] endC = { 33, 34, 35 };
	void Awake () {
        instance = this;
        CreateRandomSequence(); 
	}
	
	public int currentCard()
    {

        return cardSequence[currentIndex++];

    }
    //转折牌A选B时应调用此函数
    public void ChangeSequence()
    {
        int tempIndex = cardSequence.IndexOf(14);
        cardSequence.RemoveAt(tempIndex);
        cardSequence.InsertRange(tempIndex,transitionB );
    }
    //true为选A
    public void chooseEnding(bool A, bool B)
    {
        if (A)
            cardSequence.AddRange(endA);
        else if (B)
            cardSequence.AddRange(endB);
        else
            cardSequence.AddRange(endC);
    }
    private void CreateRandomSequence()
    {
        for (int i = 1; i < 20; i++)
        {
            if(i!=6&&i!=14)
                seedSequence.Add(i);
        }
        

        while (seedSequence.Count > 0)
        {
            int randomIndex = Random.Range(0, seedSequence.Count);
            cardSequence.Add(seedSequence[randomIndex]);
            seedSequence.RemoveAt(randomIndex);
        }
        cardSequence.InsertRange(Random.Range(5, 9), transitionA);
        cardSequence.Insert(Random.Range(12, 16), 14);
        cardSequence.InsertRange(0, begin);
        cardSequence.Add(20);
    }
    public void LoadGameSequence(List<int> sequence)
    {
        cardSequence = sequence;
    }

    public void LoadGameCardIndex(int index)
    {
        currentIndex = index;
    }

    public List<int> GetCardSequence()
    {
        return instance.cardSequence;
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }
}
