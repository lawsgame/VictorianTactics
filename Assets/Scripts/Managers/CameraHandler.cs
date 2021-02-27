using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler 
{
    private static readonly object padlock = new object();
    private static CameraHandler instance;

    private Camera _camera;
    
    private CameraHandler() { }
    
    public static CameraHandler Instance
    {
        get
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new CameraHandler();
                }
            }
            return instance;
        }
    }


}