using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace vt
{
	public class ADBLogger : EditorWindow
	{
		//
		// Fields
		//
		private Vector2 Scroll;
		
		public Process p;
		
		private bool updated = false;
		
		public string Filter = "logcat -s Unity";
		
		private string fileName;// = Environment.GetFolderPath (26) + "/aisadbPath.txt";
		
		public bool isStarted;
		
		public string logData = "Log Data\n\n";
		
		//
		// Properties
		//
		private string pathToAdb
		{
			get
			{
//				if (File.Exists (this.fileName))
//				{
//					return File.ReadAllText (this.fileName);
//				}

				return EditorPrefs.GetString("AndroidSdkRoot")+"/platform-tools/adb";
			}
		}
		
		//
		// Static Methods
		//
		[MenuItem ("Versatile/ADBLogger")]
		private static void Init ()
		{
			EditorWindow window = EditorWindow.GetWindow<ADBLogger> (true, "ADBLogger", true);
			Vector2 vector = new Vector2 (500f, 500f);
			//window.maxSize = vector;
			//window.minSize = vector;
			window.Focus ();

		}
		
		//
		// Methods
		//
		private void InitLog ()
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo ();
			processStartInfo.FileName = this.pathToAdb;
//			Debug.Log(this.pathToAdb);
			processStartInfo.Arguments = this.Filter;
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.UseShellExecute = false;
			processStartInfo.WorkingDirectory = Environment.GetFolderPath (0);
			this.p = new Process ();
			this.p.StartInfo = processStartInfo;
			this.p.OutputDataReceived += new DataReceivedEventHandler (this.OutPut);
			this.p.ErrorDataReceived += new DataReceivedEventHandler (this.OutPut);
			this.p.Start ();
			this.p.BeginErrorReadLine ();
			this.p.BeginOutputReadLine ();
		}
		
		private void OnGUI ()
		{
			/*if (GUILayout.Button ("Change Adb Path", new GUILayoutOption[0]))
			{
				string text = EditorUtility.OpenFilePanel ("Open Adb", Environment.GetFolderPath (0), "");
				if (text.Length > 1)
				{
					UnityEngine.Debug.Log(this.fileName+"   "+text);
					//File.WriteAllText (this.fileName, text);
				}
			}*/
			if (!this.isStarted)
			{
				if (GUILayout.Button ("Start Log", new GUILayoutOption[0]))
				{
					if (this.pathToAdb != "" )//&& File.Exists (this.pathToAdb))
					{
						UnityEngine.Debug.Log(pathToAdb);
						this.isStarted = true;
						this.InitLog ();
					}
					else
					{
						EditorUtility.DisplayDialog ("Error!!", "Please browse valid Adb file..", "Ok");
					}
				}
			}
			else
			{
				if (GUILayout.Button ("Stop Log", new GUILayoutOption[0]))
				{
					this.isStarted = false;
					this.p.Close ();
				}
			}
			if (GUILayout.Button ("Clear", new GUILayoutOption[0]))
			{
				this.logData = "Log Data \n\n\n";
			}
			this.Filter = EditorGUILayout.TextField ("Filter : ", this.Filter, new GUILayoutOption[0]);
			this.Scroll = GUILayout.BeginScrollView (this.Scroll, new GUILayoutOption[]
			{
				//GUILayout.Height (700f)
			});
			GUILayout.TextArea (this.logData, new GUILayoutOption[0]);
			GUILayout.EndScrollView ();
		}
		
		private void OutPut (object sender, DataReceivedEventArgs e)
		{
			this.logData = this.logData + e.Data + "\n";
			this.updated = true;
		}
		
		private void Update ()
		{
			if (this.updated)
			{
				this.updated = false;
				this.Scroll.y = float.PositiveInfinity;
				base.Repaint ();
			}
		}
	}
}
