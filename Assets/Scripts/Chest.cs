using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator chestAnim;
    [SerializeField]
    GameObject reward;
    private bool opened = false;
    // Start is called before the first frame update
    void Start()
    {
        chestAnim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D()
    {
        if(!opened)
        {
            opened=true;
            chestAnim.SetTrigger("Open");
            Instantiate(reward, this.transform.position + Vector3.up*1.5f, this.transform.rotation);
        }
        
    }
}
