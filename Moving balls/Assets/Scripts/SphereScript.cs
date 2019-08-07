﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SphereScript : MonoBehaviour
{

	private bool isTouched = false;
	public SphereType ColorType;
	private Rigidbody RB;
	public float ForceMultiplier = 25;

	public SizeType CurrentSize = SizeType.Small;


	private void Awake()
	{
		RB = GetComponent<Rigidbody>();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetMouseButtonUp(0))
		{
			
		}
	}


	private void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "Sphere")
		{
			SphereScript sphereScript = collision.collider.GetComponent<SphereScript>();
			if(GameManagerScript.Instance.CurrentSphere == this && sphereScript.ColorType == ColorType)
			{

				CurrentSize+= (int)collision.gameObject.GetComponent<SphereScript>().CurrentSize;
				Destroy(collision.gameObject);


				transform.localScale = Vector3.one;

				transform.localScale *= (int)CurrentSize;
			}
		}
	}


	private void OnMouseDown()
	{
		Debug.Log("Down");
		isTouched = true;
		GameManagerScript.Instance.CurrentSphere = this;
	}


	private void OnMouseDrag()
	{
		if (isTouched)
        {
            Debug.Log("Up");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane p = new Plane(Vector3.up, Vector3.zero);
            float dist = 0;
            p.Raycast(ray, out dist);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 30);
            RaycastHit hit;
            List<RaycastHit> res = Physics.RaycastAll(ray, 100).Where(r => r.collider.tag == "Plane").ToList();



			if (res.Count > 0)
            {

				float x = Mathf.Abs(transform.position.x - res[0].point.x);
                float z = Mathf.Abs(transform.position.z - res[0].point.z);

                Debug.Log(x);
                Debug.Log(z);
				if((x > 0.5f || z > 0.5f))
				{
					isTouched = false;
                    RB.AddForce((res[0].point - transform.position).normalized * ForceMultiplier, ForceMode.Impulse);
				}
            }
        }
	}

}



public enum SphereType
{
	Red,
    Yellow,
    Green,
    Blue
}



public enum SizeType
{
	Small = 1,
    Med,
    Large
}