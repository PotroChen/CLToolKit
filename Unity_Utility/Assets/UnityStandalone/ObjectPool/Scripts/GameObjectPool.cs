using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool :MonoBehaviour{

    private Stack<GameObject> goStack;
    private GameObject prototype;
    private int MaxCount;
    private bool isFixedSize;

    public Action<GameObject> onGet;
    public Action<GameObject> onRecycle;
    public Action<GameObject> onNew;

    public static GameObjectPool CreateGameObjectPool(GameObject prototype, int MaxCount = 0, Transform parent = null, bool isFixedSize = false)
    {
        GameObject go = new GameObject();
        GameObjectPool pool = go.AddComponent<GameObjectPool>();
        pool.Init(prototype, MaxCount, parent, isFixedSize);
        pool.WarmUp();

        return pool;
    }

    public static GameObjectPool CreateGameObjectPoolSmoothly(GameObject prototype, int MaxCount = 0, Transform parent = null, bool isFixedSize = false)
    {
        GameObject go = new GameObject();
        GameObjectPool pool = go.AddComponent<GameObjectPool>();
        pool.Init(prototype, MaxCount, parent, isFixedSize);
        pool.WarmUpSmoothly();

        return pool;
    }

    private void Init(GameObject prototype, int MaxCount = 0, Transform parent = null, bool isFixedSize  = false)
    {
        goStack = new Stack<GameObject>();
        gameObject.transform.SetParent(parent);
        this.prototype = prototype;
        this.MaxCount = MaxCount;
        this.isFixedSize = isFixedSize;

        UpdatePoolInfo();
    }

    public GameObject Get()
    {
        GameObject go = Pop();

        if (onGet != null)
            onGet(go);

        UpdatePoolInfo();

        return go;
    }

    public void Recycle(GameObject go)
    {
        Push(go);

        if (onRecycle != null)
            onRecycle(go);

        UpdatePoolInfo();
    }

    protected GameObject Pop()
    {
        GameObject go = null;
        if(goStack.Count!=0)
            go = goStack.Pop();

        if (go == null)
        {
            if (!isFixedSize)
            {
                go = GameObject.Instantiate(prototype);
                if (onNew != null)
                    onNew(go);
            }
            else
            {
                Debug.LogWarning("The [{0}Pool] is empty!");
                return go;
            }
        }
        GameObjectInit(go);
        return go;
    }

    protected void Push(GameObject go)
    {
        if (goStack.Count >= MaxCount)
        {
            GameObject.Destroy(go);
            return;
        }
        goStack.Push(go);
        go.SetActive(false);
        go.transform.SetParent(gameObject.transform);
    }

    protected void WarmUp()
    {
        for (int i = 0; i < MaxCount;i++)
        {
            GameObject go = GameObject.Instantiate(prototype.gameObject);
            Push(go);
        }
    }

    protected void WarmUpSmoothly()
    {
        StartCoroutine(WarmingUp());
    }

    protected IEnumerator WarmingUp()
    {
        for (int i = 0; i < MaxCount; i++)
        {
            GameObject go = GameObject.Instantiate(prototype.gameObject);
            Push(go);
            yield return new WaitForEndOfFrame();
        }
    }

    protected void GameObjectInit(GameObject go)
    {
        go.transform.localScale = Vector3.one;
        go.transform.rotation = Quaternion.identity;
        go.transform.position = Vector3.zero;
        go.transform.SetParent(null);
        go.SetActive(true);
    }

    private void UpdatePoolInfo()
    {
        int currentNum = goStack.Count;
        int totalNum = MaxCount;
        gameObject.name = String.Format("[{0}Pool]:{1} /{2}", prototype.name, currentNum, totalNum);
    }
}
