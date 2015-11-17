using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class startModeling : MonoBehaviour
{

	public Button startMain;

	private bool temp1,temp2,startModel1,startModel2;

	void Start () 
	{

		startMain = GetComponent<Button>();

		temp1 = true;

		temp2 = true;

		startModel1 = false;

		startModel2 = false;


	}


	public void LoadMainScreenModel1()
	{

		if(temp1)
		{

			print ("screen 1 is selected");

			startModel2 = false;

			startModel1 = true;

			temp1 = false;

			temp2 = true;

		}


	}

	public void LoadMainScreenModel2()
	{
		if(temp2)
		{

			print("screen 2 is selected");

			startModel1 = false;

			startModel2  = true;

			temp2 = false;

			temp1 = true;

		}

	}

	public void loadNextScene()
	{

		if(startModel1 && !temp1)
		{
			Application.LoadLevel ("mainScene1");

		}else if(startModel2 && !temp2)
			{
				Application.LoadLevel ("mainScene2");
			}

	}

	void Update ()
	{
		
		
	}

}
