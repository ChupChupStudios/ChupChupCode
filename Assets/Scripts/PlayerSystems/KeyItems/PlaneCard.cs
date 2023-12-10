using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneCard : ACard
{
    public LayerMask plane;
    private GameObject raycastOutput;
    [SerializeField] private AudioClip repair;
    [SerializeField] private AudioClip win;

    public override void HideEffectArea()
    {
        affectedBlocks.ForEach(tile => tile.GetComponent<Block>().ResetColor());
        affectedBlocks.Clear();
    }

    public override void CheckAndExecute(Block tile)
    {
        if (!affectedBlocks.Contains(tile.gameObject)) return;

        if (Utils.CustomRaycast(tile.gameObject.transform.position + Vector3.down, Vector3.up, out raycastOutput, plane))
        {
            SFXManager.Instance.EjecutarSonido(repair);
            
            // Cuando la use en el avión cambiar de escena a la pantalla de ganar
            Debug.Log("Plane repaired successfully");
            
            // si se gasta la carta en el tutorial:
            if (SceneManager.GetActiveScene().name == "Tutorial")
                raycastOutput.gameObject.GetComponent<TutorialPlane>().Goal();
            // si es en un nivel normal:
            else
                CambioEscenaVictoria();


            // Eliminar la carta
            deckManager.Deselect();
            Destroy(gameObject);
        }
        deckManager.Deselect();

    }

    public override void ShowEffectArea()
    {
        GameObject tile;
        if (!deckManager.BloqueUsuarioDelMazo(deckManager.ownerTransform.forward, out tile)) return;
        affectedBlocks.Add(tile);
        tile.GetComponent<Block>().ChangeColor(baseColor);
    }

    void CambioEscenaVictoria()
    {
        Debug.Log("cambiar a victoria");
        SFXManager.Instance.CambiarMúsica(win, true);
        SceneManager.LoadScene("FinalScene");
    }

    //IEnumerator EsperarCambioEscena()
    //{
    //    Debug.Log("empezar corrutina");
    //    yield return new WaitForSeconds(2.0f);
    //    Debug.Log("tiempo transcurrido");
    //    SFXManager.Instance.CambiarMúsica(win, true);
    //    SceneManager.LoadScene("FinalScene");
    //}
}
