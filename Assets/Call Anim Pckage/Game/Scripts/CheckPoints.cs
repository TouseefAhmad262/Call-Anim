using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public bool Checked;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < transform.parent.transform.childCount; i++)
            {
                transform.parent.transform.GetChild(i).gameObject.GetComponent<CheckPoints>().Checked = false;
            }
            Checked = true;
        }
    }
}