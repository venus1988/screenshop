using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;

public class Prefs 
{


	public static void Save<T> (string name, T instance)
	{
		XmlSerializer serializer = new XmlSerializer (typeof(T));
		using (var ms = new MemoryStream ()) 
		{
			serializer.Serialize (ms,instance);
			PlayerPrefs.SetString (name, System.Text.ASCIIEncoding.ASCII.GetString (ms.ToArray ()));
		}
	}
	
	public static T Load<T> (string name)
	{
		if(!PlayerPrefs.HasKey(name)) return default(T);
		XmlSerializer serializer = new XmlSerializer (typeof(T));
		T instance;
		using (var ms = new MemoryStream (System.Text.ASCIIEncoding.ASCII.GetBytes (PlayerPrefs.GetString (name)))) 
		{
			instance = (T)serializer.Deserialize (ms);
		}
		return instance;
	}



	public static void SaveJson<T>(string Key,T Instance)
	{
		string temp=JsonConvert.SerializeObject(Instance);
		PlayerPrefs.SetString(Key,temp);
	}
	public static T LoadJson<T>(string Key)
	{
		string temp=PlayerPrefs.GetString(Key);
		T instance=JsonConvert.DeserializeObject<T>(temp);
		return instance;
	}

	/*
	 * 
		var myObject = new MyClass();
		Prefs.Save<MyClass>("my object", myObject);
		var anotherObject = Prefs.Load<MyClass>("my object")

*/
}
