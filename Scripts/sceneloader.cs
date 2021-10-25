using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sceneloader : MonoBehaviour
{
    //Singleton (instance unique d'une classe) pour charger une sc�ne en mode asychrone
    
    public static sceneloader instance;// Variable statique pour avoir acc�s au singleton facilement

    public GameObject[] objetsMasque; // objets � maquer lors du chargemement de la prochaine sc�ne
    public GameObject[] objetsAffiche; // objets � afficher lors du chargement de la prochaine sc�ne
    public TMP_Text textPourcentage;

    // Cr�ation du singleton : une fois seulemement et on le maintient vivant d'une sc�ne � l'autre avec DontDestroyOnLoad.
    // Si le singleton existe d�j� (� la deuxi�me sc�ne par exemple) on supprime le gameObject.
    private void Awake()
    {
        if(instance!=null && instance != this)
        { 
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //Fonction qu'on peut appeler de partout comme ceci : instance.LoadScene("NomDeSc�ne")
    //D�marre une coroutine pour le chargement asynchrone
    public void LoadScene(string nomScene)
    {
        StartCoroutine(GestionChargementScene(nomScene, 3f));
    }

    // IEnumerator pour g�rer le chargement asynchrone.
    // Pause de 3 secondes au d�part. Non n�cessaire, mais utile pour ne pas que �a se passe trop vite!
    // Variable locale de type AsyncOperation dans lequel on met la r�f�rence � la sc�ne qui se charge en mode async.
    // On boucle dans que l'op�ration async n'est pas compl�t�.
    // On peut r�cup�rer la progression du chargement (entre 0 et 1). En multipliant par 100, on obtient le % de chargement.

    IEnumerator GestionChargementScene(string nomScene, float delaiMinimal)
    {

        
        // Masque les objets courants de la sc�ne
        foreach (GameObject obj in objetsMasque)
       {
           obj.SetActive(false);
       }
       // Affiche les objets � voir pendant le chargement de sc�ne
       foreach(GameObject obj in objetsAffiche)
       {
           obj.SetActive(true);
       }
       // Dur�e minimale d'attente
        yield return new WaitForSeconds(delaiMinimal);

        // Chargement de la sc�ne en arri�re-plan
        // La sc�ne se lancera automatiquement quand le chargement sera compl�t�.
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(nomScene);
       
        while (!asyncLoad.isDone)
        {
            textPourcentage.text = ""+asyncLoad.progress*100+"%";
            yield return null;
        }
    
        yield return null;
    }

}
