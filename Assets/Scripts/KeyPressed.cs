using UnityEngine;

public class KeyPressed : MonoBehaviour
{

    TypingController gameController;

    private void Start()
    {
        gameController = this.gameObject.GetComponent<TypingController>();
    }

    /* Keyboard pressed functions */
    /* ========================================================== */
    #region
    public void QPressed()
    {
        gameController.KeyPressed('q');
    }

    public void WPressed()
    {
        gameController.KeyPressed('w');
    }

    public void EPressed()
    {
        gameController.KeyPressed('e');
    }

    public void RPressed()
    {
        gameController.KeyPressed('r');
    }

    public void TPressed()
    {
        gameController.KeyPressed('t');
    }

    public void YPressed()
    {
        gameController.KeyPressed('y');
    }

    public void UPressed()
    {
        gameController.KeyPressed('u');
    }

    public void IPressed()
    {
        gameController.KeyPressed('i');
    }

    public void OPressed()
    {
        gameController.KeyPressed('o');
    }

    public void PPressed()
    {
        gameController.KeyPressed('p');
    }

    public void APressed()
    {
        gameController.KeyPressed('a');
    }

    public void SPressed()
    {
        gameController.KeyPressed('s');
    }

    public void DPressed()
    {
        gameController.KeyPressed('d');
    }

    public void FPressed()
    {
        gameController.KeyPressed('f');
    }

    public void GPressed()
    {
        gameController.KeyPressed('g');
    }

    public void HPressed()
    {
        gameController.KeyPressed('h');
    }

    public void JPressed()
    {
        gameController.KeyPressed('j');
    }

    public void KPressed()
    {
        gameController.KeyPressed('k');
    }

    public void LPressed()
    {
        gameController.KeyPressed('l');
    }

    public void ZPressed()
    {
        gameController.KeyPressed('z');
    }

    public void XPressed()
    {
        gameController.KeyPressed('x');
    }

    public void CPressed()
    {
        gameController.KeyPressed('c');
    }

    public void VPressed()
    {
        gameController.KeyPressed('v');
    }

    public void BPressed()
    {
        gameController.KeyPressed('b');
    }

    public void NPressed()
    {
        gameController.KeyPressed('n');
    }

    public void MPressed()
    {
        gameController.KeyPressed('m');
    }
    /* ========================================================== */
    #endregion

    public void BackSpacePressed()
    {
        gameController.BackSpacePressed();
    }

    public void EnterPressed()
    {
        gameController.EnterPressed();
    }
}
