using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : SingletonComponent<BoardManager> {

    public Vector2Int bottomLeftCorner = new Vector2Int(0, 0);
    public Vector2Int topRightCorner = new Vector2Int(5, 5);

    [SerializeField] Wrapper wrapperPrefab;
    [SerializeField] Transform wrappersHolder;

    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] Transform obstaclesHolder;

    [SerializeField] Treasure treasurePrefab;
    [SerializeField] Transform treasureHolder;

    [SerializeField] Enemy enemyPrefab;
    [SerializeField] Transform enemiesHolder;

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RefreshBoard();
        }	
	}

    public void RefreshBoard()
    {
        //Destroy previous wrappers
        foreach (Transform child in wrappersHolder)
        {
            Destroy(child.gameObject);
        }

        //Init Vertical wrappers
        int leftColumnX = bottomLeftCorner.x - 1;
        int rightColumnX = topRightCorner.x + 1;
        for (int y = bottomLeftCorner.y; y <= topRightCorner.y; y++)
        {
            var leftWrapper = Instantiate(wrapperPrefab, wrappersHolder);
            var rightWrapper = Instantiate(wrapperPrefab, wrappersHolder);
            leftWrapper.transform.position = new Vector3(leftColumnX, y, 0);
            rightWrapper.transform.position = new Vector3(rightColumnX, y, 0);
            leftWrapper.linkedWrapper = rightWrapper;
            rightWrapper.linkedWrapper = leftWrapper;
        }

        //Init Horizontal wrappers
        int bottomRowY = bottomLeftCorner.y - 1;
        int topRowY = topRightCorner.y + 1;
        for (int x = bottomLeftCorner.x; x <= topRightCorner.x; x++)
        {
            var bottomWrapper = Instantiate(wrapperPrefab, wrappersHolder);
            var topWrapper = Instantiate(wrapperPrefab, wrappersHolder);
            bottomWrapper.transform.position = new Vector3(x, bottomRowY, 0);
            topWrapper.transform.position = new Vector3(x, topRowY, 0);
            bottomWrapper.linkedWrapper = topWrapper;
            topWrapper.linkedWrapper = bottomWrapper;
        }

        bool treasureSpawned = false;
        bool enemySpawned = false;

        while (treasureSpawned == false || enemySpawned == false)
        {
            //Destroy previous treasures
            foreach (Transform child in treasureHolder)
            {
                Destroy(child.gameObject);
            }

            //Destroy previous obstacles
            foreach (Transform child in obstaclesHolder)
            {
                Destroy(child.gameObject);
            }

            //Destroy previous enemies
            foreach (Transform child in enemiesHolder)
            {
                Destroy(child.gameObject);
            }

            //Init random obstacles
            for (int x = bottomLeftCorner.x; x <= topRightCorner.x; x++)
            {
                for (int y = bottomLeftCorner.y; y <= topRightCorner.y; y++)
                {
                    if (treasureSpawned == false && Random.Range(0, 25) == 1)
                    {
                        var newTreasure = Instantiate(treasurePrefab, treasureHolder);
                        newTreasure.transform.position = new Vector3(x, y, 0);
                        treasureSpawned = true;
                    }
                    else if (enemySpawned == false && Random.Range(0, 10) == 1)
                    {
                        var newEnemy = Instantiate(enemyPrefab, enemiesHolder);
                        newEnemy.transform.position = new Vector3(x, y, 0);
                        enemySpawned = true;
                    }
                    else if (Random.Range(0, 5) == 1)
                    {
                        var newObstacle = Instantiate(obstaclePrefab, obstaclesHolder);
                        newObstacle.transform.position = new Vector3(x, y, 0);
                    }

                }
            }
        }
    }
}
