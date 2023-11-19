using System.Collections;
using System.Collections.Generic;
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
            StartCoroutine(EsperarCambioEscena());
            // Cuando la use en el avi�n cambiar de escena a la pantalla de ganar
            Debug.Log("Plane repaired successfully");
            // Eliminar la carta
            raycastOutput.gameObject.GetComponent<TutorialPlane>().Goal();

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

    IEnumerator EsperarCambioEscena()
    {
        yield return new WaitForSeconds(2.0f);
        SFXManager.Instance.CambiarM�sica(win, true);
        SceneManager.LoadScene("FinalScene");
    }
}
