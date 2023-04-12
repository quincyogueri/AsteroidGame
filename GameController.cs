using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public Player player;
    public Asteroid asteroid;
    public ParticleSystem ExplosionEffect;
    public AudioSource explosion;
    public int lives = 3;
    public float respawnRate = 3.0f;
    public int score = 00000;
    public TMP_Text ScoreText;
    public TMP_Text LivesText;

    void Update()
    {
        //continuously updates score and lives on screen
        ScoreText.text = score.ToString();
        LivesText.text = lives.ToString();
    }

    public void PlayerDead()
    {
        //creates explosion effect if player dies
        this.ExplosionEffect.transform.position = this.player.transform.position;
        this.ExplosionEffect.Play();
        explosion.Play();

        this.lives--;

        if (this.lives <= 0)
        { 
            GameOver();
        } else {
            Invoke(nameof(RespawnPlayer), this.respawnRate);
        }
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        //creates explosion effect once asteroid is destroyed
        this.ExplosionEffect.transform.position = asteroid.transform.position;
        this.ExplosionEffect.Play();
        explosion.Play();

        //scoring system
        if (asteroid.size < 0.75)
        {
            this.score += 100;
        } else if (asteroid.size < 1.2f) {
            this.score += 50;
        } else {
            this.score += 25;
        }
    }
    
    private void RespawnPlayer()
    {
        //resumes game after being paused
        Time.timeScale = 1.0f;
        //sets layer to ignore collisions for immunity of 3 seconds
        this.player.gameObject.layer= LayerMask.NameToLayer("Ignore Collisions(P)");
        Invoke(nameof(TurnOnPCollisions), 3.0f);
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.SetActive(true);
    }

    //function for destroying all game objects of a particular tag
    public void DestroyObjects(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject target in gameObjects) {
            GameObject.Destroy(target);
        }
    }

    //reactivate player vulnerability function
    private void TurnOnPCollisions()
    {
        this.player.gameObject.layer= LayerMask.NameToLayer("Player");
    }

    private void setScoreZero()
    {
        this.score = 0;
    }

    private void GameOver()
    {
        this.lives = 3;
        Invoke(nameof(setScoreZero), 2.5f);
        
        Invoke(nameof(RespawnPlayer), 3.0f);

        DestroyObjects("Asteroid");
    }   

}
