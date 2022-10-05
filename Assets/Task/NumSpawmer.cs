using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumSpawmer : MonoBehaviour
{
    public GameObject numPrefab;
    public Transform wordCanvas;

    public WordDisplay SpawnNum()
    {
        Vector3 randamPosition = new Vector3(Random.Range(-2.5f, 2.5f), 7f);
        GameObject numObj = Instantiate(numPrefab,randamPosition, Quaternion.identity ,wordCanvas);
        WordDisplay numDisplay = numObj.GetComponent<WordDisplay>();

        return numDisplay;
    }

    
}
