using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private RubyController ry;

    public GameObject HitPartical;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3f);
    }

    public void Launch(Vector2 direction,float forceSize)
    {
        rigidbody2d.AddForce(direction * forceSize);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemyController = collision.transform.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.Fix();
        }
        GameObject.Instantiate(HitPartical, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    ry = collision.GetComponent<RubyController>();
    //    if (ry == null)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
