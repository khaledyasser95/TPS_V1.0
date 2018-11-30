using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ML_Manager  {

    private GameObject gameObject ;

    private static ML_Manager m_Instance;
    
    public static ML_Manager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new ML_Manager();
                
                m_Instance.gameObject = GameObject.Find("Player");
               
            }
            return m_Instance;
        }
    }

    private static ML_Manager m_Instance2;

    public static ML_Manager Instance2
    {
        get
        {
            if (m_Instance2 == null)
            {
                m_Instance2 = new ML_Manager();

                m_Instance2.gameObject = GameObject.Find("Agent");
            }
            return m_Instance2;
        }
    }

    private Player_ML m_Player_ML;
    public Player_ML playerInput
    {
        get
        {
            if (m_Player_ML == null)
            {
                m_Player_ML = gameObject.GetComponent<Player_ML>();
            }
            return m_Player_ML;
        }
    }
}
