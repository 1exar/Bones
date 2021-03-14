using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControler : MonoBehaviour
{

    public Slider zoom;
    private float _startZoom;

    public void Start()
    {
        _startZoom = Camera.main.orthographicSize;
        startPos = transform.position;
    }

    private float lastValue = -1;
    private Vector3 startPos;
    
    
    public void ChangeCameraZoom()
    {
        Camera.main.orthographicSize = _startZoom - zoom.value;
        Vector2 pos = PointEditor.pe.activePoint.transform.position;
        
        float posToX = startPos.x - pos.x;
        float posToY = startPos.y - pos.y;

        if (lastValue < zoom.value)
        {
            transform.position = new Vector3(startPos.x - (posToX / 100) * zoom.value, startPos.y - (posToY / 100) * zoom.value,
                transform.position.z);
        }
        else
        {
            transform.position = new Vector3(startPos.x - (posToX / 100) * zoom.value, startPos.y - (posToY / 100) * zoom.value,
                transform.position.z);
        }
        
        lastValue = zoom.value;
    }

    public IEnumerator ZoomToPoint()
    {
        Vector2 pos = PointEditor.pe.activePoint.transform.position;
        yield return StartCoroutine(ZoomToPoint());
    }
    
}
