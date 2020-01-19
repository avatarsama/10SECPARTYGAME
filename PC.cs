using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PC : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public int timeLeft = 10;

    public UnityEngine.UI.Text countdown;

    public UnityEngine.UI.Text win;

    public UnityEngine.UI.Text end;

    public AudioSource musicSource;

    public AudioClip bgClip;

    public AudioClip winClip;

    public AudioClip loseClip;



    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        win.text = "";
        end.text = "";
        StartCoroutine("LoseTime");
        Time.timeScale = 1;
        musicSource.clip = bgClip;
        musicSource.Play();
    }

    void Update()
    {
        countdown.text = ("Time Left: " + timeLeft);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            timeLeft--;
        }
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Finish")
        {
            StopCoroutine("LoseTime");
            win.text = "You Win!";
            end.text = "Press Esc. to Exit Game";
            musicSource.clip = (bgClip);
            musicSource.Stop();
            musicSource.clip = winClip;
            musicSource.Play();
            Destroy(collision.collider.gameObject);
        }
        else
        {
            if (timeLeft <= 0)
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    { 
        StopCoroutine("LoseTime");
        win.text = "You Lose!";
        end.text = "Press Esc. to Exit Game";
        musicSource.clip = (bgClip);
        musicSource.Stop();
        musicSource.clip = (loseClip);
        musicSource.Play();
        Destroy(GameObject.FindWithTag("Finish"));
    }
}