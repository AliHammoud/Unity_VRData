using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShelfItem : MonoBehaviour {

	public int id;
	public int shelfId;
	public int positionId;
	public float positionY;
	public int width, height, depth;


	float a;
	//List of Products
	public List<GameObject> productGroups = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}

	public void PositionShelf(){
		this.transform.localScale = new Vector3 (width/100.0f, Manager.instance.thickness, depth/100.0f);

		if (id > 0) {
			float pY = ((positionY/100.0f) + (Manager.instance.thickness * (id+1))) -1;
			//float pY2 = Manager.instance.thickness + (positionY/100.0f) -1;
			//Debug.Log("pY : " + pY);
			//Debug.Log("Shelf : " + gameObject.name + " id : " + id + " y : " + pY);
			this.transform.localPosition = new Vector3 ((width/2) / 100.0f, pY, 0);
		} else {
			//Debug.Log("Shelf : " + gameObject.name + " id : " + id + " second");
			this.transform.localPosition = new Vector3 ((width/2) / 100.0f, (Manager.instance.thickness/2.0f + (positionY/100.0f) -1), 0);
		}
	}

	public void PositionItems(){

		a = 0.0f;

		for (int i = 0; i< productGroups.Count; i++) {

			productGroups[i].transform.localPosition = 
				new Vector3 (a + (transform.localPosition.x - (width/2) / 100.0f + Manager.instance.thickness/2.0f),                                
				             transform.localPosition.y + (productGroups[i].GetComponent<ProductGroupItem>().YSpacing/2 + Manager.instance.thickness/2.0f),
				             	transform.localPosition.z);

			a += productGroups[i].GetComponent<ProductGroupItem>().width + Manager.instance.thickness/2.0f;
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
