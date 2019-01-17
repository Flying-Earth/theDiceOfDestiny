using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveGameManager : MonoBehaviour
{

    public static SaveGameManager instance = null;

    private bool isNet = false;
    private bool isLoad = false;
    private bool isSave = false;

    private Player player = new Player();
    private int cardNum = 0;

    private List<int> cardSequence = new List<int>();

    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsNet
    {
        get { return isNet; }
        set { isNet = value; }
    }

    public bool IsLoad
    {
        get { return isLoad; }
        set { isLoad = value; }
    }

    public bool IsSave
    {
        get { return isSave; }
        set { isSave = value; }
    }

    public Player GetLoadPlayerStatus()
    {
        return instance.player;
    }

    public int GetLoadCardStatus()
    {
        return instance.cardNum;
    }

    public List<int> GetLoadCardSequence()
    {
        return instance.cardSequence;
    }

    public void SetLoadCardSequence(Dictionary<byte, object> loadCardData)
    {
        object temp;
        for (int i = 1; i <= loadCardData.Count; i++)
        {
            loadCardData.TryGetValue((byte)i, out temp);
            cardSequence.Add((int)temp);
            //Debug.Log(temp.ToString());
        }
        CardManager.instance.LoadGameSequence(cardSequence);
    }

    public void SetLoadStatus(Dictionary<byte, object> loadData)
    {
        object SaveGameName; object UserName;
        object Health; object Attack; object Armor; object Charge; object MaxHealth; object FullCharge;
        object CardNum; object TransitionA; object TransitionB;
        loadData.TryGetValue(1, out SaveGameName);
        loadData.TryGetValue(2, out UserName);
        loadData.TryGetValue(3, out Health);
        loadData.TryGetValue(4, out Attack);
        loadData.TryGetValue(5, out Armor);
        loadData.TryGetValue(6, out Charge);
        loadData.TryGetValue(7, out MaxHealth);
        loadData.TryGetValue(8, out FullCharge);
        loadData.TryGetValue(9, out CardNum);
        loadData.TryGetValue(10, out TransitionA);
        loadData.TryGetValue(11, out TransitionB);

        //instance.saveGameName = SaveGameName.ToString();
        //instance.userName = UserName.ToString();
        instance.player.Health = (int)Health;
        instance.player.Attack = (int)Attack;
        instance.player.Armor = (int)Armor;
        instance.player.Charge = (int)Charge;
        instance.player.MaxHealth = (int)MaxHealth;
        instance.player.FullCharge = (int)FullCharge;
        instance.cardNum = (int)CardNum;

        CardManager.instance.LoadGameCardIndex(cardNum);
        Player.instance.SetPlayer(instance.player);
        GameManager.instance.LoadGame((bool)TransitionA, (bool)TransitionB);
        //Debug.LogError(instance.saveGameName + instance.userName + instance.player.Charge.ToString() + instance.cardNum);
    }

    public Dictionary<byte, object> SetSaveStatus(string saveGameName, string userName, Player player, int currentCardNum,
        bool transitionA, bool transitionB)
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add(1, saveGameName);
        data.Add(2, userName);
        data.Add(3, player.Health);
        data.Add(4, player.Attack);
        data.Add(5, player.Armor);
        data.Add(6, player.Charge);
        data.Add(7, player.MaxHealth);
        data.Add(8, player.FullCharge);
        data.Add(9, currentCardNum);
        data.Add(10, transitionA);
        data.Add(11, transitionB);
        //Debug.LogError(player.Health);
        return data;
    }

    public Dictionary<byte, object> SetSaveCardSequence(List<int> cardSequence, string saveGameName)
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add(1, saveGameName);
        for (int i = 1; i <= cardSequence.Count; i++)
        {
            data.Add((byte)(i + 1), cardSequence[i - 1]);
            //Debug.Log(cardSequence[i - 1]);
        }
        return data;
    }
}
