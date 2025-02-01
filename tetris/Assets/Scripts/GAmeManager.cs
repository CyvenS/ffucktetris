using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] Tetriminos;
    public float MoveFre = 0.8f;
    private float passedTime = 0;
    private GameObject CurrentTEt;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTetrimino();
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;
        if (passedTime >= MoveFre)
        {
            passedTime -= MoveFre;
            MoveTEtrimino(Vector3.down);
        }
        UserInput();
    }
    void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveTEtrimino(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveTEtrimino(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CurrentTEt.transform.Rotate(0, 0, 90);
            if (!IsValidPosition())
            {
                CurrentTEt.transform.Rotate(0, 0, -90);
                // transform.RotateAround(transform.TransformPoint(rotationP), new Vector3(0, 0, 1), 90);
                //if (!vMove())
                // transform.RotateAround(rotationP, new Vector3(0, 0, 1), -90);
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveFre = 0.4f;

        }
        else
        {
            MoveFre = 0.8f;
        }
    }
    void SpawnTetrimino()
    {
        int index = Random.Range(0, Tetriminos.Length);
        CurrentTEt = Instantiate(Tetriminos[index], new Vector3(5, 19, 0), Quaternion.identity);
    }

    void MoveTEtrimino(Vector3 dircetion)
    {
        CurrentTEt.transform.position += dircetion;
        if (!IsValidPosition())
        {
            CurrentTEt.transform.position -= dircetion;
            if (dircetion == Vector3.down)
            {
                GetComponent<GridScript>().UpdateGrid(CurrentTEt.transform);
                CheckForLines();
                SpawnTetrimino();

            }
        }
    }
    bool IsValidPosition()
    {
        return GetComponent<GridScript>().IsValidPosition(CurrentTEt.transform);
    }
    void CheckForLines()
    {

    }
}

