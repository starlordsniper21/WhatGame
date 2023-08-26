using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bgScroll : MonoBehaviour
{

    private void Update()
    {
        transform.position += new Vector3(-50 * Time.deltaTime, 0);
        if (transform.position.x < -2978.676)
        {
            transform.position = new Vector3(2978.676f, transform.position.y);
        }
    }
}
