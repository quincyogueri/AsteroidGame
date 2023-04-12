using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aSpawner : MonoBehaviour
{ 
    public float tvariance = 15.0f;
    public float spawnRate;
    public Asteroid asteroidPrefab;
    public int spawnAmount = 1;
    public float spawnDistance = 11.0f;
    private void Start()
    {
        //repeatedly spawns asteroids
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for(int i = 0; i < this.spawnAmount; i++)
        {

            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnLocation = this.transform.position + spawnDirection;

            //randomly rotates asteroids before spawning
            float variance = Random.Range(-this.tvariance, this.tvariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            
            Asteroid clone = Instantiate(this.asteroidPrefab, spawnLocation, rotation);
            clone.gameObject.SetActive(true);
            clone.size = Random.Range(clone.minSize, clone.maxSize);
            clone.SetTrajectory(rotation * -spawnDirection);

        }


    }


}
