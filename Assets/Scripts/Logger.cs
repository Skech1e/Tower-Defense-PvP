using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
    public static Logger L { get; private set; }
    [SerializeField] bool enable_logs, enable_net_logs;
    private void Awake()
    {
        if (L == null)
            L = this;
        else
            Destroy(L);
    }

    public void Log(object message)
    {
        if (enable_logs)
            print(message);
    }

    public void LogNet(object message)
    {
        if(enable_net_logs)
            Debug.LogAssertion(message);
    }
}
