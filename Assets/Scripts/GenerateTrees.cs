using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrees : MonoBehaviour
{
    #region STRUCTS
    [System.Serializable]
    public struct Destination
    {
        public Transform startPostition;
        public Transform endPosition;

    }
    #endregion

    #region STATIC FIELDS
    private static GenerateTrees _instance;
    public static GenerateTrees Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<GenerateTrees>();
            return _instance;
        }
    }
    #endregion

    #region PUBLIC FIELDS
    public Destination[] Destinations;
    #endregion

    #region SERIALIZED FIELDS
    [SerializeField]
    private GameObject tree;
    #endregion

    #region PRIVATE FIELDS
    float timeBetweenTrees = 1.5f;

    float treeTimeToReach = 5f;
    #endregion

    #region PRIVATE METHODS
    private void Start()
    {
        StartCoroutine(InstantiateCoroutine());
    }
    #endregion

    #region COROUTINES
    IEnumerator InstantiateCoroutine()
    {
        while (true)
        {
            GameObject treeGO = Instantiate(tree);
            int randomDest = Random.Range(0, Destinations.Length);
            treeGO.GetComponent<TreeMoves>().Init(treeTimeToReach,randomDest);
            yield return new WaitForSeconds(timeBetweenTrees);
        }
    }
    #endregion
}
