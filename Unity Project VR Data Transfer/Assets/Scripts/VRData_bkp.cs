//using UnityEngine;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Net;
//using System.Text;
//using VRDataLib.Data;
//
//namespace VRDataLib {
//
//	namespace Data {
//
//		public class VRDataObject {
//
//			public delegate void sendRequest ();
//			public static event sendRequest OnSendRequest;
//
//			public static int OBJS_PER_REQUEST = 2;
//			public static string REQUEST_STATUS = "";
//
//			public static List <VRDataObject> allDataObjs = new List <VRDataObject>();
//
//			public ArrayList objData = new ArrayList();
//
//			public VRDataObject(string _modelType) {
//
//				//Keep room for different models
//				switch (_modelType) {
//
//				case ("A"):
//
//					objData = VRDataObjectBuilder.buildModel_A();
//					break;
//
//				case ("B"):
//					objData = VRDataObjectBuilder.buildModel_B();
//					break;
//
//				default:
//					Debug.LogError("VR data model type not defined, check your VRDataObject");
//					break;
//
//				}
//
//				//HACK: Not handling async properly
//				allDataObjs.Add(this);
//
//				if(allDataObjs.Count >= OBJS_PER_REQUEST) {
//
//					if (OnSendRequest != null) {
//
//						Debug.Log("Send request");
//						OnSendRequest ();
//
//					}
//
//				}
//
//			}
//
//			public static void clearAllData() {
//
//				allDataObjs.Clear();
//
//			}
//
//			public void updateVRDataObject () {
//
//				//TODO: Update values
//
//			}
//
//		}
//
//		public static class VRDataObjectBuilder {
//
//			public static ArrayList _data;
//
//			public static ArrayList buildModel_B () {
//
//				_data = new ArrayList();
//
//				Tuple1 <float> _scene;
//				_scene = new Tuple1<float> ("Scene", Data.VRDataObject.allDataObjs.Count);
//				_data.Add(_scene);
//
//				return _data;
//
//			}
//
//			public static ArrayList buildModel_A () {
//
//				_data = new ArrayList();
//
//				Tuple1 <float>
//				_scene,
//				_poi,
//				_duration,
//				_interaction,
//				_timestamp;
//
//				Tuple1 <object>
//				_pos,
//				_rot,
//				_scale;
//
//				_scene 			= new Tuple1<float> ("scene", 1);
//				_poi 			= new Tuple1<float>	("poi", 2);
//				_pos 			= new Tuple1<object> ("pos", new Tuple3<float, float, float>("x", 3, "y", 4, "z", 5));
//				_rot			= new Tuple1<object> ("rot", new Tuple3<float, float, float>("x", 6, "y", 7, "z", 8));
//				_scale			= new Tuple1<object> ("scale", new Tuple3<float, float, float>("x", 9, "y", 10, "z", 11));
//				_duration	 	= new Tuple1<float>	("duration", 12);
//				_interaction 	= new Tuple1<float>	("interaction", 13);
//				_timestamp	 	= new Tuple1<float>	("timestamp", 14);
//
//				_data.Add(_scene);
//				_data.Add(_poi);
//				_data.Add(_pos);
//				_data.Add(_rot);
//				_data.Add(_scale);
//				_data.Add(_duration);
//				_data.Add(_interaction);
//				_data.Add(_timestamp);
//
//				return _data;
//
//			}
//
//			public static string buildVRDataString (bool createFile) {
//
//				StringBuilder.stringifyData(createFile);
//
//				return StringBuilder.getDataString();
//
//			}
//
//		}
//
//		public static class StringBuilder {
//
//			private static string data = "";
//
//			public static string getDataString() {
//
//				return data;
//
//			}
//
//			public static string stringifyData(bool _createFile) {
//
//				//clear data first
//				data = "[";
//
//				int index = 0;
//
//				foreach (VRDataObject _obj in VRDataObject.allDataObjs){
//
//					index ++;
//
//					data += stringifyObj(_obj);
//					if(index < VRDataObject.allDataObjs.Count) {
//
//						data += ", ";
//
//					} else {
//
//						data += "";
//
//					}
//
//
//				}
//
//				data += "]";
//
//				if (_createFile){
//
//					//TODO: write to file
//					return data;
//
//				} else {
//
//					return data;
//
//				}
//
//			}
//
//			private static string stringifyObj(VRDataObject _obj) {
//
//				string tupleData = "{";
//				int index = 0;
//
//				foreach (Tuple _tuple in _obj.objData) {
//
//					index++;
//
//					if (_tuple.getItems () [1].GetType () != typeof(float)) {
//
//						tupleData += "\"";
//						tupleData += _tuple.getItems () [0];
//						tupleData += "\"";
//						tupleData += ": ";
//						Tuple3 <float, float, float> tupleValue = (Tuple3 <float, float, float>) _tuple.getItems () [1];
//						tupleData += tupleValue.convertToString ();
//						tupleData += "";
//
//					} else {
//
//						tupleData += _tuple.convertToString();
//
//					}
//
//					if (index < _obj.objData.Count) {
//
//						tupleData += ", ";
//
//					} else {
//
//						tupleData += "";
//
//					}
//
//				}
//
//				tupleData += "}";
//				return tupleData;
//
//			}
//
//		}
//
//
//
//		#region Tuple
//
//		public abstract class Tuple{
//
//			public abstract ArrayList getItems();
//			public abstract string convertToString();
//
//		}
//
//		public class Tuple1 <T1> : Tuple{
//
//			protected string key1;
//			protected T1 value1;
//
//			public Tuple1 (string _key1, T1 _value1) {
//
//				this.key1 = _key1;
//				this.value1 = _value1;
//
//			}
//
//			public override ArrayList getItems() {
//
//				return new ArrayList (){key1, value1};
//
//			}
//
//			public override string convertToString() {
//
//				string stringTuple = "\"" + this.key1 + "\"" + ": " + "\"" + this.value1.ToString() + "\"";
//
//				return stringTuple;
//
//			}
//
//		}
//
//		public class Tuple2 <T1, T2> : Tuple1 <T1> {
//
//			protected string key2;
//			protected T2 value2;
//
//			public Tuple2 (string _key1, T1 _value1, string _key2, T2 _value2)
//				: base (_key1, _value1) {
//
//				this.key2 = _key2;
//				this.value2 = _value2;
//
//			}
//
//			public override ArrayList getItems() {
//
//				return new ArrayList (){key1, value1, key2, value2};
//
//			}
//
//			public override string convertToString() {
//
//				string stringTuple =
//					"{ " +
//					this.key1 + ": " + this.value1.ToString() + ", " +
//					this.key2 + ": " + this.value2.ToString() +
//					"}";
//
//				return stringTuple;
//
//			}
//
//		}
//
//		public class Tuple3 <T1, T2, T3> : Tuple2 <T1, T2> {
//
//			protected string key3;
//			protected T3 value3;
//
//			public Tuple3 (string _key1, T1 _value1, string _key2, T2 _value2, string _key3, T3 _value3)
//				: base(_key1, _value1, _key2, _value2) {
//
//				this.key3 = _key3;
//				this.value3 = _value3;
//
//			}
//
//			public override ArrayList getItems() {
//
//				return new ArrayList() {key1, value1, key2, value2, key3, value3};
//
//			}
//
//			public override string convertToString() {
//
//				string stringTuple =
//					"{ " +
//					"\"" + this.key1 + "\"" + ": "  + "\"" + this.value1.ToString()  + "\"" + ", " +
//					"\"" + this.key2 + "\"" + ": "  + "\"" + this.value2.ToString()  + "\"" + ", " +
//					"\"" + this.key3 + "\"" + ": "  + "\"" + this.value3.ToString()  + "\"" + " " +
//					"}";
//
//				return stringTuple;
//
//			}
//
//		}
//
//		#endregion Tuple
//
//	}
//
//	namespace VR {
//
//		//TODO: capture VR Camera events
//		public class OVREvents {
//
//		}
//
//
//	}
//
//	namespace API {
//
//		//server communication classes
//
//		//TODO: Upgrade to System.net
//		/*
//		public static class POST {
//
//			public static void sendPostRequest (string _url, Dictionary<string, string> _params) {
//
//				WebRequest request 	= WebRequest.Create(_url);
//				request.Method 		= "POST";
//				byte[] byteData 	= new byte[_params.Count];
//
//				int dataIndex = 0;
//
//				foreach (KeyValuePair<string, string> param in _params) {
//
//					string fieldData = "{" + param.Key + ", " + param.Value + "}";
//					byteData [dataIndex] = Encoding.UTF8.GetBytes(fieldData);
//					dataIndex++;
//
//				}
//
//				request.ContentType 	= "application/json";
//				request.ContentLength 	= byteData.Length;
//
//				Stream dataStream = request.GetRequestStream();
//				dataStream.Write(byteData, 0, byteData.Length);
//				dataStream.Close();
//
//				WebResponse response = request.GetResponse();
//				Debug.Log(((HttpWebResponse) response).StatusDescription);
//				response.Close();
//
//
//			}
//
//		}*/
//
//
//	}
//}
//
//public class VRData : MonoBehaviour {
//
//	public string
//	URL_INIT = "https://www.euriskomobility.me/vr/api/initialize_session.php",
//	URL_DATA = "https://www.euriskomobility.me/vr/api/submit_data.php";
//
//	private string msg;
//
//	public IEnumerator postInit(){
//
//		yield return null;
//
//		WWWForm form = new WWWForm ();
//		form.AddField ("appid", "123");
//		form.AddField ("deviceid", "456");
//
//		WWW www = new WWW(URL_INIT, form);
//		StartCoroutine(HTTPRequest(www, "initRequest"));
//
//		Debug.Log ("Init()");
//
//	}
//
//	public IEnumerator postData(string _data){
//
//		yield return null;
//
//		WWWForm form = new WWWForm ();
//		form.AddField ("data", _data);
//		form.AddField ("appid", "123");
//		form.AddField ("userid", "22");
//		form.AddField ("sessionid", msg);
//
//		WWW www = new WWW(URL_DATA, form);
//		StartCoroutine(HTTPRequest(www, "dataRequest"));
//	}
//
//	public IEnumerator sendDataCo(string _data) {
//
//		yield return null;
//		StartCoroutine(postData(_data));
//		Debug.Log ("Data()");
//
//	}
//
//	void sendData () {
//
//		string _data = VRDataObjectBuilder.buildVRDataString(false);
//		Debug.Log (_data);
//		VRDataObject.clearAllData ();
//		StartCoroutine (sendDataCo (_data));
//
//	}
//
//	IEnumerator HTTPRequest(WWW www, string _flag){
//
//		yield return www;
//
//		// check for errors
//		if (www.error == null)
//		{
//			msg = www.text;
//			Debug.Log("WWW Ok!: " + www.text);
//
//			if (_flag == "dataRequest") {
//
//				VRDataLib.Data.VRDataObject.clearAllData ();
//				Debug.Log (VRDataObjectBuilder.buildVRDataString(false));
//
//			}
//
//			if (_flag == "initRequest") {
//
//				//Start listening to sendDataRequests
//				VRDataObject.OnSendRequest += sendData;
//
//			}
//
//		} else {
//			Debug.Log("WWW Error: "+ www.error);
//		}
//
//	}
//
//	void OnEnable () {
//
//		//Listen when to send request
//		StartCoroutine(postInit());
//
//	}
//
//	void Start () {
//
//	}
//
//}