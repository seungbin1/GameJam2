using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    #region Sigleton
    private static PlayerStatsManager _instance;
    public static PlayerStatsManager Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
                var obj = FindObjectOfType<PlayerStatsManager>();
                if (obj != null)
                {
                    _instance = obj;
                }
                else
                {
                    var playerStatsManager = new GameObject("PlayerStatsManager").AddComponent<PlayerStatsManager>();
                    _instance = playerStatsManager;
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        var objs = FindObjectsOfType<PlayerStatsManager>();
        if (objs.Length != 1)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    [SerializeField] private int hp = 1;
    public int HP { get => hp; }

    [SerializeField] private int maxHp = 3;
    public int MaxHp { get => maxHp; }

    [SerializeField] private float jumpPower = 20f;
    public float JumpPower { get => jumpPower; }

    public void TakeDamage()
    {
        this.hp--;
        if (hp < 1)
        {
            Die();
        }
        ClampHP();
    }
    public void InitHp()
    {
        hp = 1;
    }

    public void TakeHeal(int heal)
    {
        this.hp += heal;
        ClampHP();
    }

    void ClampHP()
    {
        hp = Mathf.Clamp(hp, 0, maxHp);
    }

    void Die()
    {
        GameManager.Instance.gameState = GameManager.GameState.GameOver;
        GameManager.Instance.GameOver();
        GameManager.Instance.InitScore();
        SoundManager.Instance.StopGame();
    }
}
