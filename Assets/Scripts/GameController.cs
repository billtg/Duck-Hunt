using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int targetFrameRate;
    public bool spawnDucks = true;
    public float duckSpawnX;
    public float duckTurnaround;

    public GameObject duckPrefab;
    public float baseTime;
    public float timeUntilDuck;
    public float baseTimeVar;
    private float lastDuckTime;


    public float minDuckSpeed;
    public float maxDuckSpeed;
    public float spawnHeight;
    public float smallAngle;
    public float highAngle;

    // Start is called before the first frame update
    void Start()
    {
        lastDuckTime = Time.time;
        Application.targetFrameRate = targetFrameRate;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInputs();
        if (spawnDucks)
            SpawnDucks();
    }

    private void CheckForInputs()
    {
        //Check fro Escape
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.X))
            spawnDucks = !spawnDucks;

    }

    private void SpawnDucks()
    {
        //Spawn a duck prefab with a random location, direction, and speed
        if (Time.time - lastDuckTime > timeUntilDuck)
        {
            //Random spawn
            Vector3 duckSpawn = new Vector3(Random.Range(-duckSpawnX,duckSpawnX), spawnHeight, 0);
            //Instantiate duck
            GameObject newDuck = Instantiate(duckPrefab, duckSpawn, Quaternion.identity);
            //Assign speed
            newDuck.GetComponent<DuckController>().flightSpeed = Random.Range(minDuckSpeed, maxDuckSpeed);
            
            //Assign direction
            Vector3 flightAngle = AssignDirection();
            //Edge ducks fly toward centre
            if (duckSpawn.x > duckTurnaround && flightAngle.x > 0)
                flightAngle = new Vector3(-flightAngle.x, flightAngle.y, 0);
            else if (duckSpawn.x < -duckTurnaround && flightAngle.x < 0)
                flightAngle = new Vector3(-flightAngle.x, flightAngle.y, 0);
            newDuck.GetComponent<DuckController>().flightDirection = flightAngle;



            //Flip left moving sprites
            if (flightAngle.x < 0)
                newDuck.transform.localScale = new Vector3(-newDuck.transform.localScale.x,newDuck.transform.localScale.y,1);
            //Update low/high flying sprites
            if (Mathf.Rad2Deg*Mathf.Asin(flightAngle.y) < smallAngle)
                newDuck.GetComponent<DuckController>().UpdateSprite(true);
            else if (Mathf.Rad2Deg * Mathf.Asin(flightAngle.y) > highAngle)
                newDuck.GetComponent<DuckController>().UpdateSprite(false);

            //Reset timing
            lastDuckTime = Time.time;
            timeUntilDuck = baseTime + Random.Range(-baseTimeVar, baseTimeVar);
        }

                //GameObject newItem = Instantiate(prefab, new Vector3(10f, spawnHeight + height, 0), Quaternion.identity)
    }

    public Vector3 AssignDirection()
    {
        //Pick a random angle between max and min, then turn it into a normalized Vector3 and return it
        float flightAngle = Random.Range(-75f, 75f);
        Vector3 flightVector = new Vector3(Mathf.Sin(Mathf.Deg2Rad * flightAngle), Mathf.Cos(Mathf.Deg2Rad*flightAngle), 0);
        //Vector3 randomVector = Random.insideUnitCircle;
        flightVector = flightVector.normalized;
        return flightVector;
    }
}
