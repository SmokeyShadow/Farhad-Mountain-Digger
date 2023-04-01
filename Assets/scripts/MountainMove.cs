using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainMove : MonoBehaviour {
    #region PRIVATE FIELDS
    Transform endPosition;
    Transform startPosition;
    float timer;
    float timeToReach = 5f;
    float lineZ = 21;
    Animator animator;
    float animatorTimeReach = 1f;
    float animationTimer;
    int destination;
    #endregion

    #region PUBLIC METHODS
    public void Init(float mountainTimeToReach, Sprite mountainColor, int randomDest, int animationState)
    {
        animator = GetComponent<Animator>();
        destination = randomDest;
        this.startPosition = LevelController.Instance.destinations[randomDest].startPostition;
        this.endPosition = LevelController.Instance.destinations[randomDest].endPosition;
        animator.SetInteger("destroy", animationState);
        GetComponent<SpriteRenderer>().sprite = mountainColor;
        timeToReach = mountainTimeToReach;
        StartCoroutine(MoveToDestinationCoroutine());
    }
    #endregion

    #region PRIVATE METHODS
    void OnMouseDown()
    {
        if (transform.position.z <= lineZ)
        {
            animator.enabled = true;
            LevelController.Instance.AnimateHand(destination);
            PlayRandomNote();
            StartCoroutine(SetActiveCoroutine());
        }

    }

    void PlayRandomNote()
    {
        int rand = Random.Range(0, 12);
        SoundPlayer.Instance.PlaySound((SoundPlayer.SoundClip)(rand));
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
                SoundPlayer.Instance.PlaySound(SoundPlayer.SoundClip.Slam);
                gameObject.SetActive(false);
                UIController.instance.GameOver();
                Time.timeScale = 0;
            }
            yield return null;
        }
    }
  
    IEnumerator SetActiveCoroutine()
    {
        while (true)
        {
            animationTimer += Time.deltaTime * 1f / animatorTimeReach;
            if (Utility.almostEqual(animationTimer, 1, 0.1f))
            {
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }
    #endregion
}
