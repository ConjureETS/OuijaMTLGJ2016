using UnityEngine;
using System.Collections;

public class GameState {

	private static GameState instance = new GameState();
	public static GameState Instance { get { return instance; } }

	public LevelManager currentLevel = null;

	public int numPlayers = 3;
	public int wordLength = 5;
	public int numRows = 13;
	public int numColumns = 5;
	public Player[] players;

	private GameState()
	{
		players = new Player[numPlayers];
		for (int i = 0; i < numPlayers; i++)
		{
			players[i] = new Player(i+1);
		}
	}
}
