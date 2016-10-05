using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists. 
using UnityEngine.UI;                   //Allows us to use UI.

public class GameManager : MonoBehaviour
{
	public ScriptableObject selectedCharacter;
    public GameObject playerPrefab;
    public GameObject boardManager;

    public GameObject canvasPlayerUI;
    public GameObject panelPlayers;

    public float levelStartDelay = 2f;                      //Time to wait before starting level, in seconds.
    public float turnDelay = 0.1f;                          //Delay between each Player turn.
    
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public bool playersTurn = true;     //Boolean to check if it's players turn, hidden in inspector but public.

    private string difficulty;
   
    //private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
    
    private int level = 1;                                  //Current level number, expressed in game as "Day 1".
    private List<Enemy> listaEnemies;                       //List of all Enemy units, used to issue them move commands.
    private List<Player> listaPlayers;
    private bool enemiesMoving;                             //Boolean to check if enemies are moving.
    private bool doingSetup = true;                         //Boolean to check if we're setting up board, prevent Player from moving during setup.

    private List<MovingObject> listaTodasLasEntidadesQueTenganTurnos;
    private int quienJuega; //Segun el indice de la lista de todos los que juegan 

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
       
        DontDestroyOnLoad(gameObject);
        listaTodasLasEntidadesQueTenganTurnos = new List<MovingObject>();
        listaEnemies = new List<Enemy>();  
		listaPlayers = new List<Player>();     
    }
   
    private void SetCanvasPlayerUi()
    {
        canvasPlayerUI = Instantiate(canvasPlayerUI);
        panelPlayers = GameObject.Find("PanelPlayers");
    }

    //Initializes the game for each level.
	public void InitGame()
    {
        //While doingSetup is true the player can't move, prevent player from moving while title card is up.
        doingSetup = true;
        Camera.main.orthographicSize = 12; //Setea la escala de la camara
        //Clear any Enemy objects in our List to prepare for next level.
        listaTodasLasEntidadesQueTenganTurnos.Clear();
        listaEnemies.Clear();
		SetCanvasPlayerUi();
		SetBoardManager();
        Invoke("HideLevelImage", levelStartDelay);
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        //boardScript.Start();
    }


    //Hides black image used between levels
    void HideLevelImage()
    {
        //Set doingSetup to false allowing player to move again.
        doingSetup = false;
    }

    //Update is called every frame.
    void Update()
    {
        //Check that playersTurn or enemiesMoving or doingSetup are not currently true.
        if (playersTurn || enemiesMoving || doingSetup)
            //If any of these are true, return and do not start MoveEnemies.
            return;
        //Start moving enemies.
        StartCoroutine(MoveEnemies());
    }

    //Call this to add the passed in Enemy to the List of Enemy objects.
    public void AddEnemyToList(Enemy script)
    {
        //Add Enemy to List enemies.
        listaEnemies.Add(script);
    }


    //GameOver is called when the player reaches 0 food points
    public void GameOver()
    {
        //Set levelText to display number of levels passed and game over message
        //levelText.text = "After " + level + " days, you starved.";

        //Enable black background image gameObject.
        //levelImage.SetActive(true);

        //Disable this GameManager.
        enabled = false;
    }

    //Coroutine to move enemies in sequence.
    IEnumerator MoveEnemies()
    {
        //While enemiesMoving is true player is unable to move.
        enemiesMoving = true;

        //Wait for turnDelay seconds, defaults to .1 (100 ms).
        yield return new WaitForSeconds(turnDelay);

        //COMO SE MUEVEN LOS ENEMIGOS TENDRIA QUE SER OTRA CLASE Y QUE LLAME ESOS METODOS
        //If there are no enemies spawned (IE in first level):      
        if (listaEnemies.Count == 0)
        {
            //Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
            yield return new WaitForSeconds(turnDelay);
        }      
        //Loop through List of Enemy objects.
        for (int i = 0; i < listaEnemies.Count; i++)
        {
            //Call the MoveEnemy function of Enemy at index i in the enemies List.
            listaEnemies[i].MoveEnemy();

            //Wait for Enemy's moveTime before moving next Enemy, 
            yield return new WaitForSeconds(listaEnemies[i].moveTime);
        }
        //Enemies are done moving, set enemiesMoving to false.
        enemiesMoving = false;
        //Once Enemies are done moving, set playersTurn to true so player can move.
        playersTurn = true;
        yield return new WaitForSeconds(turnDelay);
    }

    public void SetDifficultylvl(string selectedDifficulty)
    {
        difficulty = selectedDifficulty;
        Debug.Log("La dificultad seteada es: " + difficulty);
    }

    public void InstanciarJugadores(/*ScriptableObject[] nuevosJugadores*/)
    {
        //listaPlayers = new List<Player>();

        //foreach (ScriptableObject datosJugador in nuevosJugadores)
        //{
			
		Player player = Instantiate(playerPrefab).GetComponent<Player>();
		this.SetPlayerStats(player);
		GameManager.instance.listaPlayers.Add(player);
        listaTodasLasEntidadesQueTenganTurnos.Add(player);
        //}
    }

	public void SetPlayerStats(Player player) 
    {
		Character c = (Character)GameManager.instance.selectedCharacter;	
		player.name = c.name;
        player.characterPortrait = c.characterPortraitSprite;

        player.currentActionPoints = c.currentActionPoints;

        player.classType = c.classType;
        player.level = c.level;
        player.experience = c.experience;
        player.experienceRequiered = c.experienceRequiered;

        player.strength = c.strength;
        player.agility = c.agility;
        player.wisdom = c.wisdom;

        player.maxHealth = c.maxHealth;
        player.maxEnergy = c.maxEnergy;
        player.maxMana = c.maxMana;
        player.currentHealth = c.currentHealth;
        player.currentEnergy = c.currentEnergy;
        player.currentMana = c.currentMana;
        
        player.movementSpeed = c.movementSpeed;
        player.armor = c.armor;

}

    public void SetBoardManager()
    {
        boardManager = Instantiate(boardManager);
    }

	public void SetSelectedCharacter(ScriptableObject character){
		GameManager.instance.selectedCharacter = character;
	}

	public Player GetPlayerByName(string name){
		foreach (Player p in listaPlayers) {
			if (name == p.name) {
				return p;
			}
		}
		return null;
	}

    public void EndTurn()
    {
        quienJuega = (quienJuega + 1)% listaTodasLasEntidadesQueTenganTurnos.Count;
        listaTodasLasEntidadesQueTenganTurnos[quienJuega].StartTurn();
    }


}


