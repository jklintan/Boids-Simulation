using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boid : MonoBehaviour{

    //Each boid needs, a direction, a velocity, a sphere to check other flockmates (radius and angle)
    private float speed = 2.0f;
    public Vector3 velocity;
    public Vector3 position;
    public int radius;
    public int angle;
    private boid[] neighbours;

    void start(){
        //velocity = transform.forward;
    }

    void update(){
        Vector3 n = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f));
        //this.transform.Translate(n*Time.deltaTime);
        //Separation
        //Alignment
        //Cohesion

        //transform.position += transform.forward * speed * Time.deltaTime;

    }

    boid[] getNeighbours(){
        return neighbours;
    }


}
