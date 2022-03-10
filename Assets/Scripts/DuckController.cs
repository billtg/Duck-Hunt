using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckController : MonoBehaviour
{
    public float flightSpeed = 0.5f;
    public Vector3 flightDirection;

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckForLimit();
    }

    private void Move()
    {
        //move the duck
        //this.gameObject.transform.position += new Vector3(0,flightSpeed,0);
        this.gameObject.transform.position += flightDirection * flightSpeed;
    }

    private void CheckForLimit()
    {
        if (this.gameObject.transform.position.y >= 8) Destroy(this.gameObject);
    }

    public void UpdateSprite(bool lowAngle)
    {
        if (lowAngle)
            this.gameObject.GetComponent<Animator>().SetBool("lowFlight", true);
        else
            this.gameObject.GetComponent<Animator>().SetBool("highFlight", true);
    }
}
