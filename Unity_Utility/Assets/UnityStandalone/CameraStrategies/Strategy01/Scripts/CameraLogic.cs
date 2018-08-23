using UnityEngine;
using System.Collections;

//TODOList
//2.改变focus点时，应该不瞬间改变，而是过度过去
//3.提供瞬间改变的方法。
[ExecuteInEditMode]
public class CameraLogic : MonoBehaviour
{
    public Transform StageCam { get; private set; }

    [Header("位置相关属性:")]

    [Tooltip("圆心")]
    public Transform center;
    [Tooltip("水平方向角度")]
    [Range(0f,360f)]
    public float horizontalAngle = 209f;//水平旋转角度
    [Tooltip("垂直方向角度")]
    [Range(-90f, 90f)]
    public float verticalAngle = 14f;//垂直旋转角度
    [Tooltip("旋转半径")]
    public float sphereRadius = 43f;//半径


    private float targetRadius;//特写时与目标位置的距离
    private float radiusCache;

    private Vector3 centerDes;
    private Vector3 focusPosCache;

    private void Awake()
    {
        StageCam = transform;
    }

    private void LateUpdate()
    {
        UpdateCameraPos(horizontalAngle, verticalAngle, sphereRadius);
    }


    private void UpdateCameraPos(float horiAngle, float vertiAngle, float radius)
    {
        float poxY = radius * Mathf.Sin(vertiAngle * Mathf.Deg2Rad);
        float horiRadius = Mathf.Sqrt(radius * radius - poxY * poxY);
        float posX = horiRadius * Mathf.Sin(horiAngle * Mathf.Deg2Rad);//计算x位置
        float posZ = horiRadius * Mathf.Cos(horiAngle * Mathf.Deg2Rad);//计算y位置,因为forward再左侧
        StageCam.position = new Vector3(posX, poxY, posZ) + center.position;
        StageCam.LookAt(center.position);
    }
}

