using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSound : MonoBehaviour
{
    public static MainSound instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
                Destroy(this.gameObject);
        }
    }
}
