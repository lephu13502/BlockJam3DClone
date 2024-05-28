using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager
{
    static MyGrid[] grids;
    static int level = 1;
    static int playingLevel = 1;

    static public int Level
    {
        get { return level; }
    }

    public static Color WallColor = new Color(0.6352941f, 1f, 0.627451f);
    public static Color CylinderColor = new Color(0.772549f, 0.7921569f, 0.9019608f);

    public static void NextLevel()
    {
        level++;
        if (level <= 6) // Update this to reflect the new total number of levels
        {
            playingLevel = level;
        }
        else playingLevel = Random.Range(1, 4); // Update the range if you want random levels after all have been played
    }

    public static MyGrid DesignLevel()
    {
        if (grids == null) InitGrid();

        ObjectManager.Instance.DeallocateObjects<Character>();
        ObjectManager.Instance.DeallocateObjects<CharacterSpawner>();
        ObjectManager.Instance.DeallocateObjects<Component>();

        ResetGrid();
        switch (playingLevel)
        {
            case 1:
                Level1(grids[playingLevel - 1]);
                break;
            case 2:
                Level2(grids[playingLevel - 1]);
                break;
            case 3:
                Level3(grids[playingLevel - 1]);
                break;
            // Add new cases for the additional levels
            case 4:
                Level4(grids[playingLevel - 1]);
                break;
            case 5:
                Level5(grids[playingLevel - 1]);
                break;
            case 6:
                Level6(grids[playingLevel - 1]);
                break;
            default:
                break;
        }

        StackManager.Instance.CreateStack();
        ObjectManager.Instance.CheckObjects(grids[playingLevel - 1]);
        return grids[playingLevel - 1];
    }
    static void Level1(MyGrid grid)
    {
        // Create the walls around the grid
        PutWallsToAround(grid);

        // Place blue characters in a horizontal line in the middle
        for (int x = 2; x <= 4; x++)
        {
            ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(x, 2), Color.blue);
        }
        //Place yello characters in a horizontal line in the middle
        for (int x = 2; x <= 4; x++)
        {
            ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(x, 1), Color.yellow);
        }
    }
    static void Level2(MyGrid grid)
    {
        int x, y;


        PutWallsToAround(grid);
        y = (int)grid.Size.y - 2;

        //first row
        x = 0;
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), Color.blue).InBarrier = false;
        ObjectManager.Instance.AllocateObject<Component>(grid.GetNode(++x, y), WallColor);
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), Color.yellow).InBarrier = false;
        ObjectManager.Instance.AllocateObject<Component>(grid.GetNode(++x, y), WallColor);
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), Color.red).InBarrier = false;

        //second row
        x = 0;
        y--;
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), Color.blue).InBarrier = false;
        ObjectManager.Instance.AllocateObject<Component>(grid.GetNode(++x, y), WallColor);
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), Color.yellow).InBarrier = false;
        ObjectManager.Instance.AllocateObject<Component>(grid.GetNode(++x, y), WallColor);
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), Color.red).InBarrier = false;

        //second row
        x = 0;
        y--;
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), Color.blue).InBarrier = false;
        ObjectManager.Instance.AllocateObject<Component>(grid.GetNode(++x, y), WallColor);
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), Color.yellow).InBarrier = false;
        ObjectManager.Instance.AllocateObject<Component>(grid.GetNode(++x, y), WallColor);
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), Color.red).InBarrier = false;
    }

    static void Level3(MyGrid grid)
    {

        PutWallsToAround(grid);

        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(1, 2), Color.red);
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(2, 2), Color.yellow);
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(3, 2), Color.red);
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(1, 1), Color.yellow);
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(2, 1), Color.red);
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(3, 1), Color.yellow);
    }

    static void Level4(MyGrid grid)
    {
        int x, y;
        List<Color> colors;

        colors = new List<Color>();
        FillColorList(colors, Color.red, Color.yellow, Color.green, Color.blue);

        PutWallsToAround(grid);
        y = (int)grid.Size.y - 2;

        //first row
        x = 0;
        ObjectManager.Instance.AllocateObject<Component>(grid.GetNode(++x, y), WallColor);
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors));
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors));
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors));
        ObjectManager.Instance.AllocateObject<Component>(grid.GetNode(++x, y), WallColor);

        //second row
        x = 1;
        y--;
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors));
        ObjectManager.Instance.AllocateObject<CharacterSpawner>(grid.GetNode(++x, y), CylinderColor).Allocate(grid.GetNode(x, y - 1),
            GetColor(colors), GetColor(colors), GetColor(colors), GetColor(colors));
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors));

        //third row
        x = 1;
        y--;
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors));
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors));
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors));
    }

    static void Level5(MyGrid grid)
    {
        int x, y;
        List<Color> colors;

        colors = new List<Color>();
        FillColorList(colors, Color.red, Color.yellow, Color.blue, Color.green);

        PutWallsToAround(grid);
        y = (int)grid.Size.y - 2;

        //first row
        x = 0;
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors)).InBarrier = true;
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors)).InBarrier = true;
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors)).InBarrier = true;
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors)).InBarrier = true;

        //second row
        x = 0;
        y--;
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors)).InBarrier = true;
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors)).InBarrier = true;
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors)).InBarrier = true;
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors)).InBarrier = true;

        //third row
        x = 0;
        y--;
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors));
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors));
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors));
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors));
    }

    static void Level6(MyGrid grid)
    {
        int x, y;
        List<Color> colors;

        colors = new List<Color>();
        FillColorList(colors, Color.red, Color.red, Color.yellow, Color.yellow, Color.cyan, Color.blue, Color.blue, Color.cyan);

        PutWallsToAround(grid);
        y = (int)grid.Size.y - 2;

        //first row
        x = 0;
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors));
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors));
        ObjectManager.Instance.AllocateObject<Component>(grid.GetNode(++x, y), WallColor);
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors));
        ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(++x, y), GetColor(colors));

        //other rows
        for (int i = 0; i < 4; i++)
        {
            y--;
            for (x = 1; x <= 5; x++)
                ObjectManager.Instance.AllocateObject<Character>(grid.GetNode(x, y), GetColor(colors));
        }
    }

    static void PutWallsToAround(MyGrid grid)
    {
        int y;

        for (int i = 0; i < grid.Size.x; i++)
        {
            y = (int)grid.Size.y - 1;
            for (int j = y; j >= 0; j--)
            {
                if (i == 0 || i == grid.Size.x - 1 || j == grid.Size.y - 1)
                    ObjectManager.Instance.AllocateObject<Component>(grid.GetNode(i, j), WallColor);
            }
        }
    }

    static void FillColorList(List<Color> colorList, params Color[] colors)
    {
        foreach (var color in colors)
        {
            for (int i = 0; i < 3; i++) colorList.Add(color);
        }
    }

    static Color GetColor(List<Color> colors)
    {
        Color color;

        color = colors[Random.Range(0, colors.Count)];
        colors.Remove(color);
        return color;
    }

    static void InitGrid()
    {
        Transform transform;

        transform = GameObject.Find("GameManager").transform;
        grids = new MyGrid[6];
        grids[0] = MyGrid.CreateNewGrid(new Vector2(7, 5), transform.position); // Level 1: 7x5 grid
        grids[1] = MyGrid.CreateNewGrid(new Vector2(7, 5), transform.position); // Level 2: 7x5 grid
        grids[2] = MyGrid.CreateNewGrid(new Vector2(5, 4), transform.position); // Level 3: 5x4 grid
        grids[3] = MyGrid.CreateNewGrid(new Vector2(7, 5), transform.position);
        grids[4] = MyGrid.CreateNewGrid(new Vector2(6, 5), transform.position);
        grids[5] = MyGrid.CreateNewGrid(new Vector2(7, 7), transform.position);
    }

    static void ResetGrid()
    {
        foreach (MyGrid grid in grids)
        {
            for (int x = 0; x < grid.Size.x; x++)
            {
                for (int y = 0; y < grid.Size.y; y++) grid.GetNode(x, y).walkable = true;
            }
        }
    }

    public static MyGrid GetCurrentGrid()
    {
        return grids[playingLevel - 1];
    }
}
