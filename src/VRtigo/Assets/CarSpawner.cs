using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public float carSpeed = 2.0f;
    public float spawnCooldown = 3.5f;
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
            timeUntilSpawn = spawnCooldown;
        }
    }

    public void SpawnCar() 
    {
        GameObject car = Instantiate(carPrefabs[0], transform.position, Quaternion.identity) as GameObject;
        CarMovement component = car.GetComponent<CarMovement>();
        component.updateSpeed(carSpeed);
        component.updateDirection(carDirection);
        Destroy(car, despawnTime);
    }
}
