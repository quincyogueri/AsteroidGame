using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    public int counter = 0;
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float AsteroidSpeed = 7.0f;
    public float lifeTime = 50.0f;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    Vector3 lastVelocity;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    void Update()    
    {
        lastVelocity = _rigidbody.velocity;

    }

    
    private void Start()
    {
        //list of asteroid sprites 
       _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        //random rotation of asteroids
       this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f); 
       this.transform.localScale = Vector3.one * this.size;

       _rigidbody.mass = this.size;
    }

    //sets asteroid on a straight consistent path
    public void SetTrajectory(Vector2 direction)
    {   
        _rigidbody.AddForce(direction * this.AsteroidSpeed);

        Destroy(this.gameObject, this.lifeTime);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        counter++;
        //checks for collision between boundary and asteroid clone of clone(asteroids from broken asteroids)
        if ((collision.gameObject.layer == LayerMask.NameToLayer("Boundary")) && (gameObject.layer == LayerMask.NameToLayer("Clone")))
        {
            //reflects asteroid following Newton's Law
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            
            _rigidbody.velocity = direction * speed;

        }

        //checks for collision between asteroid and boundary
        if (collision.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            if (counter > 1)
            {
                // ""
                var speed = lastVelocity.magnitude;
                var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            
                _rigidbody.velocity = direction * speed;

            } 
            //checks for collision between asteroid and bullet
        } else if (collision.gameObject.tag == "Bullet")
        {
            //creates asteroid split if size of asteroid is bigger than minimum size and destroys larger asteroid
            if(((this.size) / 2)>= this.minSize)
            {
                CreateSmallerAsteroid();
                CreateSmallerAsteroid();
            }
            FindObjectOfType<GameController>().AsteroidDestroyed(this);
            Destroy(this.gameObject);
            //destroys asteroid after colliding with player
        } else if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<GameController>().AsteroidDestroyed(this);
            Destroy(this.gameObject);
        } 
    }

    //splits asteroid in half
    private void CreateSmallerAsteroid()
    {
        Vector2 nPosition = this.transform.position;
        nPosition += Random.insideUnitCircle * 0.5f;

        Asteroid split = Instantiate(this, nPosition, this.transform.rotation);
        split.gameObject.layer= LayerMask.NameToLayer("Clone");

        split.size = this.size / 2;
        split.SetTrajectory(Random.insideUnitCircle.normalized * this.AsteroidSpeed );

    }
}
