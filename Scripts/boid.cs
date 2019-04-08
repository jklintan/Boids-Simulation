using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boid : MonoBehaviour{

    //Each boid needs, a direction, a velocity, a sphere to check other flockmates (radius and angle)
    public float speed;
    public Vector3 direction;
    public Vector3 position;
    public int radius;
    public int angle;

    ////Constructor
    //boid(){
    //    direction = new Vector3(0, 0, 0);
    //    position = new Vector3(0, 0, 0);
    //}

}
