using UnityEngine;
using System.Collections;


[CreateAssetMenu (menuName = "Character")]
public class Character : ScriptableObject {

	public string name = "DefaultName";

    public Sprite characterPortraitSprite;

    public int currentActionPoints = 23;

    public string classType = "DefaulClassType";
    public int level = 0;
    public int experience = 0;
    public int experienceRequiered = 0;

    public int strength = 0;
    public int agility = 0;
    public int wisdom = 0;

    public int currentEnergy = 0;
    public int maxEnergy = 0;
    public int currentHealth = 0;
    public int maxHealth = 0;
    public int currentMana = 0;
    public int maxMana = 0;
    public float movementSpeed = 0;
    public int armor = 0;
}