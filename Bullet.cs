using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float speed = 500.0f;
    private Rigidbody2D _rigidbody;
    public float lifeTime = 10.0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    //sets trajectory of bullet
    public void Project(Vector2 direction) 
    {
        _rigidbody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.lifeTime);
    }

    //deletes bullet on collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

}
