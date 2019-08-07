using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Must be first in script execution order. */
public class Loader : MonoBehaviour
{
    public GameManager gameManager;

    private void Awake()
    {
        //Spawn gameManager
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
    }
}
