using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public GameObject platform;
    public GameObject enemy;
    public float width = 25;
    public float height = 25;

    private Vector2 originPosition;

    // Use this for initialization
    void Start() {
        originPosition = transform.position;
        BuildRoom(new RoomOptions(
         new Vector2(width, height),
         EnemySpawnDistributions.TOP,
         PlatformTypes.FLOOR,
         true   
        ));
    }

    void BuildRoom(RoomOptions options) {
        BuildOuterBounds(options);
        BuildPlatforms(options);
        SpawnEnemies(options);
    }

    void BuildOuterBounds(RoomOptions options) {
        BuildFloor(originPosition, options.dimensions.x);
        BuildFloor(new Vector2(originPosition.x, originPosition.y + options.dimensions.y), options.dimensions.x);
        BuildWall(originPosition, options.dimensions.y);
        BuildWall(new Vector2(originPosition.x + options.dimensions.x, originPosition.y), options.dimensions.y);
    }

    void BuildPlatforms(RoomOptions options) {
        if (options.platformTypes == PlatformTypes.FLOOR) {
            int platformSpacing = (int)height / options.platformLayers;
            for (int i = platformSpacing; i < (int)height; i += platformSpacing) {
                BuildFloor(new Vector2(originPosition.x, originPosition.y + i), options.dimensions.x, true);
            }
        }
    }

    void SpawnEnemies(RoomOptions options) {
        if(options.enemySpawnDistributions == EnemySpawnDistributions.TOP) {
            Vector2 enemyPos = new Vector2(originPosition.x + 1, originPosition.y + (options.PlatformHeight * (options.platformLayers - 1)));
            enemyPos = new Vector2(originPosition.x + 4, originPosition.y + (options.PlatformHeight * (options.platformLayers - 1)));
            Instantiate(enemy, enemyPos, Quaternion.identity);
            enemyPos = new Vector2(originPosition.x + 7, originPosition.y + (options.PlatformHeight * (options.platformLayers - 1)));
            Instantiate(enemy, enemyPos, Quaternion.identity);
            enemyPos = new Vector2(originPosition.x + 10, originPosition.y + (options.PlatformHeight * (options.platformLayers - 1)));
            Instantiate(enemy, enemyPos, Quaternion.identity);
            enemyPos = new Vector2(originPosition.x + 13, originPosition.y + (options.PlatformHeight * (options.platformLayers - 1)));
            Instantiate(enemy, enemyPos, Quaternion.identity);
            enemyPos = new Vector2(originPosition.x + 16, originPosition.y + (options.PlatformHeight * (options.platformLayers - 1)));
            Instantiate(enemy, enemyPos, Quaternion.identity);
            enemyPos = new Vector2(originPosition.x + 19, originPosition.y + (options.PlatformHeight * (options.platformLayers - 1)));
            Instantiate(enemy, enemyPos, Quaternion.identity);
            enemyPos = new Vector2(originPosition.x + 22, originPosition.y + (options.PlatformHeight * (options.platformLayers - 1)));
            Instantiate(enemy, enemyPos, Quaternion.identity);
        }
    }

    void BuildFloor(Vector2 position, float width, bool gaps = false) {
        float curX = position.x;
        float y = position.y;
        //TODO: configurable
        List<int> gapLocations = new List<int>();
        int gapMaxWidth = 4;
        int gapCurWidth = 0;
        if (gaps) {
            switch(Random.Range(0,4)) {
                case 0:
                case 1:
                case 2:
                    gapLocations.Add(Random.Range(0, (int)width - gapMaxWidth));
                    break;
                case 3:
                    gapLocations.Add(Random.Range(0, (int)width - gapMaxWidth));
                    gapLocations.Add(Random.Range(0, (int)width - gapMaxWidth));
                    break;
            }
        }
        bool onGap = false;
        while (Mathf.Abs(position.x - curX) <= width) {
            if (!gaps) {
                Instantiate(platform, new Vector2(curX, y), Quaternion.identity);
            } else if(!onGap) {
                Instantiate(platform, new Vector2(curX, y), Quaternion.identity);
                onGap = gapLocations.Contains((int)curX);
            } else if(gapCurWidth++ == gapMaxWidth) {
                onGap = false;
                gapCurWidth = 0;
            }
            curX += platform.GetComponent<BoxCollider2D>().size.x;
        }
    }

    void BuildWall(Vector2 position, float height) {
        float curY = position.y;
        float x = position.x;
        while (Mathf.Abs(position.y - curY) <= height) {
            Instantiate(platform, new Vector2(x, curY), Quaternion.identity);
            curY += platform.GetComponent<BoxCollider2D>().size.x;
        }
    }
}
