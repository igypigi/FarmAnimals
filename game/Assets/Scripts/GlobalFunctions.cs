using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SimpleJSON;

public class GlobalFunctions {
	// List of all animals
	public static List<Animal> animals = readAnimals();

	public static void switchCamera (string cameraName) {
		// Loop through all cameras in scene
		foreach (GameObject cam in GameObject.FindGameObjectsWithTag("Camera")) {
			Camera theCam = cam.GetComponent<Camera>() as Camera;

			// Enable cameraName and disable other
			theCam.enabled = cam.name == cameraName;
		}  
	}

	private static List<Animal> readAnimals () {
		List<Animal> list = new List<Animal>();
		string[] animalTypes = new string[] { "male", "female", "child" };

		// Read JSON
		TextAsset text = Resources.Load("animals") as TextAsset;
		string json = text.text;
		JSONNode allAnimals = JSON.Parse(json)["animals"];
		int index =  0;
		// Loop through all the animals
		while (true) {
			JSONNode animal = allAnimals[index++];
			if (animal == null) break;
			// Add animal types -> male, female, child
			foreach(string type in animalTypes) {
				list.Add(new Animal(
					animal["breed"], 
					animal[type]["name"], 
					animal[type]["objectName"], 
					animal[type]["description"]
				));
			}
		}
		return list;
	}
}


// Animal class
public class Animal {
	public string breed;
	public string name;
	public string objectName;
	public string description;

	public GameObject gameObject;
	public Texture image;
	
	public Animal (string breed, string name, string objectName, string desc) {
		this.breed = breed;
		this.name = name;
		this.objectName = objectName;
		this.description = desc;
		this.gameObject = GameObject.Find(objectName);
		this.image = this.gameObject.GetComponent<AnimalMove>().animalImage;
	}

	public static Animal getAnimalByObjectName (string name) {
		foreach (Animal animal in GlobalFunctions.animals) {
			if (animal.objectName == name) {
				return animal;
			}
		}
		return null;
	}
}