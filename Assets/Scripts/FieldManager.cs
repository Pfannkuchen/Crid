using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : SingletonMonoBehaviour<FieldManager>
{   
    [SerializeField] GameObject[] disableAfterSetup;

    public bool IsServer { get; private set; }
    bool started = false;

    Block[,] gridBlocks;

    GridElement[,] grid;
    [SerializeField] GameObject gridPrefab;

    void Update()
    {
        if (!started)
        {
            return;
        }

        if (!IsServer)
        {
            return;
        }

        updateGrid();
    }

    public void setServer(bool b)
    {
        IsServer = b;

        for (int i = 0; i < disableAfterSetup.Length; i++)
        {
            disableAfterSetup[i].SetActive(false);
        }

        if (IsServer)
        {
            createGrid();
        }

        started = true;
    }

    void createGrid()
    {
        Debug.Log("Create Grid");
        /* 
        grid = new GridElement[16, 9];

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                GameObject go = Instantiate(gridPrefab, new Vector3((float)x - 7.5f, 0f, (float)y - 4f), Quaternion.identity);
                grid[x, y] = go.GetComponent<GridElement>();
                go.transform.SetParent(this.transform);
            }
        }
        */


        // fluent grid creator test
        gridBlocks = new GridCreator()
            .create(16,9)
			.randomWalls(1f)
            .definePath(2f)
            .grab();
    }

    void updateGrid()
    {
        Debug.Log("Update Grid");
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                grid[x, y].update(PlayerCursor.allPlayerCursorPositions());
            }
        }
    }

    public static Vector3 GetTouchPosition()
    {
        Vector3 touchPosition;

        // if we are using a smart device and at least one touch action is currently registered
        if (Input.touchCount > 0 && (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer))
        {
            touchPosition = Input.GetTouch(0).position;
        }
        // fallback to mouse position
        else
        {
            touchPosition = Input.mousePosition;
        }

        touchPosition.x /= Screen.width;
        touchPosition.x *= 16f;
        touchPosition.x -= 8f;
        touchPosition.x = Mathf.Clamp(touchPosition.x, -8f, 8f);

        touchPosition.y /= Screen.height;
        touchPosition.y *= 9f;
        touchPosition.y -= 4.5f;
        touchPosition.y = Mathf.Clamp(touchPosition.y, -4.5f, 4.5f);

        touchPosition.z = touchPosition.y;
        touchPosition.y = 0f;

        return touchPosition;
    }
}
