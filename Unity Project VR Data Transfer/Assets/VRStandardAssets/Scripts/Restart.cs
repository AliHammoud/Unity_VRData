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
    public class Restart : MonoBehaviour
    {
		[SerializeField] private Material m_OverMaterial; 

        [SerializeField] private Material m_NormalMaterial;                
                         
        [SerializeField] private Material m_ClickedMaterial;               
        [SerializeField] private Material m_DoubleClickedMaterial;         
        [SerializeField] private VRInteractiveItem m_InteractiveItem;
        [SerializeField] private Renderer m_Renderer;

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

        }


        //Handle the Out event
        private void HandleOut()
        {
			
            m_Renderer.material = m_NormalMaterial;
			Application.LoadLevel (0);
			VRData.canLook = false;

        }


        //Handle the Click event
        private void HandleClick()
        {
			
            Debug.Log("Show click state");
            m_Renderer.material = m_ClickedMaterial;
			Application.LoadLevel (0);

        }


        //Handle the DoubleClick event
        private void HandleDoubleClick()
        {
            Debug.Log("Show double click");
            m_Renderer.material = m_DoubleClickedMaterial;
        }
    }

}