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
    
    public static GameManager instance =null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public bool playersTurn = true;                        //Boolean to check if it's players turn, hidden in inspector but public.
 
    private string difficulty;
   
    //private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
    
    private int level = 1;                                  //Current level number, expressed in game as "Day 1".
    private List<Enemy> listaEnemies;                       //List of all Enemy units, used to issue them move commands.
    private List<Player> listaPlayers;

    private bool doingSetup = true;                         //Boolean to check if we're setting up board, prevent Player from moving during setup.

    private List<MovingObject> listaTodasLasEntidadesQueTenganTurnos;
    public int quienJuega = -1; //Segun el indice de la lista de todos los que juegan 
    public MovingObject playingPlayer;

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
        quienJuega = -1;
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
        if (listaTodasLasEntidadesQueTenganTurnos.Count > 0)
        {
           

            /*
            //Check that playersTurn or enemiesMoving or doingSetup are not currently true.
            if (playingPlayer is Player)
                //If any of these are true, return and do not start MoveEnemies.

                return;
            //Start moving enemies.
            else if (playingPlayer is Enemy)
            {
                //StartCoroutine(MoveEnemies((Enemy) playingPlayer));
            }

            */
        } 
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
    IEnumerator MoveEnemies(Enemy playingEnemy)
    {
        //Wait for turnDelay seconds, defaults to .1 (100 ms).
        yield return new WaitForSeconds(turnDelay);  
        
        //Call the MoveEnemy function of the Enemy.
        playingEnemy.MoveEnemy();

        //Wait for Enemy's moveTime before moving next Enemy, 
        yield return new WaitForSeconds(playingEnemy.moveTime);

        //Once Enemies are done moving, Wait for turnDelay seconds.
        yield return new WaitForSeconds(turnDelay);

        EndTurn(); //Ceder el turno al proximo enemigo
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
        if (quienJuega == -1)
        {
            quienJuega = 0;
            playingPlayer = listaTodasLasEntidadesQueTenganTurnos[quienJuega];
        }

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
        /*
        quienJuega = (quienJuega + 1)% listaTodasLasEntidadesQueTenganTurnos.Count;
        playingPlayer = listaTodasLasEntidadesQueTenganTurnos[quienJuega];
        playingPlayer.StartTurn();
        */
    }


}


