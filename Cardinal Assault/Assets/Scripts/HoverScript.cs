using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverScript : MonoBehaviour
{
    public bool isHover = false;
    private float scale = 1;
    public float scaleMax = 1.1f;
    public float timeIn = 1f;
    public float timeOut = 2f;
    private RectTransform rectTransform;

    public void Start()
    {
        rectTransform = this.gameObject.GetComponent<RectTransform>();
    }

    public void Update()
    {
        float delta = Time.unscaledDeltaTime;

        // Hover animation
        
        if (isHover && scale < scaleMax)
        {
            scale += timeIn * delta;
            if (scale > scaleMax)
            {
                scale = scaleMax;
            }
        }
        else if (!isHover && scale > 1f)
        {
            scale -= timeOut * delta;
            if (scale < 1f)
            {
                scale = 1f;
            }
            
        }
        rectTransform.localScale = new Vector3(scale, scale, 1);
    }

    public void OnHoverStart()
    {
        isHover = true;
    }

    public void OnHoverStop()
    {
        isHover = false;
    }

    public void OnDisable()
    {
        scale = 1f;
        isHover = false;
        rectTransform.localScale = new Vector3(scale, scale, 1);
    }
}

