using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sceneloader : MonoBehaviour
{
    //Singleton (instance unique d'une classe) pour charger une scène en mode asychrone
    
    public static sceneloader instance;// Variable statique pour avoir accès au singleton facilement

    public GameObject[] objetsMasque; // objets à maquer lors du chargemement de la prochaine scène
    public GameObject[] objetsAffiche; // objets à afficher lors du chargement de la prochaine scène
    public TMP_Text textPourcentage;

    // Création du singleton : une fois seulemement et on le maintient vivant d'une scène à l'autre avec DontDestroyOnLoad.
    // Si le singleton existe déjà (à la deuxième scène par exemple) on supprime le gameObject.
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

    //Fonction qu'on peut appeler de partout comme ceci : instance.LoadScene("NomDeScène")
    //Démarre une coroutine pour le chargement asynchrone
    public void LoadScene(string nomScene)
    {
        StartCoroutine(GestionChargementScene(nomScene, 3f));
    }

    // IEnumerator pour gérer le chargement asynchrone.
    // Pause de 3 secondes au départ. Non nécessaire, mais utile pour ne pas que ça se passe trop vite!
    // Variable locale de type AsyncOperation dans lequel on met la référence à la scène qui se charge en mode async.
    // On boucle dans que l'opération async n'est pas complété.
    // On peut récupérer la progression du chargement (entre 0 et 1). En multipliant par 100, on obtient le % de chargement.

    IEnumerator GestionChargementScene(string nomScene, float delaiMinimal)
    {

        
        // Masque les objets courants de la scène
        foreach (GameObject obj in objetsMasque)
       {
           obj.SetActive(false);
       }
       // Affiche les objets à voir pendant le chargement de scène
       foreach(GameObject obj in objetsAffiche)
       {
           obj.SetActive(true);
       }
       // Durée minimale d'attente
        yield return new WaitForSeconds(delaiMinimal);

        // Chargement de la scène en arrière-plan
        // La scène se lancera automatiquement quand le chargement sera complété.
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(nomScene);
       
        while (!asyncLoad.isDone)
        {
            textPourcentage.text = ""+asyncLoad.progress*100+"%";
            yield return null;
        }
    
        yield return null;
    }

}
