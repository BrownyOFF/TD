using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Variables
    public int playerHealth = 3;
    public int money = 100;
    public int score = 0;
    public int bowPrice = 100;
    public int artPrice = 250;
    public int gradePrice = 500;

    
    [SerializeField] private Text UIHealth;
    [SerializeField] private Text UIScore;
    [SerializeField] private Text UIMoney;
    [SerializeField] private Text UIMess;
    [SerializeField] private Text UILose;
    [SerializeField] private Text UILevel;
    [SerializeField] private Toggle Bow;
    [SerializeField] private Toggle Art;
    [SerializeField] private Toggle Grade;

    [SerializeField] private GameObject Enemy;
    [SerializeField] private Transform pointToStart;

    [SerializeField] private Sprite bowSprite;
    [SerializeField] private Sprite artSprite;
    private SpriteRenderer spotSprite;
    
    public AudioSource audioSource;
    public AudioSource upgradeSound;

    private GameObject Child;
    
    //Level Variables
    public bool level2 = false;
    public bool level3 = false;
    public bool levelF = false;
    
    #endregion
    void Start()
    {
        InvokeRepeating("CreateEnemy", 5, 5);
        UILevel.text = "Уровень 1";
    }

    void Update()
    {
        if (Bow.isOn)
        {
            Art.isOn = false;
            Grade.isOn = false;
        }
        if (Art.isOn)
        {
            Bow.isOn = false;
            Grade.isOn = false;
        }
        if (Grade.isOn)
        {
            Art.isOn = false;
            Bow.isOn = false;
        }
        UIHealth.text = "HP: " + playerHealth;
        UIMoney.text = "Money: " + money;
        UIScore.text = "Score: " + score;
        Enemy = GameObject.FindWithTag("Enemy");

        // Choose spot
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit)
            {
                if (hit.collider.tag == "Spot")
                {
                    spotSprite = hit.collider.GetComponent<SpriteRenderer>();
                    TowerBuild(hit);
                }
                else if (hit.collider.tag == "Art" || hit.collider.tag == "Bow")
                {
                    Upgrade(hit);
                }
            }
        }
        CheckLevel();
        if (playerHealth == 0)
        {
            GameOver();
        }
    }

    void TowerBuild(RaycastHit2D hit)
    {
        if ((Art.isOn && money < artPrice) || (Bow.isOn && money < bowPrice))
        {
            UIMess.text = "Недостаточно денег";
            return;
        }
        if (!Art.isOn && !Bow.isOn)
        {
            UIMess.text = "Выбери постройку";
            return;
        }

        if (Art.isOn && money >= artPrice)
        {
            spotSprite.sprite = artSprite;
            hit.transform.gameObject.tag = "Art";
            money -= artPrice;
            hit.collider.gameObject.GetComponent<ArtScript>().enabled = true;
        }
        else if (Bow.isOn && money >= bowPrice)
        {
            spotSprite.sprite = bowSprite;
            hit.transform.gameObject.tag = "Bow";
            money -= bowPrice;
            hit.collider.gameObject.GetComponent<BowScript>().enabled = true;
        }
        audioSource.Play();
            
    }

    void Upgrade(RaycastHit2D hit)
    {
        // Execeptions
        if (money < gradePrice)
        {
            UIMess.text = "Недостаточно денег!";
            return;
        }
        if (hit.transform.gameObject.GetComponent<BowScript>().graded
                 || hit.transform.gameObject.GetComponent<ArtScript>().graded)
        {
            UIMess.text = "Максимальный грейд";
            return;
        }
        
        if (hit.transform.gameObject.tag == "Bow")
        {
            hit.transform.gameObject.GetComponent<BowScript>().damage++;
            hit.transform.gameObject.GetComponent<BowScript>().coolDown--;
            money -= gradePrice;
            Child = hit.transform.gameObject.transform.GetChild(0).gameObject;
            Child.GetComponent<SpriteRenderer>().enabled = true;
            hit.transform.gameObject.GetComponent<BowScript>().graded = true;
        }
        else
        {
            hit.transform.gameObject.GetComponent<ArtScript>().damage++;
            hit.transform.gameObject.GetComponent<ArtScript>().coolDown--;
            money -= gradePrice;
            Child = hit.transform.gameObject.transform.GetChild(0).gameObject;
            Child.GetComponent<SpriteRenderer>().enabled = true;
            hit.transform.gameObject.GetComponent<ArtScript>().graded = true;

        }
        upgradeSound.Play();
    }

    void CreateEnemy()
    {
        Instantiate(Enemy, new Vector3(pointToStart.position.x, pointToStart.position.y, 0), Quaternion.identity);
    }

    void CheckLevel()
    {
        if (score == 1000 && !level2)
        {
            InvokeRepeating("CreateEnemy", 4, 4);
            money += 500;
            level2 = true;
            UILevel.text = "Уровень 2";
        }
        else if (score == 2000 && !level3)
        {
            InvokeRepeating("CreateEnemy", 3, 3);
            money += 500;
            level3 = true;
            UILevel.text = "Уровень 3";
        }
        else if (score == 3000 && !levelF)
        {
            InvokeRepeating("CreateEnemy", 1, 1);
            InvokeRepeating("CreateEnemy", 1, 1);
            InvokeRepeating("CreateEnemy", 1, 1);
            money += 500;
            levelF = true;
            UILevel.text = "Финальный уровень(Тебе конец)";
        }
    }
    
    void GameOver()
    {
        UILose.text = "Игра Окончена. Гоблины убили твою семью(";
    }
}
