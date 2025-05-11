using System.Collections;
using System.IO;
using BepInEx;
using TMPro;
using UnityEngine;

namespace CustomMOTDText
{
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
		private string customText;
		private const string fileName = "custommotdtext.txt";
		private const string discordHandle = "@HanSolo1000Falcon";
	    
	    void Start()
	    {
		    HarmonyPatches.ApplyHarmonyPatches();
		    GorillaTagger.OnPlayerSpawned(Init);
	    }

	    void Init()
	    {
		    string filePath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, fileName);
		    try
		    {
			    if (!File.Exists(filePath))
			    {
				    File.WriteAllText(filePath, $"IF YOU'RE READING THIS IT MEANS YOU HAVE NOT YET ASSIGNED A CUSTOM MOTD TEXT. COULD YOU PLEASE DO THAT IN THE FILE CALLED '{fileName}' IN THE GAME FOLDER? FULL FILE PATH: {filePath}");
			    }
			    customText = File.ReadAllText(filePath);
		    }
		    catch (IOException ex)
		    {
			    Logger.LogError($"[CustomMOTDText] Failed to access MOTD file: {ex}");
			    customText = $"ERROR LOADING CUSTOM MESSAGE OF THE DAY TEXT, IF THIS PROBLEM PERSISTS AFTER RESTARTS PLEASE CONTACT ME IN THE GORILLA TAG MODDING GROUP DISCORD SERVER {discordHandle}";
		    }
		    StartCoroutine(AssignText());
	    }

	    IEnumerator AssignText()
	    {
		    GameObject motd;
		    TextMeshPro tmp = null;
		    while (tmp == null)
		    {
			    motd = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/motdtext");
			    if (motd != null)
			    {
				    tmp = motd.GetComponent<TextMeshPro>();
			    }
			    yield return null;
		    }
		    tmp.text = customText;
	    }
    }
}
