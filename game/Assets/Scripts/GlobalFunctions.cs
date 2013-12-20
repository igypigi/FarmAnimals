using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		// TODO: read animals from JSON or XML file
		/*
		animals.xml or json
		<breed>
			<male>
			<female>
			<child>
				<name>
				<object name>
				<desription>
			</child>
		</breed>
		<breed>....
		 */

		list.Add(new Animal("Pig", "Pig", "PigM", "This is a male pig"));
		list.Add(new Animal("Pig", "Sow", "PigF", "This is a female pig"));
		list.Add(new Animal("Pig", "Piglet", "PigC", "This is a child pig"));

		list.Add(new Animal("Sheep", "Ram", "SheepM", "This is a male sheep"));
		list.Add(new Animal("Sheep", "Sheep", "SheepF", "This is a female sheep"));
		list.Add(new Animal("Sheep", "Lamb", "SheepC", "This is a child sheep"));

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
	
	public Animal (string breed, string name, string objectName, string desc) {
		this.breed = breed;
		this.name = name;
		this.objectName = objectName;
		this.description = desc;
		this.gameObject = GameObject.Find(objectName);
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