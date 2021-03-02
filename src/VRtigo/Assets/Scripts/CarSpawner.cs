using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public float carSpeed = 2.0f;
    public float minSpawnCooldown = 3.5f;
    public float maxSpawnCooldown = 9.0f;

    public float despawnTime = 3.0f;
    public Vector3 carDirection = new Vector3(1, 0, 0);
    private float timeUntilSpawn = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0) {
            SpawnCar();
            timeUntilSpawn = Random.Range(minSpawnCooldown, maxSpawnCooldown);
        }
    }

    public void SpawnCar() 
    {
        GameObject car = Instantiate(carPrefabs[0], transform.position, Quaternion.identity) as GameObject;
        CarMovement component = car.GetComponent<CarMovement>();
        component.UpdateSpeed(carSpeed);
        component.UpdateDirection(carDirection);
        Destroy(car, despawnTime);
    }
}
