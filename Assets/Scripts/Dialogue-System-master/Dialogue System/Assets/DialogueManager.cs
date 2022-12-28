using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour {

	//public Text nameText;
	public Text dialogueText;
	public Image _grandma;
	private Queue<string> sentences;
	private int spriteCounter;

	private Dialogue dialogueObject;
	private AudioSource audioData;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
		spriteCounter = 0;
		audioData = gameObject.AddComponent<AudioSource>();
	}

	public void StartDialogue (Dialogue dialogue)
	{
		//nameText.text = dialogue.name;
		sentences.Clear();
		dialogueObject = dialogue;
		_grandma.sprite = dialogue.characterSprite[spriteCounter];
		audioData.clip = dialogue.audio[spriteCounter];
		audioData.Play();
		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		_grandma.sprite = dialogueObject.characterSprite[spriteCounter];
		audioData.clip = dialogueObject.audio[spriteCounter];
		audioData.Play();
		spriteCounter++;
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		StopAllCoroutines();
		SceneManager.LoadScene("MainGame");
	}

}
