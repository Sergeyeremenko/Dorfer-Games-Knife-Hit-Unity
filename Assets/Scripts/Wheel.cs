using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Wheel : MonoBehaviour
{
    public static Wheel Instance;

    [SerializeField] private int availableKnifes;

    [SerializeField] private Sprite firstWheel;
    [SerializeField] private Sprite secondWheel;
    [SerializeField] private Sprite thirdWheel;

    [SerializeField] private ParticleSystem wheelParticle; // партиклы дл€ разрушени€ бревна

    [SerializeField] private bool isBoss;

    [Header("Prefabs")]
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject knifePrefab;

    [Header("Settings")]
    [SerializeField] private float rotationZ;

    public List<Level> levels;

    [HideInInspector] public List<Knife> Knifes;

    private int levelIndex;

    public int AvailableKnifes => availableKnifes;

    private void Start()
    {
        if (!isBoss)
        {
            if (GameManager.Instance.Stage < 5)
            {
                GetComponent<SpriteRenderer>().sprite = firstWheel;
            }
            else if (GameManager.Instance.Stage > 5 && GameManager.Instance.Stage < 10)
            {
                GetComponent<SpriteRenderer>().sprite = secondWheel;
            }
            else if (GameManager.Instance.Stage > 10)
            {
                GetComponent<SpriteRenderer>().sprite = thirdWheel;
            }
        }

        levelIndex = Random.Range(0, levels.Count);

        if (levels[levelIndex].appleChance > Random.value)
        {
            SpawnApple();
        }
        SpawnKnifes();
    }
    private void RotateWheel()
    {
        transform.Rotate(0f, 0f, rotationZ * Time.deltaTime); // вращение бревна
    }

    private void Update() // апдейтим вращение
    {
        RotateWheel();
    }

    private void SpawnKnifes() // —павн ножей в дереве
    {
        foreach (float knifeAngle in levels[levelIndex].knifeAngleFromWheel)
        {
            GameObject knifeTmp = Instantiate(knifePrefab);
            knifeTmp.transform.SetParent(transform);

            SetRotationFromWheel(transform, knifeTmp.transform, knifeAngle, 0.20f, 180f);
            knifeTmp.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
    }

    private void SpawnApple() // спавн €блок на бревне
    {
        foreach (float appleAngle in levels[levelIndex].appleAngleFromWheel)
        {
            GameObject appleTmp = Instantiate(applePrefab);
            appleTmp.transform.SetParent(transform);

            SetRotationFromWheel(transform, appleTmp.transform, appleAngle, 0.25f, 0f);
            appleTmp.transform.localScale = Vector3.one;
        }
    }

    public void SetRotationFromWheel (Transform wheel, Transform objectToPlace, float angle, float spaceFromObject, float objectRotation) // вращени€ бревна
    {
        Vector2 offSet = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)) * (wheel.GetComponent<CircleCollider2D>().radius + spaceFromObject);
        objectToPlace.localPosition = (Vector2) wheel.localPosition + offSet;
        objectToPlace.localRotation = Quaternion.Euler(0, 0, -angle + objectRotation);
    }

    public void KnifeHit (Knife knife)
    {
        knife.myRigidbody2D.isKinematic = true;
        knife.myRigidbody2D.velocity = Vector2.zero;
        knife.transform.SetParent(transform);
        knife.Hit = true;


        Knifes.Add(knife);
        if (Knifes.Count >= AvailableKnifes)
        {
            wheelParticle.Play();
            Destroy(gameObject, 4f);
            Debug.Log("Particl Wheel!");


            LevelManager.Instance.Nextlevel();
            //wheelParticle.Play(); // партикл разрушени€ бревна
            //Destroy(gameObject, 2f); // врем€ партикла дл€ анимации разрушени€
        }
        GameManager.Instance.Score++; // добавление к счЄту
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Knife"))
        {
            wheelParticle.Play();
            Destroy(gameObject, 2f); // врем€ аимации эппл
        }
    }

    public void DestroyKnife()
    {
        foreach (var knife in Knifes)
        {
            Destroy(knife.gameObject);
        }
        Destroy(gameObject);
    }
}

[Serializable]
public class Level
{
    [Range (0, 1)] [SerializeField] public float appleChance;

    public List<float> appleAngleFromWheel = new List<float>();
    public List<float> knifeAngleFromWheel = new List<float>();
}

