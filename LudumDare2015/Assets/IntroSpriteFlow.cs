﻿using UnityEngine;
using System.Collections;

public class IntroSpriteFlow : MonoBehaviour
{
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;
	public Sprite sprite5;
	
	private bool sceneStarting = true;      // Whether or not the scene is still fading in.
	private int sceneEnding = 0;
	private SpriteRenderer spriteRenderer;

	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
	}

	void Awake ()
	{
		// Set the texture so that it is the the size of the screen and covers it.
		GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}
	
	
	void Update ()
	{
		if (Input.anyKey) {
			if(sceneEnding <5){
			sceneEnding++;
			}
			//Application.LoadLevel();
		}


		// If the scene is starting...
		if (sceneStarting) {
			// ... call the StartScene function.
			StartScene ();
		}

		//Sprite image navigation
		if (sceneEnding == 5) {
			EndScene ();
		}

		else if (sceneEnding == 1) {
			spriteRenderer.sprite = sprite2;
		}
		else if (sceneEnding == 2) {
			spriteRenderer.sprite = sprite3;
		}
		else if (sceneEnding == 3) {
			spriteRenderer.sprite = sprite4;
		}
		else if (sceneEnding == 4) {
			spriteRenderer.sprite = sprite5;
		}

	}
	
	
	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	
	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
	}
	
	
	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(GetComponent<GUITexture>().color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			GetComponent<GUITexture>().color = Color.clear;
			GetComponent<GUITexture>().enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;
		}
	}
	
	
	public void EndScene ()
	{
		// Make sure the texture is enabled.
		GetComponent<GUITexture>().enabled = true;
		
		// Start fading towards black.
		FadeToBlack();
		
		// If the screen is almost black...
		if(GetComponent<GUITexture>().color.a >= 0.95f)
			// ... reload the level.
			Application.LoadLevel("Scene1");
	}
}