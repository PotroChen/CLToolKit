using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂载目标：需要被放大缩小的gameobject
/// 
/// </summary>
public class MobileScaler : MonoBehaviour {

    public float minscale = 0.3f;
    public float maxscale = 2f;
	//记录两个手指的旧位置
	private Vector2 oposition1 = new Vector2();
	private Vector2 oposition2 = new Vector2();

    private bool isNewTouch = true;

	void Update () {
	    if (Input.touchCount >0)//多点触碰
		{
			if (Input.touchCount > 1)
			{
				//前两只手指触摸类型都为移动触摸
				if (Input.GetTouch(0).phase == TouchPhase.Moved||Input.GetTouch(1).phase == TouchPhase.Moved)
				{
					//计算出当前两点触摸点的位置
					var tempPosition1 = Input.GetTouch(0).position;
					var tempPosition2 = Input.GetTouch(1).position;

                    //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型  
                    float oldDistance;

                    //判断是否第一次触摸屏幕
                    if (isNewTouch)
                    {
                        oldDistance = Vector2.Distance(tempPosition1, tempPosition2);
                        isNewTouch = false;
                    }
                    else
                        oldDistance = Vector2.Distance(oposition1, oposition2);

					float newDistance = Vector2.Distance(tempPosition1, tempPosition2);

					if (Input.GetTouch(1).phase == TouchPhase.Began)
						return;

					//两个距离之差，为正表示放大手势， 为负表示缩小手势  
					float offset = newDistance - oldDistance;

					//放大因子， 一个像素按 0.01倍来算(100可调整)  
					float scaleFactor = offset / 100f;
					Vector3 localScale = transform.localScale;
					Vector3 scale = new Vector3(localScale.x + scaleFactor,
						localScale.y + scaleFactor,
						localScale.z + scaleFactor);

					//允许模型最小缩放到 0.3 倍最大放大2倍
					if (scale.x > minscale && scale.y > minscale && scale.z > minscale)
					{
						//实用差值运算，模型平滑缩放
						transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(Mathf.Clamp(localScale.x + scaleFactor, minscale, maxscale),
							Mathf.Clamp(localScale.y + scaleFactor, minscale, maxscale),
							Mathf.Clamp(localScale.z + scaleFactor, minscale, maxscale)), 1f);
					}

					oposition1 = tempPosition1;
					oposition2 = tempPosition2;
				}

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    isNewTouch = true;
			}
            

		}
	}
}
