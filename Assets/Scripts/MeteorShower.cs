using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShower : MonoBehaviour
{
    public GameObject[] rock;
    [SerializeField] private int noOfMeteors;
    [SerializeField] private float totaltime;
    private int noOfMeteorsFalling;

    [SerializeField] private float fallheight;
    [SerializeField] private float xRange;
    [SerializeField] private float zRange;
    private int rockListLenght;

    private void Start()
    {
        totaltime = 10f;
        noOfMeteorsFalling = Random.Range(0, noOfMeteorsFalling);
        rockListLenght = rock.Length;

    }
    private void Update()
    {
        if (totaltime > 0)
        {
            totaltime -= Time.deltaTime;
        }
        else
        {
            totaltime = 10f;
            RockShower();
        }
    }
    private void RockShower()
    {
        for (int i = 0; i < noOfMeteors; i++)
        {
            GameObject meteor = Instantiate(rock[Random.Range(0, rockListLenght)]);
            int scale = Random.Range(2, 8);
            meteor.transform.localScale = new Vector3(scale, scale, scale);         
            meteor.transform.rotation = Random.rotation;
            meteor.transform.position = new Vector3(Random.Range(-xRange, xRange),
                Random.Range(150, fallheight),
                Random.Range(-zRange, zRange));
            meteor.SetActive(true);
            meteor.GetComponent<Rigidbody>().AddForce(Random.Range(-100,100),
                Random.Range(-100, 100),
                Random.Range(-100, 100));
        }
    }
}
