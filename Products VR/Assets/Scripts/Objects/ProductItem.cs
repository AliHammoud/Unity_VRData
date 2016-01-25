using UnityEngine;
using System.Collections;

public class ProductItem : MonoBehaviour {

	public int productId;
	public string productName;
	public int horizontalFacing;
	public int verticalFacing;
	public int width;
	public int height;
	public int depth;
	public string image;
	public string color;

	public Vector3 bounds;

	public float positionX, positionY;

	public GameObject productParent, productShelf;

	// Use this for initialization
	void Start () {
		//transform.localPosition = productShelf.transform.position;

		this.transform.localScale = new Vector3 (width/100.0f, height/100.0f, depth/100.0f);
		//Debug.Log ("Shelf position : " + productShelf.transform.position);
		bounds = GetComponent<BoxCollider> ().bounds.extents;
		string color2 = color.Remove(0,1);
		gameObject.GetComponent<Renderer> ().material.color = HexToColor (color2);
	}
	
	Color HexToColor(string hex){
		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r,g,b, 255);
	}

	// Update is called once per frame
	void Update () {

	}
}
