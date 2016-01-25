using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FixtureItem : MonoBehaviour {

	public int id;
	public int z;
	public float width, height, depth;
	public int categoryId;
	public string categoryName;
	public float shelveHeightOffset = 0;

	//List of Shelves
	public List<GameObject> shelves = new List<GameObject>();

	public GameObject left, right, back, shelvesParent;

	void Start(){
		left.transform.localScale = new Vector3 (Manager.instance.thickness, height / 100.0f, depth / 100.0f);
		left.transform.localPosition = new Vector3 (-Manager.instance.thickness / 2.0f, 0, left.transform.localPosition.z);
		right.transform.localScale = new Vector3 (Manager.instance.thickness, height / 100.0f, depth / 100.0f);
		right.transform.localPosition = new Vector3 (width / 100.0f + Manager.instance.thickness/2.0f, 0, 0);
		back.transform.localScale = new Vector3 (width / 100.0f, height / 100.0f, Manager.instance.thickness);
		back.transform.localPosition = new Vector3 ((width/100.0f)/2.0f, 0.0f, (depth/100.0f)/2);
	}
}
