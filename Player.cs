using UnityEngine;

public class Player : MonoBehaviour
{   
    
    public AudioSource shootingSound;
    public Bullet bulletPrefab;
    public float speed = 2.0f;
    public float turnSpeed = 0.35f;
    private Rigidbody2D _rigidbody;
    private bool _accelerating;
    private float _turnDir;
    private bool _decelerating;

    private void Awake(){
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Only accepts user input if game is not paused
        if (!PauseMenu.isPaused)
        {
            //user input, movement and shooting
            _accelerating = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
            _decelerating = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);


            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                _turnDir = 1.0f;
            } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                _turnDir = -1.0f;
            } else {
                _turnDir = 0.0f;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
                Trigger();
                shootingSound.Play();
            }
        }
    }



    private void FixedUpdate() 
    {
        //checking for is user is moving and applying effect on spaceship
        if (_accelerating) {
            _rigidbody.AddForce(this.transform.up*speed);
        }

        if (_decelerating) {
            _rigidbody.AddForce(this.transform.up*-speed);
        }

        if (_turnDir != 0.0f) {
            _rigidbody.AddTorque(_turnDir*turnSpeed);
        }
    }

    //triggering laser/bullet
    private void Trigger() 
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    //checking for if player collides with asteroid
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameController>().PlayerDead();

        }
    }
}