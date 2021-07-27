using FightingArena;
using NUnit.Framework;
using System;
using System.Linq;

namespace Tests
{
    public class ArenaTests
    {
        private Arena arena;
        
        [SetUp]
        public void Setup()
        {
            this.arena = new Arena();
        }

        [Test]
        public void Ctor_InitializeWarriors()
        {
            Assert.That(this.arena.Warriors, Is.Not.Null);
        }

        //[Test]
        //public void Count_IncreaseCount_WhenAddWarrior() 
        //{
        //    Warrior warrior = new Warrior("Nasko", 35, 40);
        //    this.arena.Enroll(warrior);

        //    Assert.That(arena.Count, Is.EqualTo(1));
        //}

        [Test]
        public void Count_CountZero_WhenArenaIsEmpty() 
        {
            Assert.That(arena.Warriors.Count, Is.EqualTo(0));
        }

        [Test]
        public void Enroll_ThrowsException_WhenWarriorAlreadyExists() 
        {
            string name = "Warrior";
            Warrior warrior = new Warrior(name, 35, 40);
            this.arena.Enroll(warrior);

            Assert.Throws<InvalidOperationException>(() => this.arena.Enroll(new Warrior(name, 55, 55)));

        }

        [Test]
        public void Enroll_IncreasesArenaCount_WhenAddNewWarrior() 
        {
            Warrior warrior = new Warrior("Nasko", 35, 40);
            this.arena.Enroll(warrior);

            Assert.That(this.arena.Count, Is.EqualTo(1));

        }

        [Test]
        public void Enroll_AddsWorriorToWarriors()  //Is Warrior is added and exists into the collection
        {
            string name = "Nasko";
            Warrior warrior = new Warrior(name, 35, 40);
            this.arena.Enroll(warrior);
            Assert.That(this.arena.Warriors.Any(war => war.Name == name), Is.True);
        }

        [Test]        
        public void Fight_ThrowsException_WhenDefenderDoesNotExist() 
        {
            
            string nameAtt = "Attacker";
            this.arena.Enroll(new Warrior(nameAtt, 40, 40));

            Assert.Throws<InvalidOperationException>(() => this.arena.Fight(nameAtt, "Defender"));           
               
        }

        [Test]
        public void Fight_ThrowsException_WhenAttackerDoesNotExist() 
        {
            string nameDef = "Defender";
            this.arena.Enroll(new Warrior(nameDef, 40, 40));

            Assert.Throws<InvalidOperationException>(() => this.arena.Fight("Attacker", nameDef));
        }

        [Test]
        public void Fight_ThrowsException_WhenBothDoesNotExist() 
        {
            Assert.Throws<InvalidOperationException>(()=> this.arena.Fight("Attacker", "Defender"));
        }

        [Test]
        public void Fight_BothWarriorsLoseHpInFight() 
        {
            var initHp = 100;
            Warrior attacker = new Warrior("Attacker", 50, initHp);
            Warrior defender = new Warrior("Defender", 50, initHp);

            this.arena.Enroll(attacker);
            this.arena.Enroll(defender);

            this.arena.Fight(attacker.Name, defender.Name);

            Assert.That(attacker.HP, Is.EqualTo(initHp - defender.Damage));
            Assert.That(defender.HP, Is.EqualTo(initHp - attacker.Damage));
        }
    }
}
