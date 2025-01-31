using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockMove : MonoBehaviour
{
    private float pTime;
    public float fallTimev = 0.8f;
    public static int height = 20;
    public static int width = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Position track
        Vector3 position = transform.position;
        Debug.Log("Object Position: " + position); //mainly debugging tracks the position of the block, may use for grid clearing and line detection

        if(position.x > 7.49)
        {
            transform.position -= new Vector3(1, 0, 0);
            if(position.y > -1)
            {
                transform.position -= new Vector3(1, 0, 0);
                
            }
        }
        if (position.y == -1.00)
        {
            transform.position += new Vector3(0, 1, 0);
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (vMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            //if (vMove())
           // {
               // transform.position -= new Vector3(1, 0, 0);  //redundant
               // Debug.Log("Past");
            //}
        }
        if(Time.time - pTime > fallTimev)
        {
            transform.position += new Vector3(0, -1, 0);
            pTime = Time.time;
            //if (vMove())
            //{
               // transform.position -= new Vector3(1, 0, 0); //redundant
               // Debug.Log("Past");
           // }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            fallTimev = 0.05f;
        }

        else
        {
            fallTimev = 0.8f;
        }
        }
    bool vMove() //This is redundant it can be replaced with the reversed code of lines 26-28, It is already replaced for right and down just not left so this block of code is needed till its changed 
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position. x);
            int roundedY = Mathf.RoundToInt(children.transform.position. y);
            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }
        }
        return true;
    }

    }

         