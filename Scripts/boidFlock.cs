using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidFlock : MonoBehaviour{

    //This script is used to simulate the boids flocking behaviour
    //Alignment, separation and cohesion

    public int numbOfBoids = 5; //Set the number of boids objects simulated
    public boid[] dataBoids;
    public GameObject[] theBoids;
    public float radius;

    private float speed = 2.0f;
    

    //Set the object to be used as boids
    public GameObject boidPrefab;

    // Start is called before the first frame update
    void Start(){

        //Setup arrays to assembly the data about the boids
        theBoids = new GameObject[numbOfBoids];
        dataBoids = new boid[numbOfBoids];
        
        //initialize the boids
        for (int i = 0; i < numbOfBoids; i++) {
            
            //Make a random position for each boid to start at, (OBS inside spawn radius)
            Vector3 newPosition = random_vector();

            //Instantiate the boids as game objects and add to array, also convert to boids and store
            GameObject b = Instantiate(boidPrefab, newPosition, Quaternion.identity) as GameObject;
            b.gameObject.name = "Boid " + (i+1);
            theBoids[i] = b;
            boid boid = b.GetComponent(typeof(boid)) as boid;
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
            dataBoids[i].transform.position += transform.forward * speed * Time.deltaTime;
            //dataBoids[i].transform.position += transform.forward * speed * Time.deltaTime;

        }

    }

    void initializeBoids(){

    }

    //Generate a random vector inside a specific radius
    Vector3 random_vector(){
        Vector3 rand = new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(-4.5f, 4.5f), Random.Range(-4.5f, 4.5f));
        return rand;
    }

}
