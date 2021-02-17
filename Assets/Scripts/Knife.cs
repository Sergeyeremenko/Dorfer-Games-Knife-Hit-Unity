using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private float speed;

    public Rigidbody2D myRigidbody2D;
    public bool IsReleased { get; set; }

    [SerializeField] private ParticleSystem wheelParticle; // партиклы для разрушения бревна
    public bool Hit { get; set; }
    private Animator camAnim; // анимация камеры - тряска

    private void Start()
    {
        camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>(); // ищем по тегу камеру
    }
    public void FireKnife()
    {
        if (!IsReleased)
        {
            IsReleased = true;
            myRigidbody2D.AddForce(new Vector2(0f, speed), ForceMode2D.Impulse);
            SoundManager.Instance.PlayKnifeFire(); // звук стрельбы
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wheel") && !Hit && !GameManager.Instance.IsGameOver && IsReleased)
        {
            wheelParticle.Play(); // включаем партиклы для бревна при попадании
            other.gameObject.GetComponent<Wheel>().KnifeHit(this);
            GameManager.Instance.Score++; // +1 к счету
            SoundManager.Instance.Vibrate(); // Добавляем вибрацию)
            //GameManager.Instance.TotalApple++; // типо дабавил к счету +1  яблоко..
            SoundManager.Instance.PlayWheelHit();
            camAnim.SetTrigger("Shake");
            //wheelParticle.Play();
            //Apple.Instance.appleParticle.Play();
            //Destroy(gameObject, 4f);
            //Debug.Log("Particl Wheel!");
        }
        else if (other.gameObject.CompareTag("Knife") && !Hit && !GameManager.Instance.IsGameOver && other.gameObject.GetComponent<Knife>().IsReleased)
        {
            Hit = true;
            transform.SetParent(other.transform);

            myRigidbody2D.velocity = Vector2.zero;
            myRigidbody2D.isKinematic = true;

            SoundManager.Instance.PlayGameOver();
            SoundManager.Instance.Vibrate();
            camAnim.SetTrigger("Shake");

            GameManager.Instance.IsGameOver = true;
            Invoke(nameof(GameOver), 0.2f);
        }
    }

    private void GameOver()
    {
        UIManager.Instance.GameOver();
        SoundManager.Instance.Vibrate();
    }
}
