using UnityEngine;
using System.Collections;

public class ScoreHolder {

	private static int score;
	private static ExitState exitState;

	public enum ExitState {Won, Lost, Quit};

	public static void exitQuit() {
		score = 0;
		exitState = ExitState.Quit;
	}

	public static void exitWin(int newScore) {
		score = newScore;
		exitState = ExitState.Won;
	}

	public static void exitLose(int newScore) {
		score = newScore;
		exitState = ExitState.Lost;
	}

	public static int getScore() {
		return score;
	}

	public static ExitState getExitState() {
		return exitState;
	}
}
