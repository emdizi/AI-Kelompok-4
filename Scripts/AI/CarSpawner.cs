using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour
{
    
    public GameObject carPrefabs;
    public GameObject spawnerposition;

    //public float spawnRate = 2;
    //private float timer = 0;

    private void Start()
    {

    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.Vector2 spawnposition = spawnerposition.transform.position;
            Instantiate(carPrefabs,spawnposition, quaternion.identity);
        }


        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            Destroy(carPrefabs);
        }
        /*if (timer < spawnRate) {
            timer = timer + Time.deltaTime;
        }
        else {
            spawnCar();
            timer = 0;
        }*/
    }

    /*private GameObject SelectACarPrefab()
    {
        var randomIndex = Random.Range(0, carPrefabs.Length);
        return carPrefabs[randomIndex];
    }*/

    void spawnCar(){
        
    }
}
