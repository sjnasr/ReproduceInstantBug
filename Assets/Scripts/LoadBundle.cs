using UnityEngine;
using System.Collections;

public class LoadBundle : MonoBehaviour {
	public string bundleUrl;

	AssetBundle bundle;

	IEnumerator Start(){
		WWW www = new WWW (bundleUrl);
		yield return www;
		if(www.error != null){
			throw new System.Exception("There's an error " + www.error);
		}

		//bundle = www.assetBundle;
		bundle = AssetBundle.LoadFromMemory(www.bytes);
	}

	public IEnumerator Spawn(string assetName){
		if (assetName == "") {
			Instantiate (bundle.mainAsset);
		} else {
			Debug.LogWarning ("Asset Name" + assetName);
			AssetBundleRequest request= bundle.LoadAssetAsync(assetName, typeof(GameObject));
			Debug.LogWarning ("Asset Name " + assetName);
			//yield return new WaitForSeconds(0.5f);
			/*while(!request.isDone) {
				//  // TODO: hook up asyncRequest.progress to loading bar animation
				  //yield return null;
				}*/

			GameObject obj = request.asset as GameObject;

			GameObject newObject = Instantiate (obj) as GameObject;

			newObject.transform.parent = this.transform;

			Resources.UnloadUnusedAssets ();

		}

		yield return null;
	}

	public void SpawnInstant(string assetName){
		if (assetName == "") {
			Instantiate (bundle.mainAsset);
		} else {
			AssetBundleRequest request= bundle.LoadAssetAsync(assetName, typeof(GameObject));
			Debug.LogWarning ("Asset Name " + assetName);


			GameObject obj = request.asset as GameObject;

			GameObject newObject = Instantiate (obj) as GameObject;

			newObject.transform.parent = this.transform;



			Resources.UnloadUnusedAssets ();

		}
			
	}


	public IEnumerator SpawnSlowly(){
		yield return StartCoroutine( Spawn ("GroundSection"));
		yield return StartCoroutine(Spawn ("QuestionBlock"));
		yield return StartCoroutine(Spawn ("BrickBackground"));
		yield return StartCoroutine(Spawn ("DarkGround"));
		//yield return StartCoroutine(Spawn ("GreenPipe"));
		yield return StartCoroutine(Spawn ("CloudBackground"));
		//yield return StartCoroutine(Spawn ("PipeColliderScript"));
		yield return StartCoroutine(Spawn ("Cube1"));
		yield return StartCoroutine(Spawn ("Cube"));
		//yield return StartCoroutine(Spawn ("ThirdPersonController"));
	}

	public void SpawnInstantly(){
		SpawnInstant ("GroundSection");
		SpawnInstant ("QuestionBlock");
		SpawnInstant ("BrickBackground");
		SpawnInstant ("DarkGround");
		//SpawnInstant ("GreenPipe");
		SpawnInstant ("CloudBackground");
		//SpawnInstant ("PipeColliderScript");
		SpawnInstant ("Cube1");
		SpawnInstant ("Cube");
		//SpawnInstant ("ThirdPersonController");
	}

	public void StartSpawn(){
		StartCoroutine (SpawnSlowly ());
	}
}
