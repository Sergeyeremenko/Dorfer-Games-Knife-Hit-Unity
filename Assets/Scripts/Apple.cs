using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static Apple Instance;

    [SerializeField] public ParticleSystem appleParticle;

    private CircleCollider2D myCollider2D;
    private SpriteRenderer sp;

    private void Start()
    {
        myCollider2D = GetComponent<CircleCollider2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Knife"))
        {
            myCollider2D.enabled = false;
            sp.enabled = false;
            transform.parent = null;
            SoundManager.Instance.PlayAppleHit();
            SoundManager.Instance.Vibrate();
            GameManager.Instance.TotalApple++;
            appleParticle.Play();
            Debug.Log("AppleParticle!");
            Destroy(gameObject, 2f); // время анимации эппл
        }
    }
}
