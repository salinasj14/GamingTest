using UnityEngine;
using System.Collections;
//attach this to an object in the unity file
//jose,earl,koobi
public class tableOperations : MonoBehaviour
{
	// this is the secret key it MUST 
	// be the same here as in the php file
	private string secretKey = "mySecretKey";
	//------------------------------------

	// the URL that leads to the php file MUST
	// have a '?' at the end
	static string URL = "http://gaminggroup.azurewebsites.net/?";
	//------------------------------------

	static string[] parsing;
	static char[] parser = {'|',';'};

	//adds a row into a certain table
	// public static IEnumerator makePlayer(string name, string TBName)
	// name is the name of a player
	// TBName is the name of the table that the player is to be added
	//---------------------------------------------------------------------
	public static IEnumerator makePlayer(string name, string TBName)
	{
		string post_url = URL + "operation=" + "makePlayer" + "&TBName=" + TBName + "&name=" + name;
		WWW hs_post = new WWW(post_url);
		yield return hs_post;
	}
	//---------------------------------------------------------------------

	// removes a row from a certain table
	// public static IEnumerator deletePlayer(string name, string TBName)
	// name is the name of a player
	// TBName is the name of the table that the player is to be removed
	public static IEnumerator deletePlayer(string name, string TBName)
	{
		string post_url = URL + "operation=" + "deletePlayer" + "&TBName=" + TBName +"&name=" + name ;
		WWW hs_post = new WWW(post_url);
		yield return hs_post;
	}

	//updates a certain row
	//---------------------------------------------------------------------
	//public static IEnumerator setTeam(string name, int NewValue, string TBName)
	// name is the name of the player to ba updated
	// NewValue is the value to which the team will be changed to.
	// TBName is the table in which the player exists
	public static IEnumerator setTeam(string name, int newValue, string TBName)
	{
		string post_url = URL + "operation=" + "updateTeam" + "&TBName=" + TBName + "&team=" + newValue + "&name=" + name;
		WWW hs_post = new WWW(post_url);
		yield return hs_post;
	}

	//public static IEnumerator incRound(string name, string TBName)
	// incraments the number of rounds won of a player in a table
	// name is the name of the player
	// TBName is the name of the table
	public static IEnumerator incRounds(string name, string TBName)
	{
		string post_url = URL + "operation=" + "incRounds" + "&TBName=" + TBName + "&name=" + name;
		WWW hs_post = new WWW(post_url);
		yield return hs_post;
	}

	//public static IEnumerator updateKill(string name, int NewValue, string TBName)
	// name; the name of the player to be updated
	// TBName; the name of the table in which the player exists
	// this function incraments by one
	public static IEnumerator updateKill(string name, string TBName)
	{
		string post_url = URL + "operation=" + "updateKill" + "&TBName=" + TBName + "&name=" + name;
		WWW hs_post = new WWW(post_url);
		yield return hs_post;
	}

	//public static IEnumerator updateDeath(string name, string TBName)
	// name; the name of the player to be updated
	// TBName; the name of the table in which the player exists
	// this function updates by one
	public static IEnumerator updateDeath(string name, string TBName)
	{
		string post_url = URL + "operation=" + "updateDeath" + "&TBName=" + TBName + "&name=" + name;
		WWW hs_post = new WWW(post_url);
		yield return hs_post;
	}

	//public static IEnumerator incScore(string name, int NewValue, string TBName)
	// name; the name of the player to be updated
	// TBName; the name of the table in which the player exists
	// NewValue; is the value that the score will be changed to
	public static IEnumerator incScores(string name, string TBName)
	{
		string post_url = URL + "operation=" + "incScores" + "&TBName=" + TBName + "&name=" + name;
		WWW hs_post = new WWW(post_url);
		yield return hs_post;
	}

	//public static IEnumerator updateScore(string name, int NewValue, string TBName)
	// name; the name of the player to be updated
	// TBName; the name of the table in which the player exists
	// NewValue; is the value that the score will be changed by
	// adds the given value to the current score.
	public static IEnumerator updateScores(string name, int newValue, string TBName)
	{
		string post_url = URL + "operation=" + "updateScore" + "&TBName=" + TBName + "&score=" + newValue + "&name=" + name;
		WWW hs_post = new WWW(post_url);
		yield return hs_post;
	}	

	//public static IEnumerator setScore(string name, int NewValue, string TBName)
	// name; the name of the player to be updated
	// TBName; the name of the table in which the player exists
	// NewValue; is the value that the score will be changed to
	public static IEnumerator setScores(string name, int newValue, string TBName)
	{
		string post_url = URL + "operation=" + "setScore" + "&TBName=" + TBName + "&score=" + newValue + "&name=" + name;
		WWW hs_post = new WWW(post_url);
		yield return hs_post;
	}	
	//----------------------------------------------------------------------

	//deletes a certain table
	//----------------------------------------------------------------------
	//public static IEnumerator deleteTable(string TBName)
	// TBName is the name of the table to be deleted
	public static IEnumerator deleteTable(string TBName)
	{
		string post_url = URL + "operation=" + "deleteTable" + "&TBName=" + TBName;
		WWW hs_post = new WWW(post_url);
		yield return hs_post;
	}
	//----------------------------------------------------------------------

	//creates table
	//----------------------------------------------------------------------
	//public static IEnumerator createT(string TBName)
	public static IEnumerator create(string TBName)
	{
		string post_url = URL + "operation=" + "create" + "&TBName=" + TBName;
		WWW hs_post = new WWW(post_url);
		yield return hs_post;
	}
	//---------------------------------------------------------------------

	//refreshing
	//---------------------------------------------------------------------
	//
	public static IEnumerator roundRefresh(string name, string TBName)
	{
		string post_url = URL + "operation=" + "roundRefresh" + "&TBName=" + TBName + "&name=" + name;
		WWW hs_post = new WWW(post_url);
		yield return hs_post;
	}

	public static IEnumerator gameRefresh(string name, string TBName)
	{
		string post_url = URL + "operation=" + "gameRefresh" + "&TBName=" + TBName + "&name=" + name;
		WWW hs_post = new WWW(post_url);
		yield return hs_post;
	}
	//---------------------------------------------------------------------

	// gets data from a certain table
	//----------------------------------------------------------------------
	public static IEnumerator showData(string TBName)
	{
		string post_url = URL + "operation=" + "showData" + "&TBName=" + TBName;
		WWW hs_post = new WWW(post_url);
		yield return hs_post;
		string temp = hs_post.text;
		parsing = temp.Split(parser);
	}

	public static IEnumerator largestRound(string TBName)
	{
		string post_url = URL + "operation=" + "largestRound" + "&TBName=" + TBName;
		WWW hs_post = new WWW(post_url);
		yield return hs_post;
		string temp = hs_post.text;
		parsing = temp.Split(parser);
	}

	public static string[] getData()
	{
		return parsing;
	}
	
	//public string[] dataGet(string TBName)
	//TBName is the table from which the data will be retrived.
	/*
	public string[] dataGet(string TBName)
	{
		StartCoroutine(show(TBName));
		return getData();
	}
	*/
	//----------------------------------------------------------------------
}