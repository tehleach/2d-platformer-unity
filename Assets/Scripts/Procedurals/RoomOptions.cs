using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOptions {
    private const int DEFAULT_LAYER_HEIGHT = 5;
    
    public Vector2 dimensions;
    public PlatformTypes platformTypes;
    public bool platformsHaveGaps;
    public int platformLayers;
    public EnemySpawnDistributions enemySpawnDistributions;

    public RoomOptions(Vector2 dimensions, EnemySpawnDistributions enemySpawnDistributions, PlatformTypes platformTypes, bool platformsHaveGaps, int platformLayers = -1) {
        this.dimensions = dimensions;
        this.enemySpawnDistributions = enemySpawnDistributions;
        this.platformTypes = platformTypes;
        this.platformsHaveGaps = platformsHaveGaps;
        this.platformLayers = platformLayers >= 0 ? platformLayers : getDefaultPlatformLayers();
    }

    private int getDefaultPlatformLayers() {
        return (int)dimensions.y / DEFAULT_LAYER_HEIGHT;
    }

    public int PlatformHeight {
        get { return (int)dimensions.y / platformLayers; }
    }
}
