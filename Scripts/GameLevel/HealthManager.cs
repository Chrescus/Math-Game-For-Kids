using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    [SerializeField]
    private GameObject can1, can2, can3;    


   

    public void KalanHaklarýKontrolEt(int kalanHak)
    {
        switch (kalanHak)
        {
            case 3:
                can1.SetActive(true);
                can2.SetActive(true);
                can3.SetActive(true);
                break;

            case 2:
                can1.SetActive(true);
                can2.SetActive(true);
                can3.SetActive(false);
                break;

            case 1:
                can1.SetActive(true);
                can2.SetActive(false);
                can3.SetActive(false);
                break;

            case 0:
                can1.SetActive(false);
                can2.SetActive(false);
                can3.SetActive(false);
                break;
        }
    }

    
}
