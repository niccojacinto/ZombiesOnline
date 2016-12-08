using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed;
    public float lifeTime;
    public float damage = 5.0f;

    // public GameObject deathParticle;
    void Start() {
        Destroy(gameObject, lifeTime);
    }

    void Update() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<SoldierCharacter>().TakeDamage(damage);
        }
        else if(col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<AIController>().TakeDamage(damage);
        }

        Destroy(gameObject);
        // Destroy(Instantiate(deathParticle, transform.position, Quaternion.identity), 0.3f);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "EnemyHead" || col.gameObject.tag == "EnemyLeftArm" || col.gameObject.tag == "EnemyRightArm")
        {
            col.gameObject.GetComponent<Dismember>().BulletHit();
        }
    }
}
