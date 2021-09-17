using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicSight : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D coll) {
    	if (GetComponentInParent<Mimic>().Touched){
    		if (coll.CompareTag("Player")){
				GetComponentInParent<Mimic>().player = coll.transform;
			}
    	} 
	}

	private void OnTriggerStay2D(Collider2D coll) {
        if (GetComponentInParent<Mimic>().Touched){
    		if (coll.CompareTag("Player")){
				GetComponentInParent<Mimic>().player = coll.transform;
			}
    	}
    }

	private void OnTriggerExit2D(Collider2D coll) {
        GetComponentInParent<Mimic>().player = null;
    }
}
