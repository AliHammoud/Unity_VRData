using UnityEngine;
using VRStandardAssets.Utils;
using System;
using VRDataLib.Data;
using System.Collections;
using System.Collections.Generic;

namespace VRStandardAssets.Examples
{
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
    public class ExampleInteractiveItem : MonoBehaviour
    {
        [SerializeField] private Material m_NormalMaterial;                
        [SerializeField] private Material m_OverMaterial;                  
        [SerializeField] private Material m_ClickedMaterial;               
        [SerializeField] private Material m_DoubleClickedMaterial;         
        [SerializeField] private VRInteractiveItem m_InteractiveItem;
        [SerializeField] private Renderer m_Renderer;

		private Dictionary<string, string> args;
		private bool isLooking = false;
		private bool isPOI = false;
		private const float FOCUS_TIME = 0.75f;
		private float lookAtTime;

		private Vector3 scl;

        private void Awake ()
        {
			
            m_Renderer.material = m_NormalMaterial;

        }


        private void OnEnable()
        {
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
            m_InteractiveItem.OnClick += HandleClick;
            m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
			scl = this.transform.localScale;
        }


        private void OnDisable()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
            m_InteractiveItem.OnClick -= HandleClick;
            m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
        }


        //Handle the Over event
        private void HandleOver()
        {
			//Color hover = new Color (0.2f, 0.2f, 0.2f);
			m_Renderer.material.SetColor ("_Color", new Color (0.2f, 0.2f, 0.2f));

			if (VRData.canLook) {

				isLooking = true;
				m_Renderer.material = m_OverMaterial ; 

				this.transform.localScale = this.transform.localScale * 1.15f;

				StartCoroutine (letFocus());

			}

        }

		private IEnumerator letFocus() {

			args = new Dictionary<string, string>();

			lookAtTime = Time.timeSinceLevelLoad;

			yield return new WaitForSeconds (FOCUS_TIME);

			if (isLooking) {

				isPOI = true;

			} else {

				isPOI = false;

			}

		}


        //Handle the Out event
        private void HandleOut()
        {

			m_Renderer.material.SetColor ("_Color", new Color (1f, 1f, 1f));
			this.transform.localScale = scl;

			if (isPOI) {

				//Get look duration
				double lookDuration = Time.timeSinceLevelLoad - lookAtTime;
				lookDuration = Math.Round (lookDuration, VRDataObjectBuilder.PRECISION);
				args.Add ("duration", lookDuration.ToString());

				//Get timestamp (seconds since 01/01/1970)
				long ticks = DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks;
				ticks /= 10000000; //Convert windows ticks to seconds
				args.Add ("timestamp", ticks.ToString());

				//Set interaction type
				args.Add("interaction", "focus");

				Debug.Log ("Focused on: " + this.GetComponent<Transform> ().name + " for " + lookDuration + " secs");
				VRDataObject obj = new VRDataObject ("A", this.GetComponent<Transform>(), args);

			}
            m_Renderer.material = m_NormalMaterial;

			isLooking = false;
			isPOI = false;

        }


        //Handle the Click event
        private void HandleClick()
        {
			
            Debug.Log("Show click state");
            m_Renderer.material = m_ClickedMaterial;

        }


        //Handle the DoubleClick event
        private void HandleDoubleClick()
        {
            Debug.Log("Show double click");
            m_Renderer.material = m_DoubleClickedMaterial;
        }
    }

}