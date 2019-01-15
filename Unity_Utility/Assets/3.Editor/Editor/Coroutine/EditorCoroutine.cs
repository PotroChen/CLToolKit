using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

// from https://gist.github.com/benblo/10732554
// and modified to fit into this project
public class EditorCoroutine
{
    public static EditorCoroutine Start( IEnumerator _routine )
    {
        EditorCoroutine coroutine = new EditorCoroutine(_routine);
        coroutine.Start();
        return coroutine;
    }

    readonly IEnumerator routine;
    DateTime endTime = DateTime.MinValue;
    
    EditorCoroutine( IEnumerator _routine )
    {
        routine = _routine;
    }

    void Start()
    {
        //Debug.Log("start");
        EditorApplication.update += update;
    }
    public void stop()
    {
        //Debug.Log("stop");
        EditorApplication.update -= update;
    }

    void update()
    {
        /* NOTE: no need to try/catch MoveNext,
         * if an IEnumerator throws its next iteration returns false.
         * Also, Unity probably catches when calling EditorApplication.update.
         */

        //Debug.Log("update");
        //if (!routine.MoveNext())
        //{
        //    stop();
        //}

        ProcessCoroutine(this);
    }

    void ProcessCoroutine(EditorCoroutine item)
    {
        IEnumerator ie = item.routine;
        if (item.endTime < DateTime.Now)
        {
            if (ie.MoveNext())
            {
                if (ie.Current is WaitForSecondsInEditor)
                {
                    item.endTime = DateTime.Now.AddSeconds(((WaitForSecondsInEditor)(ie.Current)).seconds);
                }
            }
            else
            {
                stop();
            }
        }
    }
}