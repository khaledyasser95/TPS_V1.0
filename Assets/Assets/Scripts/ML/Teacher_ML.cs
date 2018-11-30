using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher_ML : ML_Manager {

	
    private GameObject gameObject;

    private static Teacher_ML m_Instance;

    public static Teacher_ML Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new Teacher_ML();

                 m_Instance.gameObject = GameObject.Find("Player");
                
            }
            return m_Instance;
        }
    }
}
