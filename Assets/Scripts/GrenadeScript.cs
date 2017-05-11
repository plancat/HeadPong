using UnityEngine;
using System.Collections;

public class GrenadeScript : MonoBehaviour {

	[Header("Timer")]
	//Time before the grenade explodes
	public float grenadeTimer = 5.0f;

    [Header("Explosion Prefabs")]
    //All explosion prefabs
    public Transform explosion;

	[Header("Explosion Options")]
	public float radius = 25.0F;
	public float power = 350.0F;

	[Header("Throw Force")]
	public float minimumForce = 350.0f;
	public float maximumForce = 850.0f;
	float throwForce;
 
	[Header("Audio")]
	public AudioSource impactSound;

	void Awake () {
		throwForce = Random.Range(minimumForce, maximumForce);

        GetComponent<Rigidbody>().AddRelativeTorque (
			Random.Range(500, 1500), //X Axis
			Random.Range(0,0), //Y Axis
			Random.Range(0,0)  //Z Axis
			* Time.deltaTime * 5000);
	}

	void Start () {
		GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * throwForce);
	}

	void OnCollisionEnter (Collision collision) {
		impactSound.Play ();
	}

	IEnumerator ExplosionTimer () {
		yield return new WaitForSeconds(grenadeTimer);

		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody> ();

			if (rb != null)
				rb.AddExplosionForce (power, explosionPos, radius, 3.0F);
			
			//********** USED IN THE DEMO SCENES **********
			//If the explosion hit the tags "Target", and if "isHit" 
			//is false on the target
			// if (hit.GetComponent<Collider>().tag == "Target" 
			//     	&& hit.gameObject.GetComponent<TargetScript>().isHit == false) {
			// 	
			// 	//Animate the target 
			// 	hit.gameObject.GetComponent<Animation> ().Play("target_down");
			// 	//Toggle the isHit bool on the target
			// 	hit.gameObject.GetComponent<TargetScript>().isHit = true;
			// }
            // 
			// //********** USED IN THE DEMO SCENES **********
			// //Used for flashbang effect on the player tag
			// if (isFlashbang == true) {
			// 	if (hit.GetComponent<Collider> ().tag == "Player" 
			// 			&& hit.gameObject.GetComponent<FlashbangEffectScript>().isBlinded == false) {
			// 		//If the player is near the flashbang, start flashbang effect
			// 		hit.gameObject.GetComponent<FlashbangEffectScript> ().isBlinded = true;
            // 
			// 	}
			// }
		}

		//Destroy the grenade on explosion
		Destroy (gameObject);
	}
}