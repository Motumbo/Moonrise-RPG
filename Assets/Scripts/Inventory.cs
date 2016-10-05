using UnityEngine;
using System.Collections;
using UnityEngine.UI;	//Allows us to use UI.
using System.Collections.Generic;
namespace Completed
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory instance = null;

        public GameObject canvas_interfaz_usuario; //Canvas_Interfaz_Usuario-->Inventario--->canvas_interfaz_usuario,necesaria en el prefab
        public ItemsLoot itemsLoot; //script dentro del GameObject Inventario
        
        private float nextbutton = 0.0f;
        private float buttonRate = 0.3f;

        public GameObject canvas_Tooltip; //Referencia Canvas_Tooltip, necesaria en el prefab        
        public Text resizablePanelText; //ResizablePanel 
        public Text tooltipText;

        public List<GameObject> list_Slots_Equipment;
        public List<GameObject> list_Slots_Quest;
        public List<GameObject> list_Slots_Misc;
        
        public List<GameObject> list_Slots_Equiped;

        public GameObject selectedItem;
        public GameObject selectedItemSlot;
        

        public Text name;
        public Text clas;
        public Text level;
        public Text experience;
        public Text experienceRequiered;
        public Text strength;
        public Text agility;
        public Text wisdom;
        public Text maxlife;        
        public Text maxmana;       
        public Text liferegen;
        public Text manaregen;
        public Text blockchance;
        public Text stunchance;
        public Text criticalstrikechance;
        public Text piercechance;
        public Text attackspeed;
        public Text dodgechance;
        public Text immunitychance;
        public Text movementSpeed;
        public Text damage;
        public Text armor;
        

        public float Estrength;
        public float Eagility;
        public float Ewisdom;
        public float Emaxlife;
        public float Emaxmana;
        public float Eliferegen;
        public float Emanaregen;
        public float Eblockchance;
        public float Estunchance;
        public float Ecriticalstrikechance;
        public float Epiercechance;
        public float Eattackspeed;
        public float Edodgechance;
        public float Eimmunitychance;
        public float EmovementSpeed;
        public float Edamage;
        public float Earmor;


        
        void Awake()
        {
            //Check if instance already exists
            if (instance == null)
            {                
                instance = this;
                setUp();
            }
            //If instance already exists and it's not this:
            else if (instance != this)
            {
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);
            } 
        }
        
        void FixedUpdate()
        {
            if (Input.GetButton("I") && Time.time > nextbutton) //Time.time devuelve el tiempo que paso desde el comienzo 
            {
                updateDataOnPanel();
                nextbutton = Time.time + buttonRate;
                if (canvas_interfaz_usuario.activeInHierarchy)
                {
                    canvas_interfaz_usuario.SetActive(false);
                    hideTooltip();
                    //GameManager.instance.setPlayerMovement(true);
                }
                else
                {
                    if (itemsLoot.canvas_loot_Panel.activeInHierarchy)
                    {
                        itemsLoot.canvas_loot_Panel.SetActive(false);
                        
                    }
                    canvas_interfaz_usuario.SetActive(true);
                    //GameManager.instance.setPlayerMovement(false);
                    
                }
            }
        }

        private void setUpUI()
        {
            canvas_interfaz_usuario = Instantiate(canvas_interfaz_usuario);                     
            canvas_interfaz_usuario.GetComponent<Canvas>().worldCamera = Camera.main; //setea la camara para ver bien la posicion del canvas
            canvas_interfaz_usuario.SetActive(false);
            canvas_interfaz_usuario.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        }

        private void setUpTooltip()
        {
            canvas_Tooltip = Instantiate(canvas_Tooltip);
            Text[] tooltipTexts = canvas_Tooltip.GetComponentsInChildren<Text>();
            foreach (Text text in tooltipTexts)
            {
                if (text.gameObject.name == "TooltipText")
                {
                    tooltipText = text;
                }
                if (text.gameObject.name == "ResizablePanel")
                {
                    resizablePanelText = text;
                }
            }
        }

        private void setUpListaEquipados(Slot slot)
        {
            switch (slot.tipo)
            {
                case "MainHand":
                    list_Slots_Equiped.Add(slot.gameObject);
                    break;
                case "OffHand":
                    list_Slots_Equiped.Add(slot.gameObject);
                    break;
                case "Chest":
                    list_Slots_Equiped.Add(slot.gameObject);
                    break;
                case "Head":
                    list_Slots_Equiped.Add(slot.gameObject);
                    break;
                //completar con todos los slots disponibles en el inventario
                default:
                    //Debug.Log("No se pudo asociar el slot a ninguna lista slot.tipo: "+ slot.tipo);
                    break;
            }
        }

        private void setUpListasSlots()
        {
            Slot[] slots = canvas_interfaz_usuario.GetComponentsInChildren<Slot>();            
            foreach (Slot slot in slots)
            {
                if (slot.tipo == "equipment")
                {
                    list_Slots_Equipment.Add(slot.gameObject);
                }
                else if (slot.tipo == "misc")
                {
                    list_Slots_Misc.Add(slot.gameObject);
                }
                else if (slot.tipo == "quest")
                {
                    list_Slots_Quest.Add(slot.gameObject);
                }
                else setUpListaEquipados(slot);

            }
        }

        private void setUpPlayerStatsTexts()
        {
            Text[] textos = canvas_interfaz_usuario.GetComponentsInChildren<Text>();            
            foreach (Text texto in textos)
            {
				switch (texto.gameObject.name)
                {
                    case "Name":
                        name = texto;
                        break;
                    case "Class":
                        clas = texto;
                        break;
                    case "Level":
                        level = texto;
                        break;
                    case "Experience":
                        experience = texto;
                        break;
                    case "ExperienceRequiered":
                        experienceRequiered = texto;
                        break;
                    case "Strength":
                        strength = texto;
                        break;
                    case "Agility":
                        agility = texto;
                        break;
                    case "Wisdom":
                        wisdom = texto;
                        break;
                    case "MaxLife":
                        maxlife = texto;
                        break;
                    case "MaxMana":
                        maxmana = texto;
                        break;
                    case "Liferegen":
                        liferegen = texto;
                        break;
                    case "Manaregen":
                        manaregen = texto;
                        break;
                    case "blockchance":
                        blockchance = texto;
                        break;
                    case "stunchance":
                        stunchance = texto;
                        break;
                    case "criticalstrikechance":
                        criticalstrikechance = texto;
                        break;
                    case "piercechance":
                        piercechance = texto;
                        break;
                    case "attackspeed":
                        attackspeed = texto;
                        break;
                    case "dodgechance":
                        dodgechance = texto;
                        break;
                    case "immunitychance":
                        immunitychance = texto;
                        break;
                    case "movementSpeed":
                        movementSpeed = texto;
                        break;
                    case "Damage":
                        damage = texto;
                        break;
                    case "Armor":
                        armor = texto;
                        break; 
                  
                    //completar con todos los textos
                    default:                       
                        break;
                }

            }
        }
        private void setUp()
        {
            setUpItemsLoot(); 
            setUpTooltip();
            setUpUI();            
            setUpListasSlots();
            setUpPlayerStatsTexts();
                        
        }

        private void setUpItemsLoot()
        {
            itemsLoot = this.GetComponent<ItemsLoot>();
            itemsLoot.setUp();            
        }

        public void updateDataOnPanel()
        {/*
            Player p = GameManager.instance.player.GetComponent<Player>();
            UpdateEStats();
            p.UpdateRstats();          
            
                name.text = "NAME \n"+GameManager.instance.player.GetComponent<Player>().name;
                clas.text = "CLASS \n" + GameManager.instance.player.GetComponent<Player>().clas;
                level.text = "LEVEL \n" + (int)GameManager.instance.player.GetComponent<Player>().level;
                experience.text = "CURRENT EXP \n" + (int)GameManager.instance.player.GetComponent<Player>().experience;
                experienceRequiered.text = "EXP to LVL UP \n" + (int)GameManager.instance.player.GetComponent<Player>().experienceRequiered;
                strength.text = "STRENGTH \n" +(int) p.Rstrength;
                agility.text = "AGILITY \n" + (int)p.Ragility;
                wisdom.text = "WISDOM \n" + (int)p.Rwisdom;
                maxlife.text = "LIFE \n" + (int)p.Rmaxlife;
                maxmana.text = "MANA \n" + (int)p.Rmaxmana;
                blockchance.text = "BLOCK CHANCE \n" + p.Rblockchance.ToString("0.00")+"%";
                stunchance.text = "STUN CHANCE \n" + p.Rstunchance.ToString("0.00") + "%";
                criticalstrikechance.text = "CRITICAL CHANCE \n" + p.Rcriticalstrikechance.ToString("0.00") + "%";
                piercechance.text = "PIERCE CHANCE \n" + p.Rpiercechance.ToString("0.00") + "%";
                attackspeed.text = "ATTACK SPEED \n" + p.Rattackspeed.ToString("0.00") + "%";
                dodgechance.text = "DODGE CHANCE \n" + p.Rdodgechance.ToString("0.00") + "%";
                movementSpeed.text = "MOVEMENT SPEED \n" + p.RmovementSpeed.ToString("0.00") + "%";
                immunitychance.text = "INMUNITY CHANCE \n" + p.Rimmunitychance.ToString("0.00") + "%";
                damage.text = "DAMAGE \n" + (int)p.Rdamage;
                armor.text = "ARMOR \n" + (int)p.Rarmor; */
            
        }
        public void UpdateEStats()//recorrer la lista de slots equiped y sumar los stats
        {
            Estrength = 0;
            Eagility = 0;
            Ewisdom = 0;
            Emaxlife = 0;
            Emaxmana = 0;
            Eliferegen = 0;
            Emanaregen = 0;
            Eblockchance = 0;
            Estunchance = 0;
            Ecriticalstrikechance = 0;
            Epiercechance = 0;
            Eattackspeed = 0;
            Edodgechance = 0;
            Eimmunitychance = 0;
            EmovementSpeed = 0;
            Edamage = 0;
            Earmor = 0;

            foreach (GameObject slot in list_Slots_Equiped)
            {
                Item item;
                if (slot.transform.childCount > 0)
                {
                    item = slot.transform.GetChild(0).GetComponent<Item>();
                    Estrength += item.strength;
                    Eagility += item.agility;
                    Ewisdom += item.wisdom;
                    Emaxlife += item.maxlife;
                    Emaxmana += item.maxmana;
                    Eliferegen += item.liferegen;
                    Emanaregen += item.manaregen;
                    Eblockchance += item.blockchance;
                    Estunchance += item.stunchance;
                    Ecriticalstrikechance += item.criticalstrikechance;
                    Epiercechance += item.piercechance;
                    Eattackspeed += item.attackspeed;
                    Edodgechance += item.dodgechance;
                    Eimmunitychance += item.immunitychance;
                    EmovementSpeed +=  item.movementspeed;
                    Edamage += item.damage;
                    Earmor += item.armor;
                }
            }
            //GameManager.instance.player.GetComponent<Player>().UpdateRstats();

        }

        public void SetSelectedItem(GameObject item)
        {            
            Inventory.instance.selectedItem = item;    
            Inventory.instance.updateDataOnPanel();
        }

        public void showTooltip(GameObject _item)
        {
            Item item = _item.GetComponent<Item>();
            string color="";
            if(item.rarity == "Normal"){
                color = "white";
            }else if(item.rarity == "Rare"){
                color = "blue";
            }else if(item.rarity == "Unique"){
                color = "yellow";
            }else if(item.rarity == "Legendary"){
                color = "orange";
            }
            string s = "";
            s += "<color=" + color + "><size=20>" + item.name + "</size></color>\n";
            s+="Item lvl "+ item.level + "\n";
            if((item.inventoryType != "Misc")&&(item.inventoryType != "Quest")){
                 s+="Slot "+ item.slotTipe + "\n";
            }
            else{
                 s+=""+ item.inventoryType + "\n";
            }
           
            if(item.strength > 0){
                s+="STR   +"+(int) item.strength + "\n";
            }
            if(item.agility > 0){
                s += "AGI   +" + (int)item.agility + "\n";
            }
            if(item.wisdom > 0){
                s += "WIS   +" + (int)item.wisdom + "\n";
            }
            if (item.damage > 0)
            {
                s += "DMG   +" + (int)item.damage + "\n";
            }
            if (item.armor > 0)
            {
                s += "Armor +" + (int)item.armor + "\n";
            }
            if(item.maxlife > 0){
                s += "Life  +" + (int)item.maxlife + "\n";
            }
            if(item.maxmana > 0){
                s += "Mana  +" + (int)item.maxmana + "\n";
            }            
            if(item.stunchance > 0){
                s += "Stun  +" + item.stunchance.ToString("0.00") + "%" +"\n";
            }
            if(item.criticalstrikechance > 0){
                s += "Crit  +" + item.criticalstrikechance.ToString("0.00") + "%" + "\n";
            }
            if(item.attackspeed > 0){
                s += "ATK Speed +" + item.attackspeed.ToString("0.00") + "Secs" + "\n";
            }
            if(item.dodgechance > 0){
                s += "Dodge +" + item.dodgechance.ToString("0.00") + "%" + "\n";
            } 
            if(item.immunitychance > 0){
                s += "Inmunity +" + item.immunitychance.ToString("0.00") + "%" + "\n";
            }
            if (item.blockchance > 0)
            {
                s += "Block +" + item.blockchance.ToString("0.00") + "%" + "\n";
            }
            if(item.movementspeed > 0){
                s += "Movement +" + (int)item.movementspeed + "\n";
            }             
            if(item.description != ""){
                s+=""+ item.description + "\n";
            }

            tooltipText.text = s;
            resizablePanelText.text = s;
            canvas_Tooltip.SetActive(true);

        }
        public void hideTooltip()
        {
            canvas_Tooltip.SetActive(false);
        }

        public GameObject findEmptySlotByInventoryTipe(GameObject _item)
        {
            Item item =  _item.GetComponent<Item>();
            if (item.inventoryType == "Equip")
            {
                foreach (GameObject slot in list_Slots_Equipment)
                {
                    if (slot.transform.childCount == 0)
                    {
                        return slot;
                    }
                }
            }
            else if (item.inventoryType == "Misc")
            {
                foreach (GameObject slot in list_Slots_Misc)
                {
                    if (slot.transform.childCount == 0)
                    {
                        return slot;
                    }
                }
            }
            else if (item.inventoryType == "Quest")
            {
                foreach (GameObject slot in list_Slots_Quest)
                {
                    if (slot.transform.childCount == 0)
                    {
                        return slot;
                    }
                }
            }
            return null;
        }
        public void pickUpItem(GameObject item)
        {
            
            if (itemsLoot.canvas_loot_Panel.gameObject.activeInHierarchy)
            {
                if (Inventory.instance.findEmptySlotByInventoryTipe(item.gameObject) != null)
                {
                    item.transform.SetParent(this.findEmptySlotByInventoryTipe(item.gameObject).transform);
                    Inventory.instance.hideTooltip();
                }
                else
                {
                    //avisar por mensaje  el inventario esta lleno
                }
            }else  Debug.Log("no se encontro itemsloot");
        }
        public void lootAll()
        {
            itemsLoot = this.GetComponent<ItemsLoot>();
            foreach (GameObject slot in itemsLoot.listaSlotsLootPanel)
            {
                if (slot.transform.childCount > 0)
                {
                    pickUpItem(slot.transform.GetChild(0).gameObject);                  
                }
            }
        }


         
    }
}