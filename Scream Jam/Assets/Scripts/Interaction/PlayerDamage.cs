using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    float playerhealth = 100;
    [SerializeField]
    int maxHealh = 100;


    public Sprite emptyHeart;
    public Sprite fulHeart;
    public Image[] hearts;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i <playerhealth)
            {
                hearts[i].sprite = fulHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }


            if (i < maxHealh)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if(playerhealth < 75)
        {
            hearts[3].sprite = emptyHeart;
        }
        if (playerhealth < 50)
        {
            hearts[2].sprite = emptyHeart;
        }
        if (playerhealth < 25)
        {
            hearts[1].sprite = emptyHeart;
        }


        if(playerhealth <=0)
        {
            SceneManager.LoadScene("DeathScreen");
            //SceneManager.UnloadScene("GameScene");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            playerhealth --;

        }
    }
}
