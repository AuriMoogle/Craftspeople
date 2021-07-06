using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsPeople.UI
{
    public class ResetWorkshopSelectionScrollValue : MonoBehaviour
    {
        [SerializeField]
        float value;

        void Awake()
        {
            PlayerPrefs.SetFloat(WorkshopGrid.ScrollValuePlayerPref, value);
        }
    } 
}
