using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour {
    GameObjectPool pool;
    // Use this for initialization
    void Start () {
        GameObject shere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        pool = GameObjectPool.CreateGameObjectPool(shere, 10,null,true);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullect = pool.Get();
            Shoot(bullect);

        }
	}

    public void Shoot(GameObject bullet)
    {
        StartCoroutine(Shooting(bullet));
    }
    
    IEnumerator Shooting(GameObject bullet)
    {
        float time = 0;
        while (time < 10f)
        {
            bullet.transform.Translate(transform.forward * 10 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }
        pool.Recycle(bullet);//Destroy(bullet);
    }
}
