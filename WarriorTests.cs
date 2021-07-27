using FightingArena;
using NUnit.Framework;
using System;

namespace Tests
{
    public class WarriorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("", 50, 100 )]
        [TestCase("  ", 50, 100)]
        [TestCase(null, 50, 100)]
        [TestCase("WarriorName", 0, 100)]
        [TestCase("WarriorName", -10, 100)]
        [TestCase("WarriorName", 50, -10)]        
        public void Ctor_ThrowsException_WhenIsInvalid(string name, int damage, int hp)
        {
            //Warrior war = new Warrior(name, damage, hp);
            Assert.Throws<ArgumentException>(() => new Warrior(name, damage, hp));
        }

        [Test]
        [TestCase(30, 55)]
        [TestCase(15, 55)]
        [TestCase(55, 30)]
        [TestCase(55, 15)]
        public void Attack_ThrowsException_WhenHpIsLessThanMin(int attackerHp, int warriorHp ) 
        {
            Warrior attacker = new Warrior("Attacker", 50, attackerHp);
            Warrior warrior = new Warrior("Warrior", 10, warriorHp);
            Assert.Throws<InvalidOperationException>(() => attacker.Attack(warrior));
        }

        [Test]        
        public void Attack_ThrowsException_WhenHpLessThanDamage()
            //this.HP < warrior.Damage
        {
            Warrior attacker = new Warrior("Attacker", 50, 100);
            Warrior warrior = new Warrior("Warrior", attacker.HP + 1 , 100);

            Assert.Throws<InvalidOperationException>(() => attacker.Attack(warrior));
        }

        [Test]
        public void Attack_DecreaseHpWithDamage_WhenHpAttackerMoreThanOrEqualWorrierDamage()
            //this.HP >= warrior.Damage
        {
            int initialAttackerHP = 100;
            //int initialWorriorDamage = initialAttackerHP - 1;
            Warrior attacker = new Warrior("Attacker", 50, initialAttackerHP);
            Warrior warrior = new Warrior("Warrior", attacker.HP, 100);
            attacker.Attack(warrior);
            Assert.That(attacker.HP, Is.EqualTo(initialAttackerHP - warrior.Damage));
        }

        [Test]
        public void Attack_SetWarriorHpToZero_WhenAttackerDamageIsGreaterThanWarriorHP()
        // if (this.Damage > warrior.HP)
        {
            
            Warrior attacker = new Warrior("Attacker", 50, 100);
            Warrior warrior = new Warrior("Warrior", 30, 40);
            attacker.Attack(warrior);

            Assert.That(warrior.HP, Is.EqualTo(0));
        }

        //(this.Damage <= warrior.HP)
        //warrior.HP -= this.Damage;
        [Test]
        public void Attack_DecreaseWarriorHpWithAttrDamage_WhenAttrDamageIsLessThanOrEqual_WarriorHP() 
        {
            int warriorInitHp = 100;
            Warrior attacker = new Warrior("Attacker", 50, 100);
            Warrior warrior = new Warrior("Warrior", 30, warriorInitHp);
            attacker.Attack(warrior);

            Assert.That(warrior.HP,  Is.EqualTo(warriorInitHp - attacker.Damage));
        }
    }
}