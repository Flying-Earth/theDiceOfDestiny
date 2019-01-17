using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject GameStoryDlg;
    public GameObject IllustrationDlg;
    public GameObject ThrowDiceBtn;
    public GameObject GameOverDlg;
    public GameObject JusticeSwordBtn;
    public GameObject MainMenu;
    public Text EndText;
    public Transform[] EnemySpawnPoint;
    public AudioClip FightClip;
    public AudioClip VictoryClip;
    public AudioClip GameOverClip;
    public AudioClip CongratulationClip;
    public AudioClip BattleVictoryClip;
    private AudioSource SFX;
    [Header("敌人种类")]
    public GameObject EnemyPrefab;
    [HideInInspector]
    public List<Enemy> enemies = new List<Enemy>();
    [HideInInspector]
    public int enemyCount = 0;
    public int currentNum;

    public delegate void GetCounter();
    public static event GetCounter getCounter;
    public delegate void AbandonCard();
    public static event AbandonCard abandonCard;

    private bool gameStart = false;
    private bool bSelectCard = false;
    private bool gameEnd = false;
    private bool PlayerTurn = true;
    private bool isChoosing = true;
    private bool isSelecting = true;
    private bool isReading = true;
    private bool isAbility = false;
    private bool hasChooseA;
    private bool transitionA = false;
    private bool transitionB = false;
    private bool bMenu = false;


    [HideInInspector]
    public int swordCount;
    [HideInInspector]
    public int shieldCount;
    [HideInInspector]
    public int chargeCount;

    public bool CanSelectCard
    {
        get
        {
            return bSelectCard;
        }
        set
        {
            bSelectCard = value;
        }
    }
    public bool IsSelecting
    {
        set
        {
            isSelecting = value;
        }
    }
    public bool IsChoosing
    {
        set
        {
            isChoosing = value;
        }
        get
        {
            return isChoosing;
        }
    }
    public bool IsAbility
    {
        get
        {
            return isAbility;
        }
        set
        {
            isAbility = value;
        }
    }
    public bool IsReading
    {
        set
        {
            isReading = value;
        }
    }
    public bool HasChooseA
    {
        set
        {
            hasChooseA = value;
        }
    }

	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;
        StartCoroutine(GameMainProcess());
        SFX = gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bMenu = !bMenu;
            MainMenu.SetActive(bMenu);
        }
    }


    IEnumerator GameMainProcess()
    {
        while(!gameStart)
        {
            yield return null;
        }
        while(!gameEnd)
        {
            CanSelectCard = true;
            AnimationManager.instance.CardHighlight(CanSelectCard);
            isSelecting = true;
            while (isSelecting)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
            if (currentNum == 1 ||  currentNum == 5 || currentNum == 7
                || currentNum == 14 || currentNum == 15 || currentNum == 20)
                yield return StartCoroutine(Battle());
            else if (currentNum == 29 || currentNum == 32 || currentNum == 35)
            {
                GameEnd();
            }
            else 
                yield return StartCoroutine(GameEvent());

        }
    }
    IEnumerator Battle()
    {

        SpawnEnemy(currentNum);
        SoundManager.instance.SwitchToBattleBGM();
        PlayerTurn = true;
        while (enemyCount > 0)
        {
            if (PlayerTurn)
            {
                ThrowDiceBtn.SetActive(true);
                isChoosing = true;
                ResetCounter();
                while (isChoosing)
                {
                    yield return null;
                }
                if (targetCounterSum() > 0)
                {
                    AnimationManager.instance.FightPlayer();
                    SFX.PlayOneShot(FightClip);
                    yield return new WaitForSeconds(0.8f);
                }
                foreach(var enemy in enemies)
                {
                    while ( enemy.targetCounter > 0 && !enemy.IsDead)
                    {
                        enemy.Damage(Player.instance.Attack);
                        enemy.targetCounter--;
                        enemy.UpdateText();
                        yield return new WaitForSeconds(0.5f);
                    }
                }
                JusticeSwordBtn.SetActive(false);
                PlayerTurn = false;
            }
            else
            {
                AnimationManager.instance.FightEnemy();
                SFX.PlayOneShot(FightClip);
                foreach (var enemy in enemies)
                {
                    if (enemy.IsDead)
                        continue;
                    Player.instance.Damage(enemy.attack);

                }
                PlayerTurn = true;

            }
        }
        foreach(var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        
        SFX.PlayOneShot(BattleVictoryClip);
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.SwitchToBGM();
        ResetCounter();
        Player.instance.ResetProperty();
        enemies.Clear();
    }

    IEnumerator GameEvent()
    {
        yield return new WaitForSeconds(0.5f);
        if (((currentNum == 3 || currentNum == 8 || currentNum == 11 || currentNum == 12
                || currentNum == 16 || currentNum == 24) && hasChooseA)||(currentNum == 26 && !hasChooseA))
        {
            isReading = true;
            IllustrationDlg.SetActive(true);
            
            while (isReading)
            {
                yield return null;
            }
        }
        if (currentNum == 3 && hasChooseA)
        {
            Player.instance.Recover(20);
        }
        if (currentNum == 8 && hasChooseA)
        {
            Player.instance.Attack += 3;
        }
        if (currentNum == 11 && hasChooseA)
        {
            Player.instance.Attack += 1;
        }
        if (currentNum == 12 && hasChooseA)
        {
            Player.instance.DamageHealth(15);
            Player.instance.Attack += 3;
        }
        if (currentNum == 16 && hasChooseA)
        {
            Player.instance.DamageHealth(10);
        }
        if (currentNum == 18 && hasChooseA)
        {
            yield return StartCoroutine(Battle());
            isReading = true;
            IllustrationDlg.SetActive(true);

            while (isReading)
            {
                yield return null;
            }
            Player.instance.Attack += 5;
            Player.instance.Armor += 5;
        }
        if (currentNum == 24 && hasChooseA)
        {
            CardManager.instance.ChangeSequence();
            transitionA = true;
            Player.instance.Attack += 10;
            Player.instance.Armor += 5;
            Player.instance.MaxHealth -= 15;
        }
        else if(currentNum == 24 && !hasChooseA)
            transitionA = false;
        if (currentNum == 26 && !hasChooseA)
        {
            transitionB = false;
            CardManager.instance.chooseEnding(transitionA, transitionB);
            Player.instance.Recover(20);
            Player.instance.Armor += 3;
        }
        else if (currentNum == 26 && hasChooseA)
        {
            transitionB = true;
            CardManager.instance.chooseEnding(transitionA, transitionB);
        }
        if (currentNum == 2 && hasChooseA)
        {
            yield return StartCoroutine(Battle());
        }
        else if (currentNum == 2 && !hasChooseA)
        {
            DiceManager.instance.ThrowDices();
            yield return new WaitForSeconds(0.5f);
            GetCounterFromDices(false);
            DiceManager.instance.ResetDices();
            if (chargeCount < 3)
            {
                Player.instance.DamageHealth(10);                
                ResetCounter();
                yield return StartCoroutine(Battle());
            }
            else
            {
                SFX.PlayOneShot(VictoryClip);
            }
        }
        if (currentNum == 9 && hasChooseA)
        {
            DiceManager.instance.ThrowDices();
            yield return new WaitForSeconds(0.5f);
            GetCounterFromDices(false);
            DiceManager.instance.ResetDices();
            if (chargeCount < 3)
                Player.instance.DamageHealth(10);
            else
                Player.instance.Attack += 3;
            ResetCounter();
        }
        if (currentNum == 17)
        {
            DiceManager.instance.ThrowDices();
            yield return new WaitForSeconds(0.5f);
            GetCounterFromDices(false);
            DiceManager.instance.ResetDices();
            if (shieldCount <= 3 && shieldCount >= 2)
            {
                Player.instance.DamageHealth(10);
            }
            else if (shieldCount < 2)
                Player.instance.DamageHealth(15);
            else
            {
                SFX.PlayOneShot(VictoryClip);
            }
            ResetCounter();
        }
        if (currentNum == 19)
        {
            DiceManager.instance.ThrowDices();
            yield return new WaitForSeconds(0.5f);
            GetCounterFromDices(false);
            DiceManager.instance.ResetDices();
            if (swordCount < 3)
            {
                Player.instance.DamageHealth(10);
                Player.instance.Armor -= 2;
            }
            else
            {
                SFX.PlayOneShot(VictoryClip);
            }
            ResetCounter();
        }
        if (currentNum == 4)
        {
            Player.instance.Attack -= 2;
        }
        if (currentNum == 10)
            Player.instance.Armor += 5;
        if (currentNum == 13)
        {
            Player.instance.Recover(30);
        }
    }
   
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GameBegin()
    {
        gameStart = true;
    }
    public void GameEnd()
    {

        if (Player.instance.Health <= 0)
        {
            EndText.text = "你死了";
            SFX.PlayOneShot(GameOverClip);
        }
        else
        {
            SFX.PlayOneShot(CongratulationClip);
        }
        enabled = false;
        GameOverDlg.SetActive(true);
    }
    
    public void GetCounterFromDices(bool isBattle)
    {
        if (getCounter != null)
            getCounter();
        if (isBattle)
        {
            Player.instance.CalculateCounter();
            if (Player.instance.Charge == Player.instance.FullCharge)
                JusticeSwordBtn.SetActive(true);
        }
        Debug.Log(swordCount + " " + shieldCount + " " + chargeCount);
        Debug.Log("currentNum:" + currentNum);
    }
    private void ResetCounter()
    {
        swordCount = 0;
        shieldCount = 0;
        chargeCount = 0;
    }
    public int targetCounterSum()
    {
        int sum = 0;
        foreach (var enemy in enemies)
        {
            sum += enemy.targetCounter;
        }
        return sum;
    }
    private void AddEnemy(int enemyNum,int positionNum)
    {
        
        EnemyPrefab.GetComponent<Enemy>().ChangeProperty(enemyNum);
        Instantiate(EnemyPrefab, EnemySpawnPoint[positionNum]);
        enemyCount++;
        Debug.Log("SpawnEnemy:" + enemyNum);

    }
    //根据故事调整敌人
    public void SpawnEnemy(int storyNum)
    {
        switch (storyNum)
        {
            case 1: AddEnemy(0,0); AddEnemy(0,1); AddEnemy(0, 2); break;//哥布林盗贼*3
            case 2: AddEnemy(1,1); break;//巨大魔猪
            case 5: AddEnemy(2,0); AddEnemy(2,2); AddEnemy(2, 3); AddEnemy(2, 4); break;//骷髅士兵*4
            case 7: AddEnemy(3,1); break;//巨型食人魔
            case 14: AddEnemy(4,0); AddEnemy(4, 1); AddEnemy(4, 2); AddEnemy(4, 3); AddEnemy(4, 4); break;//地精盗贼*5
            case 15: AddEnemy(5,0); AddEnemy(5,1); AddEnemy(5, 2); break;//地精战士*3
            case 18: AddEnemy(6,1); break;//骷髅战士
            case 20: AddEnemy(7,1);  break;//恶龙
            default:break;
        }
    }
    public void AbandonCards()
    {
        if (abandonCard != null)
            abandonCard();
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadGame(bool A, bool B)
    {
        transitionA = A;
        transitionB = B;
    }

    public bool GetTransA()
    {
        return transitionA;
    }

    public bool GetTransB()
    {
        return transitionB;
    }
}
