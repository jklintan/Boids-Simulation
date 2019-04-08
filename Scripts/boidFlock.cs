using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidFlock : MonoBehaviour{

    //This script is used to simulate the boids flocking behaviour
    //Alignment, separation and cohesion

    public int numbOfBoids = 5; //Set the number of boids objects simulated
    public boid[] dataBoids;
    public GameObject[] theBoids;
    private float radius = 10.0f;

    private List<boid> neighbors;

    private float speed = 10.0f;

    public float cohesionFactor = 0.3f;
    public float separationFactor = 0.5f;
    public float alignmentFactor = 0.3f;

    //Set the object to be used as boids
    public GameObject boidPrefab;

    // Start is called before the first frame update
    void Start(){

        //Setup arrays to assembly the data about the boids
        theBoids = new GameObject[numbOfBoids];
        dataBoids = new boid[numbOfBoids];

        //initialize the boids
        boidPrefab.gameObject.name = "Boid " + 1;
        theBoids[0] = boidPrefab;
        boid boid = theBoids[0].GetComponent(typeof(boid)) as boid;
        dataBoids[0] = boid;
        for (int i = 1; i < numbOfBoids; i++) {
            
            //Make a random position for each boid to start at, (OBS inside spawn radius)
            Vector3 newPosition = random_vector();

            //Instantiate the boids as game objects and add to array, also convert to boids and store
            GameObject b = Instantiate(boidPrefab, newPosition, Quaternion.identity) as GameObject;
            b.gameObject.name = "Boid " + (i+1);
            theBoids[i] = b;
            boid = b.GetComponent(typeof(boid)) as boid;
            dataBoids[i] = boid;
        }


       

    }

    // Update is called once per frame
    void Update(){
        //var currentPosition = transform.position;
        //var currentRotation = transform.rotation;
        //var velocity = (1, 1, 1);
        //// Moves forawrd.

        //boid[] boids = FindObjectsOfType(typeof(boid)) as boid[];

        //Update the positions for every boid
        for (int i = 0; i < numbOfBoids; i++){
            //Calculate separation
            //Vector3 separation = Separation(dataBoids[i]);
            Vector3 cohesion = Cohesion(dataBoids[i]);
            //Calculate alignment
            Vector3 alignment = Alignment(dataBoids[i]);
            //Calculate cohesion
            dataBoids[i].transform.position += (alignment + cohesion ) * speed * Time.deltaTime;
            //dataBoids[i].transform.position += alignment * speed * Time.deltaTime;
        }

    }

    //Rule #1, steer to avoid crowding local flockmates
    Vector3 Separation(boid b){
        Vector3 velocity = new Vector3(0, 0, 0);
        getNeighbours(b);
        for(int i = 0; i < neighbors.Count; i++){
            velocity += neighbors[i].transform.position;
        }
        velocity /= neighbors.Count;

        return velocity;
    }

    //Rule #2, boids try to keep a small distance away from other objects (including other boids)
    //Steer toward the average heading of local flockmates
    Vector3 Alignment(boid b){
        Vector3 c = new Vector3(0, 0, 0);
        for (int i = 0; i < numbOfBoids; i++){
            float distance = Vector3.Distance(transform.position, dataBoids[i].transform.position);
            Vector3 otherHeading = dataBoids[i].transform.forward;
            c = Vector3.Slerp(c, otherHeading, (1.0f / (distance * distance)));
            //    boid comparingBoid = dataBoids[i];
            //    if (comparingBoid != b)
            //    {
            //        if (Vector3.Distance(comparingBoid.transform.position, b.transform.position) < 100)
            //        {
            //            c = c - (b.transform.position - comparingBoid.transform.position);
            //        }
            //    }
        }
        //print(c);

        return c;
    }

    //Rule #3, boids try to fly towards the centre of mass of neighbouring boids
    Vector3 Cohesion(boid b)
    {
        Vector3 percievedCentre = new Vector3(0, 0, 0);
        for (int i = 0; i < numbOfBoids; i++)
        {
            boid comparingBoid = dataBoids[i];
            if (comparingBoid != b)
            {
                //If a neighbouring boid
                if (Vector3.Distance(comparingBoid.transform.position, b.transform.position) < radius)
                {
                    percievedCentre = percievedCentre + comparingBoid.transform.position;
                }
            }
        }
        percievedCentre = percievedCentre / (numbOfBoids - 1);
        return (percievedCentre - b.transform.position) / 100;
    }

    private float abs(Vector3 vector3)
    {
        throw new NotImplementedException();
    }

    void initializeBoids(){

    }

    void getNeighbours(boid b){
        neighbors.Clear();
        neighbors = new List<boid>();
        for(int i = 0; i < numbOfBoids; i++) {
            if (Vector3.Distance(dataBoids[i].transform.position, b.transform.position) < radius){
                neighbors.Add(dataBoids[i]);
            }
        }
    }

    //Generate a random vector inside a specific radius
    Vector3 random_vector(){
        Vector3 rand = new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), UnityEngine.Random.Range(-4.5f, 4.5f), UnityEngine.Random.Range(-4.5f, 4.5f));
        return rand;
    }

}
