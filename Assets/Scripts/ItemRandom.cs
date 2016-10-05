using UnityEngine;
using System.Collections;

namespace Completed
{
    public class ItemRandom : MonoBehaviour
    {
        //Random.Range (0, gridPositions.Count);
        public string name;
        public string description;
        public int level;
        public string slotTipe;
        public string inventoryType;

        public string rarity;

        public float strength;
        public float strength1;
        public float agility;
        public float agility1;
        public float wisdom;
        public float wisdom1;

        public float damage;
        public float damage1;

        public float armor;
        public float armor1;

        public float maxlife;
        public float maxlife1;
        public float maxmana;        
        public float maxmana1;
        public float liferegen;
        public float manaregen;

        public float blockchance;
        public float blockchance1;
        public float dodgechance;
        public float dodgechance1;
        public float immunitychance;
        public float immunitychance1;

        public float stunchance;
        public float stunchance1;
        public float criticalstrikechance;               
        public float criticalstrikechance1;
        public float piercechance;
        public float piercechance1;
        public float attackspeed;
        public float attackspeed1;
        public float movementspeed;        
        public float movementspeed1;

        //de acuerdo a la rareza va agregando stats, el item de la BDD tiene 2 valores para cada stat (min y max) y se obtiene un valor random para generar el item final
        public void generateItemStats()
        {
            Item item = this.gameObject.GetComponent<Item>();
            
            item.name = name;
            item.description = description;
            item.slotTipe = slotTipe;
            item.inventoryType = inventoryType;
           // item.level = GameManager.instance.getDifficultylvl();
            
            int rarity = Random.Range(0, 100);
            int rnd = Random.Range(1, 3); // 1 por cada clase
            if (slotTipe == "Weapon")
            {
                item.damage = Random.Range(damage, damage1);
            }
            else
            {
                item.armor = Random.Range(armor, armor1);
            }

            item.rarity = "Normal";
            if (rarity > 60)//rare
            {
                item.rarity = "Rare";
                if (rnd == 1)
                {
                    item.strength = Random.Range(strength, strength1);
                }
                else if (rnd == 2)
                {
                    item.agility = Random.Range(agility, agility1);
                }
                else if (rnd == 3)
                {
                    item.wisdom = Random.Range(wisdom, wisdom1);
                }
            }
            if (rarity > 80)//unicque
            {
                item.rarity = "Unique";
                rnd = Random.Range(1, 5);//1st stat
                if (rnd == 1)
                {
                    item.maxlife = Random.Range(maxlife, maxlife1);
                }
                else if (rnd == 2)
                {
                    item.maxmana = Random.Range(maxmana, maxmana1);
                }
                else if (rnd == 3)
                {
                    item.blockchance = Random.Range(blockchance, blockchance1);
                }
                else if (rnd == 4)
                {
                    item.dodgechance = Random.Range(dodgechance, dodgechance1);
                }
                else if (rnd == 5)
                {
                    item.immunitychance = Random.Range(immunitychance, immunitychance1);
                }

                rnd = Random.Range(1, 5);//2nd stat
                if (rnd == 1)
                {
                    item.stunchance = Random.Range(stunchance, stunchance1);
                }
                else if (rnd == 2)
                {
                    item.criticalstrikechance = Random.Range(criticalstrikechance, criticalstrikechance1);
                }
                else if (rnd == 3)
                {
                    item.piercechance = Random.Range(piercechance, piercechance1);
                }
                else if (rnd == 4)
                {
                    item.attackspeed = Random.Range(attackspeed, attackspeed1);
                }
                else if (rnd == 5)
                {
                    item.movementspeed = Random.Range(movementspeed, movementspeed1);
                }
               

            }
            if(rarity > 95)//legendary
            {
                item.rarity = "Legendary";
                rnd = Random.Range(1, 2);
                if (rnd == 1)
                {
                    item.damage = Random.Range(damage, damage1);
                }
                else if (rnd == 2)
                {
                    item.armor = Random.Range(armor, armor1);
                }

            }
        }
        
        
    }
}


