using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    float playerHealth = 100;
    [SerializeField]
    int maxHealh = 100;


    public Sprite emptyHeart;
    public Sprite fulHeart;
    public Image[] hearts;

    [SerializeField] Image heart;
    [SerializeField] Image heart1;
    [SerializeField] Image heart2;
    [SerializeField] Image heart3;


    private void Awake()
    { 
        DontDestroyOnLoad(heart);
        DontDestroyOnLoad(heart1);
        DontDestroyOnLoad(heart2);
        DontDestroyOnLoad(heart3);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = 100;
        hearts = new Image[4];

        hearts[0] = heart;
        hearts[1] = heart1;
        hearts[2] = heart2;
        hearts[3] = heart3;

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < playerHealth)
                {
                    hearts[i].sprite = fulHeart;
                }
                else
                {
                    if (hearts[i] != null)
                    {
                        Debug.Log("hearts good");
                    }
                    else
                    {
                        Debug.Log("Broken here");
                    }
                    if (hearts[i].sprite != null)
                    {
                        Debug.Log("Heart sprite good");
                    }
                    if (emptyHeart != null)
                    {
                        Debug.Log("Empty heart good");
                    }
                    hearts[i].sprite = emptyHeart;
                }


                if (i < maxHealh)
                {
                    hearts[i].enabled = true;
                }
                //else
                //{
                //    hearts[i].enabled = false;
                //}
            }

            if (playerHealth < 75)
            {
                hearts[3].sprite = emptyHeart;
            }
            if (playerHealth < 50)
            {
                hearts[2].sprite = emptyHeart;
            }
            if (playerHealth < 25)
            {
                hearts[1].sprite = emptyHeart;
            }


            if (playerHealth <= 0)
            {
                SceneManager.LoadScene("DeathScreen");
                //SceneManager.UnloadScene("GameScene");
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            playerHealth --;

        }
    }
}
