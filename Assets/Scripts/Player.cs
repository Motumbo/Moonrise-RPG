using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MovingObject
{
    public GameObject panelPlayerStatPrefab;
	public GameObject panelPlayer;	//Panel Instanciado

    public int wallDamage = 1;
    public int pointsPerFood = 20;
    public int pointsPerSoda = 10;
    public int pointsPerPotion = 50;

    public Text nameText;
    public Text actionPointsText;
    public Text healthText;
    public Text energyText;
    public Text manaText;
    public Text expText;

    public Sprite characterPortrait;
    public Slider healthSlider;
    public Slider energySlider;
    public Slider manaSlider;
    public Slider expSlider;                    

    public GameObject inventory;
    public bool movement = true;
    private float restartLevelDelay = 1f;       //Delay time in seconds to restart level.

    public AudioClip moveSound1;                //1 of 2 Audio clips to play when player moves.
    public AudioClip moveSound2;                //2 of 2 Audio clips to play when player moves.		
    public AudioClip gameOverSound;             //Audio clip to play when player dies.
    public AudioClip attacksound1;
    public AudioClip attacksound2;
    private Animator animator;                  //Used to store a reference to the Player's animator component.

    //basic stats
    public string name;

    public int currentActionPoints = 23;

    public string classType;
    public int level;
    public int experience;
    public int experienceRequiered;

    public int strength;
    public int agility;
    public int wisdom;

    public int currentEnergy;
    public int maxEnergy;
    public int currentHealth;
    public int maxHealth;
    public int currentMana;
    public int maxMana;
    public float movementSpeed;
    public int armor;

    //real stats (basic stats +inventory stats)
    public float Rstrength;
    public float Ragility;
    public float Rwisdom;
    public float Rmaxlife;
    public float Rlife;
    public float Rmaxmana;
    public float Rmana;
    public float Rliferegen;
    public float Rmanaregen;
    public float Rblockchance;
    public float Rstunchance;
    public float Rcriticalstrikechance;
    public float Rpiercechance;
    public float Rattackspeed;
    public float Rdodgechance;
    public float Rimmunitychance;
    public float RmovementSpeed;
    public float Rdamage;
    public float RdamagePerSecond;
    public float Rarmor;

    private Enemy target = null;
    public List<GameObject> listaSkills;      // agrego las skills disponible de acuerdo al lvl del player   - solo las disponibles-

    private float nextbutton = 0.0f;
    private float buttonRate = 0.3f;


    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        inventory = Instantiate(inventory); //instancia el inventario
        inventory.transform.SetParent(this.transform);  
		SetUpPanelPlayerStat(); 
    }

    // Update is called once per frame
    void Update()
    {

        if (!GameManager.instance.playersTurn) //Se fija si le toca
        {
            return;
        }
        else
        {
            DoStuff();
            /*
            int horizontal = 0;
            int vertical = 0;

            horizontal = (int)Input.GetAxisRaw("Horizontal");
            vertical = (int)Input.GetAxisRaw("Vertical");
            //ESTO IMPIDE QUE EL JUGADOR SE MUEVA DIAGONALMENTE
            if (horizontal != 0)
                vertical = 0;

            if (horizontal != 0 || vertical != 0)
                AttemptMove<Wall>(horizontal, vertical);
                */
        }

        //if ( Time.time > nextbutton) //Time.time devuelve el tiempo que paso desde el comienzo 
        //{
        //nextbutton = Time.time + buttonRate;
        //}
    }

    private IEnumerator DoStuff() //Representa una parte de un turno del jugador
    {
        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");
        //ESTO IMPIDE QUE EL JUGADOR SE MUEVA DIAGONALMENTE
        if (horizontal != 0)
            vertical = 0;

        if (horizontal != 0 || vertical != 0)
            AttemptMove<Wall>(horizontal, vertical);

        if (currentActionPoints == 0)
        {
            GameManager.instance.EndTurn();
        }
        else
        {
            yield return null;
        }
    }

    protected override void OnCantMove<T>(T Component)
    {
        Wall hitWall = Component as Wall;
        hitWall.DamagedWall(wallDamage);
        animator.SetTrigger("playerChop");
    }

    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoseVida(int loss)
    {
        animator.SetTrigger("playerHit");
        currentHealth -= loss;
        healthText.text = " - " + loss + " Vida = " + currentHealth;
        CheckifGameOver();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Food")
        {
            currentEnergy += pointsPerFood;
            energyText.text = " + " + pointsPerFood + " Food = " + currentEnergy;
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Soda")
        {
            currentEnergy += pointsPerSoda;
            energyText.text = " + " + pointsPerSoda + " Food = " + currentEnergy;
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Potion")
        {
            currentHealth += pointsPerPotion;
            healthText.text = " + " + pointsPerPotion + " Vida = " + currentHealth;
            other.gameObject.SetActive(false);
        }
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        currentEnergy--;
        base.AttemptMove<T>(xDir, yDir);
        RaycastHit2D hit;
        //CheckifGameOver(); //JUDIO DE MIERDA, ERA PORQUE CAMBIASTE EL STAT A MAXLIFE.
        UpdateDataOnPlayerStatsPanel();
        //GameManager.instance.playersTurn = false;
    }

    private void CheckifGameOver()
    {
        if (currentHealth <= 0)
            GameManager.instance.GameOver();
    }

	public void SetPlayerStatsPanelTextReferences()
    {		
		Text[] listaTexts = panelPlayer.GetComponentsInChildren<Text>();
        foreach (Text text in listaTexts)
        {
            switch (text.name)
            {
                case "TextName":
                    nameText = text;
                    break;

                case "TextActionPoints":
                    actionPointsText = text;
                    break;

                case "TextHealth":
                    healthText = text;
                    break;

                case "TextEnergy":
                    energyText = text;
                    break;

                case "TextMana":
                    manaText = text;
                    break;

                case "TextExp":
                    expText = text;
                    break;
                default:
                    Debug.Log("El text" + text.name + "no puedo ser encontrado. SetPlayerStatsPanelTextReferences().");
                    break;
            }
        }
    }

    public void SetPlayerStatsPanelImageReferences()
    {
        Image[] listaImages = panelPlayer.GetComponentsInChildren<Image>();
        foreach (Image image in listaImages)
        {
            switch (image.name)
            {
                case "ImageCharacter":
                    image.sprite = characterPortrait;
                    break;
                default:
                    Debug.Log("El image" + image.name + "no puedo ser encontrado. SetPlayerStatsPanelImageReferences().");
                    break;
            }
        }
    }

    public void SetPlayerStatsPanelSliderReferences()
    {
        Slider[] listaSliders = panelPlayer.GetComponentsInChildren<Slider>();
        foreach (Slider slider in listaSliders)
        {
            switch (slider.name)
            {
                case "SliderHealth":
                    healthSlider = slider;
                    break;
                case "SliderEnergy":
                    energySlider = slider;
                    break;
                case "SliderMana":
                    manaSlider = slider;
                    break;
                case "SliderExp":
                    expSlider = slider;
                    break;
                default:
                    Debug.Log("El slider" + slider.name + "no puedo ser encontrado. SetPlayerStatsPanelSliderReference().");
                    break;
            }

        }
    }



    public void UpdateDataOnPlayerStatsPanel()
    {
        actionPointsText.text = currentActionPoints.ToString();

        healthText.text =  currentHealth + "/" + maxHealth;
        energyText.text = currentEnergy + "/" + maxEnergy;
        manaText.text = currentMana + "/" + maxMana;
        expText.text = experience + "/" + experienceRequiered;

        healthSlider.maxValue = maxHealth;
        energySlider.maxValue = maxEnergy;
        manaSlider.maxValue = maxMana;
        expSlider.maxValue = experienceRequiered;

        healthSlider.value = currentHealth;
        energySlider.value = currentEnergy;
        manaSlider.value = currentMana;
        expSlider.value = experience;

        nameText.text = name;
    }

    public void GainLife()
    {
		Text[] listaText = EventSystem.current.currentSelectedGameObject.transform.GetComponentsInChildren<Text>();
		foreach (Text text in listaText) {
			if (text.name == "NameText") {
				Player p = GameManager.instance.GetPlayerByName(text.text); //text.text seria el nombre del jugador dentro de NameText
				Debug.Log("Nombre del jugador: "+ text.text);
			    if ((currentHealth + 10) < maxHealth)
			    {
			        p.currentHealth += 10;
			    }
				else Debug.Log("El jugador: " + text.text + " ya tiene vida suficiente.");
                p.UpdateDataOnPlayerStatsPanel();
			    //ME PARECE QUE LO MAS FACIL VA A SER DIRECTAMENTE GUARDAR UNA REFERENCIA DEL PLAYER EN EL PANEL
			}	
		}
    }

    public void CenterCameraToPlayer()
    {
        Text[] listaText = EventSystem.current.currentSelectedGameObject.transform.GetComponentsInChildren<Text>();
        foreach (Text text in listaText)
        {
            if (text.name == "NameText")
            {
                Player p = GameManager.instance.GetPlayerByName(text.text); //text.text seria el nombre del jugador dentro de NameText
                Camera.main.transform.SetParent(p.transform);
                Camera.main.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, -10);
            }
        }
    }

    public void SetUpPanelPlayerStat(){
		panelPlayer = Instantiate(panelPlayerStatPrefab);
		panelPlayer.transform.SetParent(GameManager.instance.panelPlayers.transform);
		SetPlayerStatsPanelTextReferences();
        SetPlayerStatsPanelImageReferences();
        SetPlayerStatsPanelSliderReferences();
        UpdateDataOnPlayerStatsPanel();
    }

}
