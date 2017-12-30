using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

    [SerializeField]
    private GameObject ObstaclePrefab;
    [SerializeField]
    private Camera cam;
    private float nextSpawnY = 0; 
    private float prefabSize;
    [SerializeField]
    private float MinObstacleDistancee;
    [SerializeField]
    private float MaxObstacleDistancee;

    [SerializeField]
    private float TargetRangeLeft;
    [SerializeField]
    private float TargetRangeRight;
    // Use this for initialization
    void Start () {
        nextSpawnY = 5;
        prefabSize = ObstaclePrefab.transform.localScale.x * ObstaclePrefab.GetComponent<BoxCollider2D>().size.x;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.GameStarted)
        {
            if (cam.transform.position.y >= nextSpawnY - 10) {
                SpanwObstacle();
                nextSpawnY += Random.Range(MinObstacleDistancee, MaxObstacleDistancee);
            }
        }
	}

    private void SpanwObstacle()
    {
        //bool left = Random.Range(0, 2) == 1;
        float SpawnX;

        SpawnX = Random.Range(TargetRangeLeft, TargetRangeRight);

        //SpawnX = -CamBounds.OrthographicBounds(cam).extents.x + prefabSize/2 ;

        Instantiate(ObstaclePrefab, new Vector3(SpawnX, nextSpawnY,0),Quaternion.identity);
         

    }
}
