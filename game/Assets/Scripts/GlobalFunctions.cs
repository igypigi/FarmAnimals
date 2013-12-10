using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalFunctions {
	// List of all animals
	public static List<Animal> animals = readAnimals();

	public static void switchCamera (string cameraName) {
		Debug.Log("Switching to camera " + cameraName);

		// Loop through all cameras in scene
		foreach (GameObject cam in GameObject.FindGameObjectsWithTag("Camera")) {
			Camera theCam = cam.GetComponent<Camera>() as Camera;

			// Enable cameraName and disable other
			if (cam.name == cameraName) {
				theCam.enabled = true;
			} else {
				theCam.enabled = false;
			}
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

		return list;
	}
}


// Animal class
public class Animal
{	
	//Properties. 
	public string Breed { get; set; }
	public string Name { get; set; }
	public string ObjectName { get; set; }
	public string Description { get; set; }
	public bool inBarn { get; set; }
	
	public Animal (string breed, string name, string objectName, string desc)
	{
		this.Breed = breed;
		this.Name = name;
		this.ObjectName = objectName;
		this.Description = desc;
		this.inBarn = true;
	}
}