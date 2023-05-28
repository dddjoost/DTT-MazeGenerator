using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.MazeGenerators;
using UnityEngine;

public class CameraMaanger : MonoBehaviour
{
    [SerializeField]
    public Camera camera;
    void Start()
    {
        MazeGeneration.ChangeMapSize.AddListener(ChangeCameraPosition);
    }

    private void ChangeCameraPosition(int width, int height, float cellWidth, float cellHeight)
    {
        transform.position = new Vector3((width * cellWidth) / 2f, 10, (height * cellHeight) / 2f - (cellHeight / 2));
        transform.rotation = Quaternion.Euler(90,0,0);
        
        Vector2 size = new Vector2((width * cellWidth), (height * cellHeight));
        float aspect = Screen.width / (float)Screen.height; // not using cam.aspect because it is not always updated immediately
        float heightOfCamera = size.x / aspect;
        if (height < size.y) heightOfCamera = size.y;
        camera.orthographicSize = heightOfCamera / 2 + 10;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
