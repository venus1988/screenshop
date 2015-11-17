using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DynamicScrollView : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    public int noOfItems ;

    public GameObject item;
	public GameObject itemsep;
    public GridLayoutGroup gridLayout;

    public RectTransform scrollContent;
	 int scrollContentWidth=190;

	int imageIndex = 0;
    public ScrollRect scrollRect;
	bool notdistroy=false;


    #endregion

    #region PRIVATE_VARIABLES
    #endregion

    #region UNITY_CALLBACKS
    void OnEnable()
    {
       // InitializeList();
		for(int i=0;i<GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count;i++) 
		{
			PlayerPrefs.SetString("sep_enable"+i,"true");
		}
    }
    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS
    private void ClearOldElement()
    {
		if(notdistroy==true)
		  {

			for (int i = 0; i < gridLayout.transform.childCount; i++)
	        {
	            Destroy(gridLayout.transform.GetChild(i).gameObject);
	        }
		}
    }


	private void InitializeNewSepItem(string name)
	{
		print ("====NAME " + name);
		GameObject newItem = Instantiate(itemsep) as GameObject;
		newItem.name = name;
		newItem.transform.parent = gridLayout.transform;
		newItem.transform.localScale = new Vector3 (1, 0.5f, 1);//Vector3.one;
		newItem.SetActive(true);
	}
	private void InitializeNewItem(string name)
	{
		print ("====NAME " + name);
		GameObject newItem = Instantiate(item) as GameObject;
		newItem.name = name;
		newItem.transform.parent = gridLayout.transform;
		newItem.transform.localScale = Vector3.one;
		newItem.SetActive(true);
		newItem.GetComponent<Image>().sprite = GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded[int.Parse(name)];
		
	}
    public void SetContentHeight()
    {
		//print ("-------------grid" + gridLayout.transform.childCount);
		int tempcount = 0;
		for (int i =0; i <gridLayout.transform.childCount; i++) 
			if(PlayerPrefs.GetString("sep_enable"+i) == "true") tempcount++;
		tempcount *= 2;
        float scrollContentHeight = (tempcount * gridLayout.cellSize.y) + ((tempcount - 1) * gridLayout.spacing.y);
		scrollContent.sizeDelta = new Vector2(scrollContentWidth, scrollContentHeight);
    }

    public void InitializeList()
    {
        ClearOldElement();
		print ("noOfItems    " + noOfItems);

		for (int i = 0; i < noOfItems; i++) {
			if(PlayerPrefs.GetString("sep_enable" + i) == "true"){
				InitializeNewItem ("" + (i));
				InitializeNewSepItem ("" + (i)+".5");
			}
		}
		SetContentHeight ();

    }

    #endregion

    #region PRIVATE_COROUTINES
    private IEnumerator MoveTowardsTarget(float time,float from,float target)
	{
        float i = 0;
        float rate = 1 / time;
		print (from);
        while(i<1)
		{
            i += rate * Time.deltaTime;
            scrollRect.verticalNormalizedPosition = Mathf.Lerp(from,target,i);
            yield return 0;
        }
    }
    #endregion

    #region DELEGATES_CALLBACKS
    #endregion

    #region UI_CALLBACKS

    public void AddNewElement()
    {

        InitializeNewItem("" + (gridLayout.transform.childCount ));
        SetContentHeight();
        StartCoroutine(MoveTowardsTarget(0.2f, scrollRect.verticalNormalizedPosition, 0));
    }
	public void AddElement(string name)
	{
		InitializeNewItem (name);
		InitializeNewSepItem (name+".5");
		SetContentHeight();
	}
    #endregion


}
