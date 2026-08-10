using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
