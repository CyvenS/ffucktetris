using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] Tetriminos;
    public float MoveFre = 0.8f;
    private float passedTime = 0;
    int height = 20;
    int width = 10;

    public GameObject CurrentTEt;
    public GameObject blockPrefab;
    public GameObject[,] grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = new GameObject[width,height];
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
        int index = UnityEngine.Random.Range(0, Tetriminos.Length);
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
                BlockSet();
                SpawnTetrimino();
                

            }
        }
    }
    bool IsValidPosition()
    {
        return GetComponent<GridScript>().IsValidPosition(CurrentTEt.transform);
    }

    void BlockSet()
    {
        int blocksize = 4;
        if (CurrentTEt.name == "Stairs")
        {
            blocksize = 7;
        }
        GameObject[] block = new GameObject[blocksize];

        for (int i = 0; i < blocksize; i++)
        {
            
            Debug.Log(CurrentTEt.name);
            block[i] = CurrentTEt.transform.GetChild(i).gameObject;


            Vector3 pos;
            pos = new Vector3(block[i].transform.position.x, block[i].transform.position.y, block[i].transform.position.z);
            GameObject newBlock = Instantiate(blockPrefab, pos, Quaternion.identity);
            newBlock.GetComponent<SpriteRenderer>().color = block[i].GetComponent<SpriteRenderer>().color;

            int x = (int)Math.Round(block[i].transform.position.x);
            int y = (int)Math.Round(block[i].transform.position.y);

            grid[x,y] = newBlock;
        }
        

    }
    void CheckForLines()
    {
        for (int y = 0; y < height; y++)
        {
            if (IsLineFull(y))
            {
                ClearLine(y);
                ShiftRowsDown(y);
            }
        }
    }
    bool IsLineFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
            {
                return false; //full row
            }
        }
        return true; //not full
    }
    void ClearLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
        }
    }
    void ShiftRowsDown(int clearRow)
    {
        for (int y = clearRow; y < height - 1; y++)
        {
            for (int x = 0; x < width; x++)
            {
                grid[x, y] = grid[x, y + 1];
                if (grid[x, y] != null)
                {
                    grid[x, y].transform.position += Vector3.down;
                }
                grid[x, y + 1] = null;
            }
        }

    }
}

