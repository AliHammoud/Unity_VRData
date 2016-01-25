using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class Manager : MonoBehaviour {

	public static Manager instance;

	//Fixture Variables
	public float thickness = 0.02f;

	public List<GameObject> FixturesList = new List<GameObject> ();


	public string URL = "https://www.euriskomobility.me/vr/products.php";
	string JSONString;

	public GameObject fixturePrefab,shelfPrefab,productPrefab, productGroupPrefab;

	// Use this for initialization
	IEnumerator Start () {
		instance = this;
		yield return StartCoroutine("GetProducts");
	}

	IEnumerator GetProducts(){
		WWW www = new WWW(URL);
		yield return www;
		if (www.error != null){
			Debug.Log("Error");
		}
		else{
			JSONString = www.text;
			JSONParse();
		}
	}

	void JSONParse(){
		var N = JSONNode.Parse(JSONString);
		Debug.Log (JSONString);
		for(int i = 0; i< N.Count; i++){
		//Fixture

			Fixture f = new Fixture();
			f.z = int.Parse(N[i]["z"]);
			f.width = int.Parse(N[i]["width"]);
			f.height = int.Parse(N[i]["height"]);
			f.depth = int.Parse(N[i]["depth"]);
			f.categoryId = int.Parse(N[i]["categoryId"]);
			f.categoryName = N[i]["categoryName"];

			GameObject fixtureObject = Instantiate(fixturePrefab, this.transform.position, this.transform.rotation) as GameObject;


			FixtureItem fI = fixtureObject.GetComponent<FixtureItem>();
			fI.categoryId = f.categoryId;
			fI.width = f.width;
			fI.height = f.height;
			fI.depth = f.depth;
			fI.categoryName = f.categoryName;
			fI.z = f.z;
			fI.id = i;
			FixturesList.Add(fixtureObject);

			//Shelves
			var shelves = N[i]["shelfs"];

			for(int j = 0; j< shelves.Count; j++){
				Shelf s = new Shelf();
				s.height = int.Parse(N[i]["shelfs"][j]["height"]);
				s.shelfId = int.Parse(N[i]["shelfs"][j]["shelfId"]);

				GameObject shelfObject = Instantiate(shelfPrefab, this.transform.position, this.transform.rotation) as GameObject;
				fI.shelves.Add(shelfObject);
				shelfObject.transform.SetParent(fI.shelvesParent.transform);
				ShelfItem sI = shelfObject.transform.FindChild("Shelf").gameObject.GetComponent<ShelfItem>();

				sI.width = f.width;
				sI.depth = f.depth;
				sI.height = s.height;
				sI.positionId = j;
				sI.shelfId = s.shelfId;
				sI.id = j;

				if(j>0){
					fI.shelveHeightOffset += fI.shelves[j-1].gameObject.transform.FindChild("Shelf").gameObject.GetComponent<ShelfItem>().height + thickness;
					sI.positionY = fI.shelveHeightOffset;
				}

				sI.PositionShelf();

				//Products
				var products = N[i]["shelfs"][j]["products"];

				//Product Groups
				for(int p = 0 ; p < products.Count; p++){

					ProductGroup prodGroup = new ProductGroup();
					prodGroup.productId = int.Parse(N[i]["shelfs"][j]["products"][p]["productId"]);
					prodGroup.productName = N[i]["shelfs"][j]["products"][p]["productName"];
					prodGroup.horizontalFacing = int.Parse(N[i]["shelfs"][j]["products"][p]["horizontalFacing"]);
					prodGroup.verticalFacing = int.Parse(N[i]["shelfs"][j]["products"][p]["verticalFacing"]);

					GameObject productGroup = Instantiate(productGroupPrefab, sI.transform.position, sI.transform.rotation) as GameObject;
					sI.productGroups.Add(productGroup);
					productGroup.transform.SetParent(sI.transform.parent);
					productGroup.name = "PG " + p;
					ProductGroupItem pgI = productGroup.GetComponent<ProductGroupItem>();

					pgI.productId = prodGroup.productId;
					pgI.horizontalFacing = prodGroup.horizontalFacing;
					pgI.verticalFacing = prodGroup.verticalFacing;
					pgI.productName = prodGroup.productName;
					pgI.shelf = sI.GetComponent<ShelfItem>();

					//Products
					int productsNumber = pgI.horizontalFacing * pgI.verticalFacing;
					for(int pp = 0; pp < productsNumber; pp++){
						Product prod = new Product();
						prod.productId = int.Parse(N[i]["shelfs"][j]["products"][p]["productId"]);
						prod.productName = N[i]["shelfs"][j]["products"][p]["productName"];
						prod.width = int.Parse(N[i]["shelfs"][j]["products"][p]["width"]);
						prod.height = int.Parse(N[i]["shelfs"][j]["products"][p]["height"]);
						prod.depth = int.Parse(N[i]["shelfs"][j]["products"][p]["depth"]);
						prod.image = N[i]["shelfs"][j]["products"][p]["image"];
						prod.color = N[i]["shelfs"][j]["products"][p]["color"];

						GameObject productObject = Instantiate(productPrefab, sI.transform.position, sI.transform.rotation) as GameObject;
						pgI.groupedProducts.Add(productObject);
						productObject.transform.SetParent(productGroup.transform);
						
						ProductItem pI = productObject.GetComponent<ProductItem>();

						pI.name = "product_" + pp.ToString() + "_shelf_" + p;
						pI.productId = prod.productId;
						pI.image = prod.image;
						pI.color = prod.color;
						pI.productName = prod.productName;
						pI.productParent = sI.transform.parent.gameObject;
						pI.productShelf = sI.gameObject;
						pI.width = prod.width;
						pI.height = prod.height;
						pI.depth = prod.depth;

						//Add VRData functionality
						#region VRData_init

						//make sure the vr data kit is set up
						pI.gameObject.AddComponent <VRInteractiveItem> ();
						pI.gameObject.AddComponent <InteractiveSceneItem> ();

						//get interactive shelf item script
						InteractiveSceneItem shelfItemIA = pI.gameObject.GetComponent <InteractiveSceneItem> ();

						//fill interactive shelf item params
						VRInteractiveItem vrItem = pI.gameObject.GetComponent <VRInteractiveItem> ();
						Renderer itemRenderer = pI.gameObject.GetComponent <Renderer> ();

						shelfItemIA.m_InteractiveItem = vrItem;
						shelfItemIA.m_Renderer = itemRenderer;

						#endregion VRData_init

					}
				}
				//End of Products
			}
			//End of Shelves

		//End of Fixture

			//Cap Shelve
			GameObject shelfObjectCap = Instantiate(shelfPrefab, this.transform.position, this.transform.rotation) as GameObject;
			fI.shelves.Add(shelfObjectCap);
			shelfObjectCap.transform.SetParent(fI.shelvesParent.transform);
			ShelfItem sII = shelfObjectCap.transform.FindChild("Shelf").gameObject.GetComponent<ShelfItem>();
			sII.width = f.width;
			sII.depth = f.depth;
			shelfObjectCap.gameObject.name = "Cap";
			sII.PositionShelf();
			shelfObjectCap.transform.position = new Vector3(0.0f, fI.height/100.0f - (thickness), sII.transform.position.z);
		}
		PositionFixtures ();
	}
	public bool IsOdd(int value)
	{
		return value % 2 != 0;
	}

	void PositionFixtures(){
		int a = 1;
		float fixturesX = 0;
		float fixturesZ = 0;

		foreach (GameObject f in FixturesList) {

			FixtureItem fI = f.gameObject.GetComponent<FixtureItem>();

			if(fI.z % 3 == 0){
				Debug.Log(" A = " + a);
				fixturesZ += 300.0f;
				fixturesX = 0;
			}

			if(!IsOdd(fI.z)){ 
				f.transform.position = new Vector3(fixturesX/100.0f, 1.0f, fI.depth/100.0f + fixturesZ/100.0f);
				f.transform.rotation = Quaternion.Euler(0,180.0f,0);
			}else{
				f.transform.position = new Vector3(fixturesX/100.0f, 1.0f, fixturesZ/100.0f);
				fixturesX += fI.width;
			}
			a++;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
