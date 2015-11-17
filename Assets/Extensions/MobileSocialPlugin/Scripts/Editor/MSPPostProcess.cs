using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;

public class MSPPostProcess  {

	private const string BUNLDE_KEY = "SA_PP_BUNLDE_KEY";

	[PostProcessBuild(48)]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {

		#if UNITY_IPHONE

		#if UNITY_3_5 || UNITY_4_0 || UNITY_4_1	|| UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6

		string Accounts = "Accounts.framework";
		if(!ISDSettings.Instance.frameworks.Contains(Accounts)) {
			ISDSettings.Instance.frameworks.Add(Accounts);
		}
		
		
		string SocialF = "Social.framework";
		if(!ISDSettings.Instance.frameworks.Contains(SocialF)) {
			ISDSettings.Instance.frameworks.Add(SocialF);
		}
		
		string MessageUI = "MessageUI.framework";
		if(!ISDSettings.Instance.frameworks.Contains(MessageUI)) {
			ISDSettings.Instance.frameworks.Add(MessageUI);
		}

		#else
		
		#endif




		#endif
	}

}
