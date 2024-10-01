using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI CoinText;
    public Blades BladeSettings;
    public int Chances;
    [Space(20)]
    public GameObject GameOverPanel;
    public GameObject GameWinPanel;
    public GameObject Player;
    public GameObject CaveMask;
    [Space(20)]
    public GameObject ChancesParent;
    public GameObject ChancesPrefeb;
    public GameObject CheckPointsParent;

    #region Static Values
    public static TextMeshProUGUI S_CoinText;
    [HideInInspector] public static int CurruntCoins;
    [HideInInspector] public static int S_CurruntChances;
    [HideInInspector] public static float S_BladeOfset;
    [HideInInspector] public static float S_BladeSpeed;
    [HideInInspector] public static float S_StayTime;
    [HideInInspector] public static float S_DelayTime;
    [HideInInspector] public static GameObject S_GameOverPanel;
    [HideInInspector] public static GameObject S_GameWinPanel;
    [HideInInspector] public static bool IsGameOver;
    [HideInInspector] public static GameObject S_Player;
    [HideInInspector] public static GameObject S_CaveMask;
    [HideInInspector] public static GameObject S_ChancesParent;
    [HideInInspector] public static GameObject S_ChancesPrefeb;
    static bool CanRespawn;
    #endregion
    // Start is called before the first frame update

    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
    void Start()
    {
        IsGameOver = false;
        S_CoinText = CoinText;
        S_BladeOfset = BladeSettings.BladeOfset;
        S_BladeSpeed = BladeSettings.BladeSpeed;
        S_StayTime = BladeSettings.StayTime;
        S_DelayTime = BladeSettings.DelayTime;
        S_GameOverPanel = GameOverPanel;
        S_GameWinPanel = GameWinPanel;
        S_Player = Player;
        S_CaveMask = CaveMask;
        S_CurruntChances = Chances;
        S_ChancesParent = ChancesParent;
        S_ChancesPrefeb = ChancesPrefeb;
        UpdateChances();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanRespawn)
        {
            StartCoroutine(ReSpawn());
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
    }

    public static void UpdateCoinsText()
    {
        S_CoinText.text = "Coins: " + CurruntCoins.ToString();
    }

    public static void GameOver()
    {
        S_GameOverPanel.SetActive(true);
        IsGameOver = true;
    }
    public static void GameWin()
    {
        S_GameWinPanel.SetActive(true);
        IsGameOver = true;
    }

    public static void PlayerWin()
    {
        S_Player.transform.SetParent(S_CaveMask.transform);
        S_Player.GetComponent<Animator>().enabled = true;
        S_Player.GetComponent<Animator>().SetTrigger("WIn");
        GameWin();
    }
    public void Restart()
    {
        CurruntCoins = 0;
        IsGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public static void PlayerHit()
    {
        S_CurruntChances--;
        UpdateChances();
        if(S_CurruntChances <= 0)
        {
            GameOver();
        }
        else
        {
            IsGameOver = true;
            CanRespawn = true;
        }
    }

    IEnumerator ReSpawn()
    {
        CanRespawn = false;
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < CheckPointsParent.transform.childCount; i++)
        {
            if (CheckPointsParent.transform.GetChild(i).gameObject.GetComponent<CheckPoints>().Checked)
            {
                Player.transform.position = CheckPointsParent.transform.GetChild(i).gameObject.transform.position;
                IsGameOver = false;
            }
        }
    }

    static void UpdateChances()
    {
        for (int i = 0; i < S_ChancesParent.transform.childCount; i++)
        {
            Destroy(S_ChancesParent.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < S_CurruntChances; i++)
        {
            Instantiate(S_ChancesPrefeb, S_ChancesParent.transform);
        }
    }
}
[System.Serializable]
public class Blades
{
    public float BladeOfset;
    public float BladeSpeed;
    public float StayTime;
    public float DelayTime;
}