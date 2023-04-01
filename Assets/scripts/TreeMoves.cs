using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMoves : MonoBehaviour {
    #region PRIVATE FIELDS
    float timer;
    float timeToReach = 1f;
    int destination;
    Transform startPosition;
    Transform endPosition;
    #endregion

    #region PUBLIC METHODS
    public void Init(float treeTimeToReach, int randomDest)
    {
        destination = randomDest;
        this.startPosition = GenerateTrees.Instance.Destinations[randomDest].startPostition;
        this.endPosition = GenerateTrees.Instance.Destinations[randomDest].endPosition;
        timeToReach = treeTimeToReach;
        StartCoroutine(MoveToDestinationCoroutine());
    }
    #endregion

    #region COROUTINES
    IEnumerator MoveToDestinationCoroutine()
    {
        while (true)
        {
            timer += Time.deltaTime * 1f / timeToReach;
            transform.position = Vector3.Lerp(startPosition.position, endPosition.position, timer);
            if (Utility.almostEqual(timer, 1, 0.01f))
            {
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }
    #endregion
}
