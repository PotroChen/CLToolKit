using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAndRotate: MonoBehaviour
{
    private Camera cameraCache;

    private Vector2 lastPos1 = new Vector2();
    private Vector2 lastPos2 = new Vector2();
    private float lastDistance;
    private Vector2 lastDirection;
    private bool isNewTouch = true;
    float angle;
    Vector3 axis;

    private void Start()
    {
        cameraCache = Camera.main;
    }

    void Update()
    {
        //Move
        #if !UNITY_EDITOR
        if(Input.touchCount == 1)
        {
            switch(Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    StartCoroutine(TouchMove());
                    break;
            }
        }
        #endif
        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(MouseMove());
        #endif

        //ZoomAndRotate
        if(Input.touchCount >1)
        {
            if(isNewTouch)
            {
                lastPos1 = Input.GetTouch(0).position;
                lastPos2 = Input.GetTouch(1).position;

                //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型 
                lastDistance = Vector2.Distance(lastPos1, lastPos2);
                lastDirection = (lastPos2 - lastPos1).normalized;
                isNewTouch = false;
            }
            if(Input.GetTouch(0).phase == TouchPhase.Moved||Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                //计算出当前两点触摸点的位置
                Vector2 tempPosition1 = Input.GetTouch(0).position;
                Vector2 tempPosition2 = Input.GetTouch(1).position;

                if (Input.GetTouch(1).phase == TouchPhase.Began)
                    return;

#region Zoom
                float newDistance = Vector2.Distance(tempPosition1, tempPosition2);
                //两个距离之差，为正表示放大手势， 为负表示缩小手势  
                float scaleOffset = newDistance - lastDistance;

                //放大因子， 一个像素按 0.01倍来算(100可调整)  
                float scaleFactor = scaleOffset / 100f;
                Vector3 localScale = transform.localScale;
                Vector3 scale = new Vector3(localScale.x + scaleFactor,
                                            localScale.y + scaleFactor,
                                            localScale.z + scaleFactor);

                //实用差值运算，模型平滑缩放
                if(transform.localScale.x>0.01)
                    transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(localScale.x + scaleFactor,
                                                                                          localScale.y + scaleFactor,
                                                                                          localScale.z + scaleFactor), 1f);
                else
                    transform.localScale = Vector3.one*0.011f;
#endregion

                Vector2 newDirection = (tempPosition2 - tempPosition1).normalized;
                axis =new Vector3() ;
                Quaternion.FromToRotation(lastDirection,newDirection).ToAngleAxis(out angle,out axis);
                transform.Rotate(axis,angle);
                 

                lastPos1 = tempPosition1;
                lastPos2 = tempPosition2;
                lastDistance = newDistance;
                lastDirection = newDirection;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(1).phase == TouchPhase.Ended)
                isNewTouch = true;
        }
	}

    IEnumerator MouseMove()
    {
        Vector3 lastMousePos = Input.mousePosition;
        Vector3 traScreenPos = cameraCache.WorldToScreenPoint(transform.position);
        Vector3 offset = traScreenPos - lastMousePos;
        while(Input.GetMouseButton(0))
        {
            Vector3 curTraScreenPos = Input.mousePosition+offset;
            transform.position = cameraCache.ScreenToWorldPoint(curTraScreenPos);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator TouchMove()
    {
        Vector3 lastMousePos = Input.GetTouch(0).position;
        Vector3 traScreenPos = cameraCache.WorldToScreenPoint(transform.position);
        Vector3 offset = traScreenPos - lastMousePos;

        while (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase != TouchPhase.Moved)
            {
                yield return new WaitForEndOfFrame();
                continue;
            }

            Vector3 curTraScreenPos = new Vector3(Input.GetTouch(0).position.x,Input.GetTouch(0).position.y,0) + offset;
            transform.position = cameraCache.ScreenToWorldPoint(curTraScreenPos);
            yield return new WaitForEndOfFrame();
        }
    }
}
