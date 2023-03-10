using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownWinter : MonoBehaviour
{
    public GameObject Option1;
    public GameObject Option2;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Dropdown(int value)
    {
        if (value == 0)
        {
            Option1.SetActive(false);
            Option2.SetActive(false);

        }
        if (value == 1)
        {
            Option2.SetActive(true);
            Option2.SetActive(false);

        }
        if (value == 2)
        {
            Option1.SetActive(false);
            Option2.SetActive(true);


        }
       
    }
}
