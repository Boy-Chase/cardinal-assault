using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    #region singleton
    public static CameraEffects Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    #endregion singleton

    /*------   Tilt Variables   ------*/
    private float effectDuration;
    [SerializeField] private float maxDuration = 2;
    [SerializeField] private float maxRotation = 10;
    private bool movingAway = true;
    private int direction = 1;
    /*--------------------------------*/
    /*------   Zoom Variables   ------*/
    [SerializeField] private float maxZoom = 2;
    private float zoomTimePassed;
    private float maxZoomTime;
    private bool zoom;
    private float initialCamScale;
    private int zoomDirection = 1;
    /*--------------------------------*/
    /*----   Duration Variables   ----*/
    private float timePassed;
    const float FPS = 60;
    /*--------------------------------*/
    /*------   Misc Variables   ------*/
    [SerializeField] private GameObject hurtOverlay;
    /*--------------------------------*/
    void Start()
    {
        initialCamScale = GetComponent<Camera>().orthographicSize;
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        effectDuration += Time.deltaTime;
        zoomTimePassed += Time.deltaTime;
        if (timePassed < 1 / FPS) return;

        Tilt();
        if (zoom) Zoom();
        timePassed = 0;
    }

    private void Tilt()
    {
        if (movingAway) transform.rotation = quaternion.Euler(0, 0, direction * ((effectDuration / maxDuration) * maxRotation) * Mathf.Deg2Rad);
        else transform.rotation = quaternion.Euler(0, 0, (direction * maxRotation - (direction * ((effectDuration / maxDuration) * maxRotation))) * Mathf.Deg2Rad);

        if (effectDuration >= maxDuration)
        {
            effectDuration = 0;
            if (movingAway) movingAway = false;
            else
            {
                direction = direction * -1;
                movingAway = true;
            }
        }
    }

    private void Zoom()
    {
        if (zoomTimePassed >= maxZoomTime) {
            zoom = false;
            zoomDirection = zoomDirection * -1;
        }
        else {
            if (zoomDirection == 1) GetComponent<Camera>().orthographicSize = initialCamScale - maxZoom * (zoomTimePassed / maxZoomTime);
            else GetComponent<Camera>().orthographicSize = initialCamScale - (maxZoom * (1-(zoomTimePassed / maxZoomTime)));
        }
    }
    public void BeginZoom() 
    { 
        zoom = true;
        hurtOverlay.SetActive(false);
        maxZoomTime = LevelManager.Instance.beatSpeed / 5;
        zoomTimePassed = 0;
    }

    public void Hurt()
    {
        //GetComponent<Camera>().orthographicSize = initialCamScale + 2;
        hurtOverlay.SetActive(true);
        //zoom = false;
    }
}
