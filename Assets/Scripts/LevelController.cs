using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    #region ENUMS
    public enum levelType { EASY, NORMAL, SEMINORMAL, HARD };
    #endregion

    #region STRUCTS
    [System.Serializable]
    public struct Level
    {
        public levelType type;
        public int Waves;
        public Sprite mountainColor;
        public float timeBetweenWaves;
        public float mountainTimeToReach;
        public int animationState;
    }

    [System.Serializable]
    public struct Destination
    {
        public Transform startPostition;
        public Transform endPosition;

    }
    #endregion

    #region STATIC FIELDS
    private static LevelController _instance;
    public static LevelController Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<LevelController>();
            return _instance;
        }
    }
    #endregion

    #region PUBLIC FIELDS
    public Destination[] destinations;
    #endregion

    #region SERIALIZED FIELDS
    [SerializeField]
    private Level[] levels;
    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private GameObject mountain;
    [SerializeField]
    private GameObject rightHand;
    [SerializeField]
    private GameObject leftHand;
    #endregion

    #region PRIVATE FIELDS
    private int counter = 0;
    #endregion

    #region PUBLIC METHODS
    public void AnimateHand(int hand)
    {
        if (hand == 0)
            rightHand.GetComponent<Animator>().Play(0);
        else if (hand == 2)
            leftHand.GetComponent<Animator>().Play(0);
        else
        {
            int randomHand = Random.Range(0, 2);
            if (randomHand == 0)
                StartCoroutine(MoveHandCoroutine(1.4f));
            else
                StartCoroutine(MoveHandCoroutine(-0.13f));
        }
    }
    #endregion

    #region PRIVATE METHODS
    // Use this for initialization
    void Start()
    {
        StartCoroutine(InstantiateCoroutine());
        StartCoroutine(ChangeLevel());
    }

    void IncreaseWave()
    {
        counter++;
    }
    #endregion

    #region COROUTINES
    IEnumerator MoveHandCoroutine(float x)
    {
        float timer = 0;
        float timeToReach = 0.5f;
        float startPosition = x>0 ? 5.73f :-4.34f;
        while (true)
        {
         
            timer += 1f * Time.deltaTime / timeToReach;
            if(x > 0)
                rightHand.transform.position = new Vector3(Mathf.Lerp(startPosition, x, timer), rightHand.transform.position.y, rightHand.transform.position.z);
            else
                leftHand.transform.position = new Vector3(Mathf.Lerp(startPosition, x, timer), leftHand.transform.position.y, leftHand.transform.position.z);
            if (Utility.almostEqual(timer, 1, 0.1f))
            {
                if (x > 0)
                {
                    rightHand.GetComponent<Animator>().Play(0);
                    StartCoroutine(MovehandBackCoroutine(startPosition));
                    yield break;
                }
                else
                {

                    leftHand.GetComponent<Animator>().Play(0);
                    StartCoroutine(MovehandBackCoroutine(startPosition));
                    yield break;

                }
            }

            yield return null;

        }
    }

    IEnumerator MovehandBackCoroutine(float x)
    {
        float timer = 0;
        float timeToReach = 0.5f;
        float startPosition = x > 0 ? rightHand.transform.position.x : leftHand.transform.position.x;
        while (true)
        {
            timer += 1f * Time.deltaTime / timeToReach;
            if (x > 0)
            {
          
                rightHand.transform.position = new Vector3(Mathf.Lerp(startPosition, x, timer), rightHand.transform.position.y, rightHand.transform.position.z);
            }
            else
            {
         
                leftHand.transform.position = new Vector3(Mathf.Lerp(startPosition, x, timer), leftHand.transform.position.y, leftHand.transform.position.z);
            }
            if (Utility.almostEqual(timer, 1, 0.1f))
            {

                yield break;
            }
            yield return null;

        }
    }

    IEnumerator InstantiateCoroutine()
    {
        while (true)
        {
            GameObject mountainGO = Instantiate(mountain);
            int randomDest = Random.Range(0, destinations.Length);
            mountainGO.GetComponent<MountainMove>().Init(levels[currentLevel].mountainTimeToReach, levels[currentLevel].mountainColor
                , randomDest, levels[currentLevel].animationState);
            IncreaseWave();
            yield return new WaitForSeconds(levels[currentLevel].timeBetweenWaves);
        }
    }

    IEnumerator ChangeLevel()
    {
        while (true)
        {
            if (counter == levels[currentLevel].Waves)
            {
                if (levels[currentLevel].type != levelType.HARD)
                    currentLevel++;
                counter = 0;
            }
            yield return null;
        }
    }
    #endregion

}
