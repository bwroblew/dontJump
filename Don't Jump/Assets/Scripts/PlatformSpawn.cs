using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour {

    public Queue platformQueue;
    public Queue decQueue;
    public Player player;
    public Transform playerT;
    public Object[] platformPrefab;
    public GameObject[] decPrefab;
    public MainManager mm;
    public Vector2 lastPlatform;
    public Vector2 newPlatform;
    public const float offset = 25;
    private int maxPlatforms = 10;
    public int lastWidth;
    private const int minPlatformWidth = 1;
    private const int maxPlatformWidth = 9;
    private const int minUnitDistance = 1;

    private const int randMaxPlatformWidth = 8;
    private const int randMinPlatformWidth = 5;

    private const int jumpWidth = 5;

    private const float minYPlatform = -4.5f;
    private const float maxYPlatform = 0.5f;


    public const float maxBetween = 5;
    public const float minP = 0.7f;

    public const float minLog = 0.2f;
    public const float maxLog = 1f;

    private const int probabilityDec = 20;


    void Start() {
        mm = gameObject.GetComponent<MainManager>();
        platformQueue = new Queue();
        decQueue = new Queue();
        lastPlatform.x = 15.2f;
        lastPlatform.y = -4.5f;
        lastWidth = 5; //starting width
    }

    void Update() {
        if (lastPlatform.x < playerT.transform.position.x + offset)
        {
            CreatePlatform();
        }
    }

    public float Map(float what, float minS, float maxS, float minE, float maxE)
    {
        what = minE + (what - minS) * (maxS - minS) / (maxS - minS);
        return what;
    }

    public int CreatePlatform()
    {
        int size;
        float prob = (float)(4.11 * Mathf.Log(Mathf.Log(mm.distance, 1.66f), 1.11f)) / 100;
        float prob2 = 8;//(float)(-0.5 * Mathf.Log(mm.distance + 20, Mathf.Exp(1)) + 10);
        int randMax = Mathf.FloorToInt(prob2);
        if (randMax > randMaxPlatformWidth)
            randMax = randMaxPlatformWidth;
        else if (randMax < randMinPlatformWidth)
            randMax = randMinPlatformWidth;
        /*if (lastWidth > 1)
            size = Random.Range(minPlatformWidth, maxPlatformWidth);
        else*/
        size = Random.Range(minPlatformWidth + 1, randMax + 1);
        float minD = lastWidth / 2 + size / 2 + minUnitDistance;
        if (minD < jumpWidth)
            minD = jumpWidth;
        //float c = Map(player.jumpTime, 1f, player.maxMul, minUnitDistance, maxBetween*minUnitDistance);
        float c = Map(lastWidth, minPlatformWidth, maxPlatformWidth, 1f, 4f);
        //Debug.Log(c);
        //float prob_p = prob;
        //Debug.Log(prob);
        if (prob < minLog || float.IsNaN(prob))
            prob = minLog;
        if (prob > maxLog)
            prob = maxLog;
        float newX = lastPlatform.x + minD + minUnitDistance * c * (minP < prob ? Random.Range(minP, prob) : minP);
        newPlatform.x = newX;
        float rand = Random.Range(-4, 3);
        if (rand > 0 && (newX - lastPlatform.x)*0.5 > minD)
        {
            newPlatform.x -= 0.2f * (newX-lastPlatform.x);
        }
        rand /= 2.5f;
        if (size < 4 && rand > 0)
            rand = 0;
        newPlatform.y = lastPlatform.y + rand;
        if (newPlatform.y < minYPlatform)
            newPlatform.y = minYPlatform;
        if (newPlatform.y > maxYPlatform)
            newPlatform.y = maxYPlatform;
        if (platformQueue.Count > maxPlatforms)
        {
            Destroy((Object)platformQueue.Dequeue());
        }
        platformQueue.Enqueue(Instantiate(platformPrefab[size-1], new Vector3(newPlatform.x, newPlatform.y, 0), Quaternion.identity));
        //Debug.Log(size + " " + minD + " " + lastWidth + " " + c + " " + prob_p + " " + prob + " " + newX);
        lastWidth = size;
        lastPlatform = newPlatform;
        CreateDecoration(size, newPlatform.x, newPlatform.y);
        return size;
    }

    void CreateDecoration(int size, float x, float y)
    {
        int wif = Random.Range(0, 100);
        if (wif > probabilityDec)
            return;
        int what = Random.Range(0, decPrefab.Length);
        int where = Random.Range(Mathf.RoundToInt(x - size / 2.5f + decPrefab[what].GetComponent<RectTransform>().sizeDelta.x / 2 + 0.1f), Mathf.RoundToInt(x + size / 2.5f - decPrefab[what].GetComponent<RectTransform>().sizeDelta.x / 2 - 0.1f));
        if (decQueue.Count > maxPlatforms)
        {
            Destroy((Object)decQueue.Dequeue());
        }
        decQueue.Enqueue(Instantiate(decPrefab[what], new Vector3(where, y + 0.33f + decPrefab[what].GetComponent<RectTransform>().sizeDelta.y/2, 0), Quaternion.identity));
    }

}
