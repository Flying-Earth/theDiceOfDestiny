using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCard : MonoBehaviour {

    // Use this for initialization
    public GameObject GameStoryDlg;
    public GameObject card;
    private int cardNum;
    IEnumerator SelectACard()
    {
        GameManager.instance.CanSelectCard = false;
        AnimationManager.instance.CardHighlight(false);
        cardNum = CardManager.instance.currentCard();
        card.GetComponent<Card>().changeMat(cardNum);
        Instantiate(card);
        GameManager.instance.currentNum=cardNum;
        yield return new WaitForSeconds(2.5f);
        StoryManager.instance.changeStory(cardNum);
        GameStoryDlg.SetActive(true);
        
    }
    private void OnMouseDown()
    {
        if (GameManager.instance.CanSelectCard)
        {
            StartCoroutine(SelectACard());
        }
        
    }
}
