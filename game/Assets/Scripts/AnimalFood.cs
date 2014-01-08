using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimalFood : MonoBehaviour {

	private const float maxDistanceToFood = 3.0f;
	private const float maxDistanceToFoodBasket = 3.0f;


	private List<FoodBasket> foodBaskets;
	List<Food> foods;
	private Food foodInHands = null;

	private GameObject player;


	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");

		// food baskets
		foodBaskets = new List<FoodBasket>();

		GameObject fence = GameObject.Find("Fences");
		foreach (Transform breedFence in fence.transform) {
			Transform foodBasket = breedFence.Find("Food basket");
			string animalBreed = breedFence.name.Substring(0, breedFence.name.Length - "Fence".Length);
			foodBaskets.Add(new FoodBasket(foodBasket.gameObject, animalBreed));
		}


		// foods
		foods = new List<Food>();

		GameObject food = GameObject.Find("Food");
		foreach (Transform animalFood in food.transform) {
			string animalBreed = animalFood.name.Substring(0, animalFood.name.Length - "Food".Length);
			string description = "Food description."; // TODO
			foods.Add(new Food(animalFood.gameObject, animalBreed, description));
		}
	}


	// Update is called once per frame
	void Update () {
		Vector3 playerPosition = player.transform.position;


		// play sound for next animal
		GameObject nextAnimal = GameObject.Find(foods[0].animalBreed + "M");
		if (!nextAnimal.audio.isPlaying) {
			nextAnimal.audio.Play();
		}



		Food closestFood = null;
		float closestFoodDistance = float.MaxValue;

		// find closest food
		foreach (Food food in foods) {
			Vector3 foodPosition = food.gameObject.transform.position;
			float dist = Vector3.Distance(playerPosition, foodPosition);
			if (dist < closestFoodDistance) {
				closestFood = food;
				closestFoodDistance = dist;
			}
		}


		FoodBasket closestFoodBasket = null;
		float closestFoodBasketDistance = float.MaxValue;

		// find closest food basket
		foreach (FoodBasket foodBasket in foodBaskets) {
			Vector3 foodBasketPosition = foodBasket.gameObject.transform.position;
			float dist = Vector3.Distance(playerPosition, foodBasketPosition);
			if (dist < closestFoodBasketDistance) {
				closestFoodBasket = foodBasket;
				closestFoodBasketDistance = dist;
			}
		}


		// 
		if (Input.GetButtonDown("Lift")) {
			Debug.Log("AnimalFood: button down (Lift)");
			if (foodInHands != null) {
				if (closestFoodBasketDistance <= maxDistanceToFoodBasket) {
					if (foodInHands.animalBreed.Equals(closestFoodBasket.animalBreed)) {
						// TODO: change food basket to full
						//foodInHands.gameObject.SetActive(false);
						foodInHands.gameObject.transform.position = closestFoodBasket.gameObject.transform.position;

						foods.Remove(foodInHands);
						if (foods.Count == 0) {
							Completed();
						}
					}
				}
				foodInHands = null;
			} else {
				if (closestFoodDistance <= maxDistanceToFood) {
					if (closestFood.animalBreed.Equals(foods[0].animalBreed)){
						foodInHands = closestFood;
					} else {
						// wrong food
						Debug.Log("AnimalFood: wrong food");
					}
				}
			}
		}

		if (foodInHands != null) {
			Vector3 tmp = playerPosition;
			tmp.x += 2.0f; // TODO
			foodInHands.gameObject.transform.position = tmp;
			foodInHands.gameObject.transform.rotation = Quaternion.RotateTowards(foodInHands.gameObject.transform.rotation, player.transform.rotation, 10);
		} else {
			// TODO: maybe show food description and disable AnimalProximity script
		}
	}


	void Completed () {
		this.enabled = false;
		Debug.Log("AnimalFood: all animals fed");
		// TODO: add next objective
	}


	// show user current task
	void OnGUI () {
		GUI.Box(new Rect(20 + 3 * 90 + 20, 10, 3 * 90, 25), "Feed the animal you hear.");
	}
}


public class FoodBasket {
	public GameObject gameObject;
	public string animalBreed;
	public bool isEmpty;

	public FoodBasket (GameObject gameObject, string animalBreed) {
		this.gameObject = gameObject;
		this.animalBreed = animalBreed;
		this.isEmpty = true;
	}
}


public class Food {
	public GameObject gameObject;
	public string animalBreed;
	public string description;

	public Food (GameObject gameObject, string animalBreed, string description) {
		this.gameObject = gameObject;
		this.animalBreed = animalBreed;
		this.description = description;
	}
}
