using UnityEngine;
using System.Collections;

public partial class GameManager : MonoBehaviour {
public void GenerateMeteors()
    {
        CancelInvoke("SpawnBigMeteor");
        CancelInvoke("SpawnSmallMeteor");
		Meteors.meteorSpeed = -1f;
        InvokeRepeating("SpawnBigMeteor", 0.8f, 1f);
        InvokeRepeating("SpawnSmallMeteor", 0.5f, 1f);

    }

    public void SpawnBigMeteor()
    {
        if (this.gs == gameState.running)
        {
            NGUITools.AddChild(meteorHolder, meteorBig);
        }
    }

    public void SpawnSmallMeteor()
    {
        if (this.gs == gameState.running)
        {
            var go = NGUITools.AddChild(meteorHolder, meteorSmall);
            var randY = Random.Range(-150, -200);
            var position = new Vector3(0, (float)randY, 0);

            go.transform.localPosition = position;
        }
    }

	public void DestroyMeteors()
	{
		while (meteorHolder.gameObject.transform.childCount > 0)
			NGUITools.Destroy(meteorHolder.transform.GetChild(0).gameObject);
	}

    public void MeteorExplosion(Vector3 position)
    {
        instantiate(meteorDestroyParticle, position);
    }

    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(prefab, position, Quaternion.identity) as ParticleSystem;
        Destroy(newParticleSystem.gameObject, newParticleSystem.startLifetime);
        return newParticleSystem;
    }

    IEnumerator MeteorDifficulty()
    {
        while (true)
        {
            yield return new WaitForSeconds(7f);

            if (this.gs == gameState.running)
            {
                Time.timeScale += 0.075f;
            }
        }
    }
    	public void StopMeteors()
	{
		CancelInvoke("SpawnBigMeteor");
		CancelInvoke("SpawnSmallMeteor");
		Debug.LogError ("Stoping meteors!");
	}
}
