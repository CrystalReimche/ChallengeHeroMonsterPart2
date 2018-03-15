using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChallengeHeroMonsterPart2
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Random rndDmgMax = new Random();
            // Instantiating Hero 
            Character hero = new Character();
            hero.Name = "Xena: The Warrior Princess";
            hero.Health = 100;
            hero.DamageMaximum = 20;
            hero.AttackBonus = false;

            // Instantiating Monster
            Character monster = new Character();
            monster.Name = "The Cyclops";
            monster.Health = 100;
            monster.DamageMaximum = 20;
            monster.AttackBonus = true;

            // Instantiating Dice
            Dice dice = new Dice();

            

            // Attack Bonus
            if (hero.AttackBonus)
                monster.Defend(hero.Attack(dice));
            if (monster.AttackBonus)
                hero.Defend(monster.Attack(dice));

            // The Battle Loop
            while (hero.Health > 0 && monster.Health > 0)
            {
                displayRoundHeader();
                monster.Defend(hero.Attack(dice));
                hero.Defend(monster.Attack(dice));


                printStats(hero);
                printStats(monster);
            }

            displayResult(hero, monster);
        }

        private void displayRoundHeader()
        {
            rLabel.Text += "<hr /><p>Round begins ...</p>";
        }

        private void displayResult(Character opponent1, Character opponent2)
        {
            if (opponent1.Health <= 0 && opponent2.Health <= 0)
                rLabel.Text += String.Format("<p>Both {0} and {1} died.</p>", opponent1.Name, opponent2.Name);
            else if (opponent1.Health <= 0)
                rLabel.Text += String.Format("<p>{0} defeats {1}</p>", opponent2.Name, opponent1.Name);
            else
                rLabel.Text += String.Format("<p>{0} defeats {1}</p>", opponent1.Name, opponent2.Name);

        }


        private void printStats(Character character)
        {
            
            rLabel.Text += String.Format("<p>Name: {0} - Health: {1} - DamageMaximum: {2} - AttackBonus: {3}<p>",
                character.Name,
                character.Health,
                character.DamageMaximum.ToString(),
                character.AttackBonus.ToString());
        }

    }

    //  ***********************************************************
    //                     CHARACTER CLASS
    //  ***********************************************************
    class Character
    {
        // Creating Character object properties
        public string Name { get; set; }
        public int Health { get; set; }
        public int DamageMaximum { get; set; }
        public bool AttackBonus { get; set; }


        //Attack Method
        public int Attack(Dice dice)
        {
            dice.Sides = this.DamageMaximum;
            return dice.Roll();
        }


        // Defend Method
        public void Defend(int dmg) // This isn't int because it's not returning any value, it's only doing a calculation
        {
            this.Health -= dmg;
        }

    }


    //  ***********************************************************
    //                          DICE CLASS
    //  ***********************************************************
    class Dice
    {
        // Creating Dice object property
        public int Sides { get; set; }

        Random rnd = new Random();

        // Roll Method
        public int Roll()
        {
            return rnd.Next(this.Sides + 1);    // This is generating a random number up to the maximum that was set in line 18 & 25
        }
    }
}