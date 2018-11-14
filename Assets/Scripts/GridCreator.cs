using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator
{

    enum BlockHandle
    {
        Empty,
        WallSimple,
        Spawn,
        Waypoint
    }
    BlockHandle[,] blockHandles;

    /// Setup for the grid.
    public GridCreator create(int width, int height)
    {
        blockHandles = new BlockHandle[width, height];
        for (int x = 0; x < blockHandles.GetLength(0); x++)
        {
            for (int y = 0; y < blockHandles.GetLength(1); y++)
            {
                blockHandles[x, y] = BlockHandle.Empty;
            }
        }

        return this;
    }

    public GridCreator randomWalls(float amount)
    {
        for (int x = 0; x < blockHandles.GetLength(0); x++)
        {
            for (int y = 0; y < blockHandles.GetLength(1); y++)
            {
                if (Random.Range(0f, 1f) < amount)
                    blockHandles[x, y] = BlockHandle.WallSimple;
            }
        }

        return this;
    }

    public GridCreator definePath(float directness)
    {

        int y = 0;

        for (int x = 0; x < blockHandles.GetLength(0); x++)
        {
            if (x == 0)
            {
                y = Random.Range(0, blockHandles.GetLength(1));
                blockHandles[x, y] = BlockHandle.Spawn;
            }
            else if (x == blockHandles.GetLength(0) / 2)
            {
                blockHandles[x, y] = BlockHandle.Spawn;
            }
            else if (x == blockHandles.GetLength(0) - 1)
            {
                blockHandles[x, y] = BlockHandle.Spawn;
            }
            else
            {
                blockHandles[x, y] = BlockHandle.Empty;

                float noise = directness;
                while (noise > 0f)
                {
                    if (Random.Range(0f, 1f) < noise)
                    {
                        if (Random.Range(0f, 100f) > 50f)
                        {
                            y++;
                        }
                        else
                        {
                            y--;
                        }
                    }

                    if (y < 0)
                    {
                        y = 0;
                        noise = 0f;
                    }
                    if (y >= blockHandles.GetLength(1))
                    {
                        y = blockHandles.GetLength(1) - 1;
                        noise = 0f;
                    }
                    blockHandles[x, y] = BlockHandle.Empty;

                    noise -= Random.Range(0f, 1f);
                }
            }
        }

        return this;
    }

    /// Returns the finished grid which is made of Blocks.
    public Block[,] grab()
    {
        Block[,] grid = new Block[blockHandles.GetLength(0), blockHandles.GetLength(1)];
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                Debug.Log(x + " " + y + " " + blockHandles[x, y]);
                grid[x, y] = CreateBlock(ToPrefab(blockHandles[x, y]), x, y);
            }
        }

        return grid;
    }

    // ----------------
    // HELPER FUNCTIONS
    // ----------------

    /// Converts a BlockHandle to a Block prefab.
    static GameObject ToPrefab(BlockHandle blockHandle)
    {
        switch (blockHandle)
        {
            case BlockHandle.WallSimple: return Prefabs.Instance.wallSimple;
            case BlockHandle.Spawn: return Prefabs.Instance.spawn;
            default: return Prefabs.Instance.empty;
        }
    }

    /// Creates a Block from a Block prefab.
    static Block CreateBlock(GameObject prefab, int x, int y)
    {
        GameObject go = GameObject.Instantiate(prefab, new Vector3((float)x - 7.5f, 0f, (float)y - 4f), Quaternion.identity);

        return go.GetComponent<Block>();
    }
}
