using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHeight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Rotate(float num)
    {
        Vector3 pos = gameObject.transform.position;
        pos.y = num / 10;
        gameObject.transform.position = pos;
    }
}
