using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class dreamloLeaderBoard : MonoBehaviour {
    private string privatelvl02 = "YkvC6-VSpU-xUh1Xx7XDgwsjDs2Ak9HUOewA9t9RQTdg";
    private string publiclvl02 = "5b0986ac191a850bcc34db52";
    private string privatelvl01 = "5OxG7JQCJUW1FqkXt_S6PwG4KMK6PD_EerjaVEq3lzqA";
    private string publiclvl01 = "5b09864a191a850bcc34da38";
    private string privatelvl0 = "SVUaeyXKyUi2DS_6ahwXCQ019xOROjGU2otg4MfiQpkA";
    private string publiclvl0 = "5b1bf021191a8a0bcc5ddd96";
    private string privatelvl1 = "U_B6NbnRPkacVkm3q_oBjgyhWXs3MpWEiLPP2xH8RjiQ";
    private string publiclvl1 = "5b1bf027191a8a0bcc5ddda7";
    private string privatelvl2 = "A2Ltn_7-_EunDY6uplZyCQjK3eP6cSPEKjsSTiGk28Mg";
    private string publiclvl2 = "5b1bf02c191a8a0bcc5dddb3";
    private string privatelvl3 = "xFEApKzLT0ev31FUm5WRcAkLqxuCJilEWcG7j7VjSzlg";
    private string publiclvl3 = "5b1bf031191a8a0bcc5dddbd";
    private string privatelvl4 = "7DWzl1dpg0CzJ-lCKbvxogx0stP98KZEq4Cc-LiS77wA";
    private string publiclvl4 = "5b1bf039191a8a0bcc5dddd1";
    private string privatelvl5 = "ZKQhxyYbckCaj18YFxDrsA7Wcr6cuUd0eMMKZC2-DTzw";
    private string publiclvl5 = "5b1d4d3c191a8a0bcc6506e1";

    string dreamloWebserviceURL = "http://dreamlo.com/lb/";

    private string privateCode = "";
    private string publicCode = "";
    
    string highScores02 = "";
    string highScores01 = "";
    string highScores0 = "";
    string highScores1 = "";
    string highScores2 = "";
    string highScores3 = "";
    string highScores4 = "";
    string highScores5 = "";

    public LeaderboardV5 lb5;

    ////////////////////////////////////////////////////////////////////////////////////////////////

    // A player named Carmine got a score of 100. If the same name is added twice, we use the higher score.
    // http://dreamlo.com/lb/(your super secret very long code)/add/Carmine/100

    // A player named Carmine got a score of 1000 in 90 seconds.
    // http://dreamlo.com/lb/(your super secret very long code)/add/Carmine/1000/90

    // A player named Carmine got a score of 1000 in 90 seconds and is Awesome.
    // http://dreamlo.com/lb/(your super secret very long code)/add/Carmine/1000/90/Awesome

    ////////////////////////////////////////////////////////////////////////////////////////////////


    public struct Score {
		public string playerName;
		public float score;
		public int seconds;
		public string shortText;
		public string dateString;
	}

	
	void Start()
	{
        print(Application.version);
        highScores02 = "";
        highScores01 = "";
        highScores0 = "";
        highScores1 = "";
        highScores2 = "";
        highScores3 = "";
        highScores4 = "";
        highScores5 = "";

        //StartCoroutine(AddScoreWithPipe("notVictor", int.Parse((PlayerPrefs.GetFloat("minigame", 1000) * -10000).ToString("0")), privatelvl1));
    }
	
	public static dreamloLeaderBoard GetSceneDreamloLeaderboard()
	{
		GameObject go = GameObject.Find("dreamloPrefab");
		
		if (go == null) 
		{
			Debug.LogError("Could not find dreamloPrefab in the scene.");
			return null;
		}
		return go.GetComponent<dreamloLeaderBoard>();
	}


	public static double DateDiffInSeconds(System.DateTime now, System.DateTime olderdate)
	{
	    var difference = now.Subtract(olderdate);
	    return difference.TotalSeconds;
	}

	System.DateTime _lastRequest = System.DateTime.Now;
	int _requestTotal = 0;




	bool TooManyRequests()
	{
		var now = System.DateTime.Now;

		if (DateDiffInSeconds(now, _lastRequest) <= 2)
		{
			_lastRequest = now;
			_requestTotal++;
			if (_requestTotal > 3)
			{
				Debug.LogError("DREAMLO Too Many Requests. Am I inside an update loop?");
				return true;
			}

		} else {
			_lastRequest = now;
			_requestTotal = 0;
		}

		return false;
	}

	public void AddScore(string playerName)
	{
		if (TooManyRequests()) return;

        StartCoroutine(AddScoreWithPipe(playerName, PlayerPrefs.GetInt("GameLevel6", 0), privatelvl02));
        StartCoroutine(AddScoreWithPipe(playerName, PlayerPrefs.GetInt("Cubecount", 0), privatelvl01));
        StartCoroutine(AddScoreWithPipe(playerName, int.Parse(((PlayerPrefs.GetFloat("minigame", 1000) + PlayerPrefs.GetFloat("GameLevel2", 1000) + PlayerPrefs.GetFloat("GameLevel3", 1000) + PlayerPrefs.GetFloat("GameLevel4", 1000) + PlayerPrefs.GetFloat("GameLevel5", 1000)) * -10000 + 10000000).ToString("0")), privatelvl0));
        StartCoroutine(AddScoreWithPipe(playerName, int.Parse((PlayerPrefs.GetFloat("minigame", 1000) * -10000 + 10000000).ToString("0")), privatelvl1));
        StartCoroutine(AddScoreWithPipe(playerName, int.Parse((PlayerPrefs.GetFloat("GameLevel2", 1000) * -10000 + 10000000).ToString("0")), privatelvl2));
        StartCoroutine(AddScoreWithPipe(playerName, int.Parse((PlayerPrefs.GetFloat("GameLevel3", 1000) * -10000 + 10000000).ToString("0")), privatelvl3));
        StartCoroutine(AddScoreWithPipe(playerName, int.Parse((PlayerPrefs.GetFloat("GameLevel4", 1000) * -10000 + 10000000).ToString("0")), privatelvl4));
        StartCoroutine(AddScoreWithPipe(playerName, int.Parse((PlayerPrefs.GetFloat("GameLevel5", 1000) * -10000 + 10000000).ToString("0")), privatelvl5));


        lb5.outputText.text = "Upload Successful";
        lb5.toggleSubmitTrue();
    }

    // This function saves a trip to the server. Adds the score and retrieves results in one trip.
    IEnumerator AddScoreWithPipe(string player, int totalScore, string code)
	{
		player = Clean(player);
		
		WWW www = new WWW(dreamloWebserviceURL + code + "/add-pipe/" + WWW.EscapeURL(player) + "/" + totalScore.ToString());
		yield return www;
	}
	
	IEnumerator GetScores()
	{
        publicCode = publiclvl02;
        highScores02 = "";
        WWW www = new WWW(dreamloWebserviceURL + publicCode + "/pipe");
        yield return www;
        highScores02 = www.text;

        publicCode = publiclvl01;
        highScores01 = "";
        www = new WWW(dreamloWebserviceURL + publicCode + "/pipe");
        yield return www;
        highScores01 = www.text;

        publicCode = publiclvl0;
        highScores0 = "";
        www = new WWW(dreamloWebserviceURL + publicCode + "/pipe");
        yield return www;
        highScores0 = www.text;

        publicCode = publiclvl1;
		highScores1 = "";
		www = new WWW(dreamloWebserviceURL +  publicCode  + "/pipe");
		yield return www;
		highScores1 = www.text;

        publicCode = publiclvl2;
        highScores2 = "";
        www = new WWW(dreamloWebserviceURL + publicCode + "/pipe");
        yield return www;
        highScores2 = www.text;

        publicCode = publiclvl3;
        highScores3 = "";
        www = new WWW(dreamloWebserviceURL + publicCode + "/pipe");
        yield return www;
        highScores3 = www.text;

        publicCode = publiclvl4;
        highScores4 = "";
        www = new WWW(dreamloWebserviceURL + publicCode + "/pipe");
        yield return www;
        highScores4 = www.text;

        publicCode = publiclvl5;
        highScores5 = "";
        www = new WWW(dreamloWebserviceURL + publicCode + "/pipe");
        yield return www;
        highScores5 = www.text;

        lb5.formatScores();
    }
    public void DeletePrevious(string name)
    {
        StartCoroutine(DeleteScore(privatelvl02, name));
        StartCoroutine(DeleteScore(privatelvl01, name));
        StartCoroutine(DeleteScore(privatelvl0, name));
        StartCoroutine(DeleteScore(privatelvl1, name));
        StartCoroutine(DeleteScore(privatelvl2, name));
        StartCoroutine(DeleteScore(privatelvl3, name));
        StartCoroutine(DeleteScore(privatelvl4, name));
        StartCoroutine(DeleteScore(privatelvl5, name));
        lb5.UploadScores();
    }
    IEnumerator DeleteScore(string code, string name)
    {
        WWW www = new WWW(dreamloWebserviceURL + code + "/delete/" + WWW.EscapeURL(name));
        yield return www;        
    }
	/*
	IEnumerator GetSingleScore(string playerName)
	{
		highScores = "";
		WWW www = new WWW(dreamloWebserviceURL +  publicCode  + "/pipe-get/" + WWW.EscapeURL(playerName));
		yield return www;
		highScores = www.text;
	}
	*/
	public void LoadScores()
	{
		if (TooManyRequests()) return;
		StartCoroutine(GetScores());
	}

	
	public string[] ToStringArray(int L)
    {
        if (L == -2)
        {
            if (this.highScores02 == null) return null;
            if (this.highScores02 == "") return null;

            string[] rows = this.highScores02.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            return rows;
        }
        if (L == -1)
        {
            if (this.highScores01 == null) return null;
            if (this.highScores01 == "") return null;

            string[] rows = this.highScores01.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            return rows;
        }
        if (L == 0)
        {
            if (this.highScores0 == null) return null;
            if (this.highScores0 == "") return null;

            string[] rows = this.highScores0.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            return rows;
        }
        if (L == 1)
        {
            if (this.highScores1 == null) return null;
            if (this.highScores1 == "") return null;

            string[] rows = this.highScores1.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            return rows;
        }
        if (L == 2)
        {
            if (this.highScores2 == null) return null;
            if (this.highScores2 == "") return null;

            string[] rows = this.highScores2.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            return rows;
        }
        if (L == 3)
        {
            if (this.highScores3 == null) return null;
            if (this.highScores3 == "") return null;

            string[] rows = this.highScores3.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            return rows;
        }
        if (L == 4)
        {
            if (this.highScores4 == null) return null;
            if (this.highScores4 == "") return null;

            string[] rows = this.highScores4.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            return rows;
        }
        if (L == 5)
        {
            if (this.highScores5 == null) return null;
            if (this.highScores5 == "") return null;

            string[] rows = this.highScores5.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            return rows;
        }
        return null;
        
    }
	
	public List<Score> ToListLowToHigh(int N)
	{
		Score[] scoreList = this.ToScoreArray(N);
		
		if (scoreList == null) return new List<Score>();
		
		List<Score> genericList = new List<Score>(scoreList);
			
		genericList.Sort((x, y) => x.score.CompareTo(y.score));
		
		return genericList;
	}
	
	public List<Score> ToListHighToLow(int N)
	{
		Score[] scoreList = this.ToScoreArray(N);
		
		if (scoreList == null) return new List<Score>();

		List<Score> genericList = new List<Score>(scoreList);
			
		genericList.Sort((x, y) => y.score.CompareTo(x.score));
		
		return genericList;
	}
	
	public Score[] ToScoreArray(int E)
	{
		string[] rows = ToStringArray(E);
		if (rows == null) return null;
		
		int rowcount = rows.Length;
		
		if (rowcount <= 0) return null;
		
		Score[] scoreList = new Score[rowcount];
		
		for (int i = 0; i < rowcount; i++)
		{
			string[] values = rows[i].Split(new char[] {'|'}, System.StringSplitOptions.None);
			
			Score current = new Score();
			current.playerName = values[0];
			current.score = 0;
			current.seconds = 0;
			current.shortText = "";
			current.dateString = "";
			if (values.Length > 1) current.score = CheckInt(values[1]);
			if (values.Length > 2) current.seconds = CheckInt(values[2]);
			if (values.Length > 3) current.shortText = values[3];
			if (values.Length > 4) current.dateString = values[4];
			scoreList[i] = current;
		}
		
		return scoreList;
	}
	
	
	
	// Keep pipe and slash out of names
	
	string Clean(string s)
	{
		s = s.Replace("/", "");
		s = s.Replace("|", "");
        s = s.Replace("+", " ");
        return s;
		
	}
	
	int CheckInt(string s)
	{
		int x = 0;
		
		int.TryParse(s, out x);
		return x;
	}
	
}
