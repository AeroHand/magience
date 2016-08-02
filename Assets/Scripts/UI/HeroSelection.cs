using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroSelection : MonoBehaviour {

    public Image heroimage;
    public Text heroname;


    public Sprite Accelerator;
    public Sprite PlaceholderHeroImg;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void acceleratorenter() {
        heroimage.sprite = Accelerator;
        heroname.text = "Acceleration";
    }

    public void placeholderheroenter() {
        heroimage.sprite = PlaceholderHeroImg;
        heroname.text = "Coming Soon..";
    }

    public void placeholderheroexit() {

    }
}
