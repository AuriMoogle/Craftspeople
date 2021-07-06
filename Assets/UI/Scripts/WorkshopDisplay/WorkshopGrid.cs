
namespace CraftsPeople.UI
{
    using CraftsPeople.Data;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class WorkshopGrid : MonoBehaviour
    {
        // Save scrollbar state over scenes
        public static readonly string ScrollValuePlayerPref = "WorkshopSelection-ScrollValue";

        [SerializeField]
        Scrollbar scrollBar;

        [SerializeField]
        List<WorkshopData> workshops = new List<WorkshopData>();

        GameObject workshopPrefab;

        void Start()
        {
            StartCoroutine(SetScrollPos());

            workshopPrefab = transform.GetChild(0).gameObject;
            workshopPrefab.SetActive(false);

            foreach (var data in workshops)
            {
                GameObject go = Instantiate(workshopPrefab, transform);
                go.name = data.workshopName;
                go.SetActive(true);
                WorkshopDisplay cell = go.GetComponent<WorkshopDisplay>();
                cell.SetData(data);
            }
        }

        private IEnumerator SetScrollPos()
        {
            yield return null;
            scrollBar.value = PlayerPrefs.GetFloat(ScrollValuePlayerPref);
        }

        private void OnDisable()
        {
            PlayerPrefs.SetFloat(ScrollValuePlayerPref, scrollBar.value);

        }
    }
}
