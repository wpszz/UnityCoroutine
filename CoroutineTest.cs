using UnityEngine;
using System;
using System.Collections;

public class CoroutineTest : MonoBehaviour
{
    IEnumerator m_testIter;

    class MyWaitForSeconds
    {
        public float seconds;

        public MyWaitForSeconds(float seconds)
        {
            this.seconds = seconds;
        }
    }

    private void Awake()
    {
        m_testIter = Coroutine01();
        m_testIter.MoveNext();
    }

    private void Update()
    {
        if (m_testIter != null)
        {
            object current = m_testIter.Current;
            if (current is MyWaitForSeconds)
            {
                MyWaitForSeconds myWait = (MyWaitForSeconds)current;
                myWait.seconds -= Time.deltaTime;
                if (myWait.seconds > 0)
                    return;
            }

            if (!m_testIter.MoveNext())
                m_testIter = null;
        }
    }

    IEnumerator Coroutine01()
    {
        Debug.LogError("Coroutine01 yield return new MyWaitForSeconds(2)");
        yield return new MyWaitForSeconds(2);

        for (int i = 0; i < 10; i++)
        {
            Debug.LogError("Coroutine01 loop: " + i);
            yield return new MyWaitForSeconds(1);
        }

        Debug.LogError("Coroutine01 yield return new WaitForSeconds(2)");
        yield return new WaitForSeconds(2);

        Debug.LogError("Coroutine01 yield return null");
        yield return null;

        Debug.LogError("Coroutine01 yield return new WaitForEndOfFrame()");
        yield return new WaitForEndOfFrame();

        Debug.LogError("Coroutine01 yield return null");
        yield return null;

        Debug.LogError("Coroutine01 yield break");
        yield break;
    }
}
