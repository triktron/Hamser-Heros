using UnityEngine;
using System.Collections;
using System;

public class Highscores : MonoBehaviour
{

	const string privateCode = "5mIF_u9sUEi8XL5iRI7lvwfU5YJBmRDkCYmHHSl6yHSA";
	const string publicCode = "5ea063130cf2aa0c28bb3242";
	const string webURL = "http://dreamlo.com/lb/";

	public Highscore[] highscoresList;
	public static Highscores instance;

	void Awake()
	{
			instance = this;
	}

	public static void AddNewHighscore(string username, float time, GameStateManager.Dificulties dif, int level)
	{
		instance.StartCoroutine(instance.UploadNewHighscore(username, time, dif, level));
	}

	IEnumerator UploadNewHighscore(string username, float time, GameStateManager.Dificulties dif, int level)
	{
		WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "|" + GameStateManager.uuid + "|" + level + "|" + (int)dif + "/" + (int)(time * 1000));
		yield return www;

		if (string.IsNullOrEmpty(www.error))
		{
			print("Upload Successful");
		}
		else
		{
			print("Error uploading: " + www.error);
		}
	}

	public delegate void Action();

	public void DownloadHighscores(Action cb)
	{
		StartCoroutine(DownloadHighscoresFromDatabase(cb));
	}

	IEnumerator DownloadHighscoresFromDatabase(Action cb)
	{
		WWW www = new WWW(webURL + publicCode + "/pipe-asc/");
		yield return www;

		if (string.IsNullOrEmpty(www.error))
		{
			FormatHighscores(www.text);
			cb();
		}
		else
		{
			print("Error Downloading: " + www.error);
		}
	}

	void FormatHighscores(string textStream)
	{
		string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
		highscoresList = new Highscore[entries.Length];

		for (int i = 0; i < entries.Length; i++)
		{
			string[] entryInfo = entries[i].Split(new char[] { '|' });
			string username = entryInfo[0];
			float time = int.Parse(entryInfo[4]) / 1000f;
			GameStateManager.Dificulties dif = (GameStateManager.Dificulties)int.Parse(entryInfo[3]);
			int level = int.Parse(entryInfo[2]);
			highscoresList[i] = new Highscore(username, time, dif, level);
		}
	}

}

public struct Highscore
{
	public string username;
	public float time;
	public GameStateManager.Dificulties dif;
	public int level;

	public Highscore(string _username, float _time, GameStateManager.Dificulties _dif, int _level)
	{
		username = _username;
		time = _time;
		dif = _dif;
		level = _level;
	}

}