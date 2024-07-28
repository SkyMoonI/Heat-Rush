using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooler : MonoBehaviour
{
	float coolingValue = 5f;


	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Player")
		{
			HeatController.Instance.Heat -= coolingValue;
			Destroy(gameObject);
		}
	}
}
