using UnityEngine;
using System.Collections;
using System.Net;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public static MainMenu instance = null;
    public GameObject canvasMainMenu;
    public GameObject gameManager;

    private float levelStartDelay = 2f;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a MainMenu.
            Destroy(gameObject);
    }

    //public void StartGame(string dificultad)
    public void StartGame(string difficulty)
    {
        gameManager = Instantiate(gameManager);
        GameManager.instance.InitGame();
        GameManager.instance.SetDifficultylvl(difficulty); //NO SE PUEDE SETEAR ASI COMO PENSABA
        Invoke("HideMainMenu", levelStartDelay);
    }

    private void HideMainMenu() //NO SE QUE HACE ESTA GOMA
    {
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
