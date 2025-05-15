using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour, IInteractable
{
    public static Reward Instance;

    //set within this script, get publicly
    public bool isOpened;
    //public string RewardID { get; private set; }
    //public GameObject itemPrefab; //item the chest drops, the reward. lets change it to a ui interface
    [Header("Reward Manager")]
    //public GameObject rewardsPanel;
    GameObject rewardsPanel;
    GameObject rewardsCanvas;
    public GameObject[] rewardButtonPrefabs;
    public Transform rewardOneContainer;
    public Transform rewardTwoContainer;
    public Transform rewardThreeContainer;
    //List<GameObject> _rewards = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        rewardsCanvas = GameObject.FindGameObjectWithTag("RewardCanvas");
        //have to find rewards panel when instantiated
        rewardsPanel = rewardsCanvas.transform.Find("RewardsPanel").gameObject;
        Debug.Log("found rewards panel");
        rewardOneContainer = rewardsPanel.transform.Find("RewardOneContainer");
        rewardTwoContainer = rewardsPanel.transform.Find("RewardTwoContainer");
        rewardThreeContainer = rewardsPanel.transform.Find("RewardThreeContainer");
        rewardsPanel.SetActive(false);
        //rewardButtonPrefabs = GameObject.FindGameObjectsWithTag("Rewards");
        //_rewards.Add(Instantiate(rewardButtonPrefabs));
    }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

        void Update()
    {
        //if (isOpened)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        CloseMenu();
        //    }
        //}
    }

    public bool CanInteract()
    {
        return !isOpened;
    }

    public void Interact()
    {
        if (!CanInteract()) return;
        //open chest
        OpenReward();
    }

    private void OpenReward()
    {
        SetOpened(true);

        //drop item
        /*if (itemPrefab)
        {
            GameObject droppedItem = Instantiate(itemPrefab, transform.position + Vector3.down, Quaternion.identity);
        }*/

        //instantiate random buttons into the containers
    }

    public void SetOpened(bool opened)
    {
        if (isOpened = opened)
        {
            ShowRewardUI(true);
        }
    }

    public void ShowRewardUI(bool show)
    {
        rewardsPanel.SetActive(show);


        Debug.Log("instantiated");
        GameObject buttonOneClone = Instantiate(rewardButtonPrefabs[Random.Range(0, 3)], rewardOneContainer.position, rewardOneContainer.rotation, rewardOneContainer.transform);
        GameObject buttonTwoClone = Instantiate(rewardButtonPrefabs[Random.Range(3, 6)], rewardTwoContainer.position, rewardTwoContainer.rotation, rewardTwoContainer.transform);
        GameObject buttonThreeClone = Instantiate(rewardButtonPrefabs[Random.Range(6, 9)], rewardThreeContainer.position, rewardThreeContainer.rotation, rewardThreeContainer.transform);
        //setpause
        Time.timeScale = 0;
        //deletes duplicate buttons after picking
        Destroy(buttonOneClone, 1f);
        Destroy(buttonTwoClone, 1f);
        Destroy(buttonThreeClone, 1f);
    }

    public void CloseMenu()
    {
        //set pause false
        Time.timeScale = 1;
        rewardsPanel.SetActive(false);
        SetOpened(false);
        Destroy(gameObject);
        Debug.Log("destroyed");
    }
}
