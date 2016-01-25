using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProductGroupItem : MonoBehaviour{
	public int productId;
	public string productName;
	public int horizontalFacing;
	public int verticalFacing;
	public float width;
	public float height;
	public float XSpacing, YSpacing;

	public ShelfItem shelf;

	public List<GameObject> groupedProducts = new List<GameObject>();
	
	// Use this for initialization
	void Start () {
		PositionItems ();
	}

	public void PositionItems(){
		int i = 0;

		for (int x = 0; x < horizontalFacing; x++) {
			for(int y = 0; y < verticalFacing; y++){
				ProductItem p = groupedProducts[i].GetComponent<ProductItem>();
				XSpacing = (p.width/100.0f) + 0.005f;
				YSpacing = (p.height/100.0f) + 0.002f;

				groupedProducts[i].transform.localPosition = new Vector3(XSpacing * x + (p.width/100.0f /2), YSpacing * y, 0.0f);
				i++;
			}
		}

		width = XSpacing * horizontalFacing;
		height = YSpacing * verticalFacing;

		shelf.PositionItems ();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
