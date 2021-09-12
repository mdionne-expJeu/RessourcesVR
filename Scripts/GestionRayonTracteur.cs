using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // acc?s aux objets du XR Interaction Toolkit
using UnityEngine.InputSystem; // pour utiliser le nouveau InputSyteme
using UnityEngine.Events;
using System;

/*
 * Description g?n?rale
 * Script simple qui d'activer et de d?sactiver le rayon tracteur lorsqu'on appuie/relache le bouton grip du contr?leur
 * Mathieu Dionne
 * Derni?re modifications : 12 septembre 2021
 */

public class GestionRayonTracteur : MonoBehaviour
{
    // [SerializeField] Permet de "s?rialiser" une variable priv?e. Elle sera accessible dans l'inspecteur de Unity
    // l'objet qui poss?dent le rayon tracteur (component XRRayInteractor, XRInteractorLineVisual, etc.)
    [SerializeField]
    GameObject ControleurTracteur;

    // L'action du contr?leur qui active/d?sactive le rayon. Peut ?tre autre chose que le grip. Action ? d?finir dans le tableau InputAction
    [SerializeField]
    InputActionReference inputActionReference_ActiveGrip; 



  
    private void OnEnable()
    {
        // s'ex?cute lorsque le script devient actif (enable)
        // ajout de la fonction qui sera appel?e lorsque l'action sera effectu?e
        inputActionReference_ActiveGrip.action.performed += ActiveRayon;
    }

    private void OnDisable()
    {
        // s'ex?cute lorsque le script devient inactif (disable)
        // ajout de la fonction qui sera appel?e lorsque l'action sera effectu?e
        inputActionReference_ActiveGrip.action.performed -= DesactiveRayon;
    }

    private void Start()
    {

        //Par d?faut, on d?sactive le rayon
        ControleurTracteur.GetComponent<XRRayInteractor>().enabled = false;
        ControleurTracteur.GetComponent<XRInteractorLineVisual>().enabled = false;
    }

    // M?thode qui s'ex?cute lorsque l'action sera effectu?e (typiquement enfoncer le bouton grip).
	// Dans le tableau InputAction,l'action doit ?tre configur?e de type "button" et le binding avec un TriggerBehavior ? PressAndRelease.
    // Lorsque la m?thode sera appel?e, le param?tre obj sera ?gal ? 1f (bouton enfonc?) ou 0f (bouton rel?ch?)
    private void ActiveRayon(InputAction.CallbackContext obj)
    {
       
        // Bouton enfonc?, on active le rayon
        // ReadValue<float> permet de r?cuper la valeur de type float contenu dans le param?tre obj
        if (obj.ReadValue<float>() == 1f)
        {
            ControleurTracteur.GetComponent<XRRayInteractor>().enabled = true;
            ControleurTracteur.GetComponent<XRInteractorLineVisual>().enabled = true;
        }
        // Bouton relach?, on d?sactive le rayon
        else if (obj.ReadValue<float>() == 0f)
        {
            ControleurTracteur.GetComponent<XRRayInteractor>().enabled = false;
            ControleurTracteur.GetComponent<XRInteractorLineVisual>().enabled = false;
        }
    }
}
