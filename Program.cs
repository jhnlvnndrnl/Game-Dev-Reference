using System;
using System.Threading;

class Program
{
    // Adjust typing speed (milliseconds per character)
    static int SPEED = 15;

    static Random rng = new Random();

    static void Main()
    {
        Console.Title = "Rise of Carlo - Console";
        GameLoop();
    }

    static void GameLoop()
    {
        Player carlo = new Player("Carlo");
        SystemCore sys = new SystemCore();

        SlowWrite("\n--- Rise of Carlo ---\n\n");
        SlowWrite("Press any key to begin the tutorial or press 'x' to exit.\n> ");
        var k = Console.ReadKey(true).KeyChar;
        if (k == 'x' || k == 'X') { SlowWrite("\nGoodbye.\n"); return; }

        SceneTutorial(carlo);
        SceneOutbreak(carlo);
        SceneSystemActivation(carlo, sys);
        SceneUpgrade(carlo, sys);

        // Boss arcs
        if (!SceneBossDesert(carlo, sys)) return;
        if (!SceneBossOcean(carlo, sys)) return;
        if (!SceneBossForest(carlo, sys)) return;

        SceneFinalSetup(carlo, sys);
        EndScreen();
    }

    static void SceneTutorial(Player p)
    {
        SlowWrite("\nScene 1 - The Daily Struggle\n");
        SlowWrite("You are weak, your sword is rusted, and each coin barely pays for a night's meal.\n");
        SlowWrite("You enter a low-rank dungeon to farm — this is how it always has been.\n\n");

        SlowWrite("Small enemies approach. Learn to attack.\n");
        PauseKey("Press any key to swing your rusty sword... ");
        int damage = rng.Next(1, 4);
        SlowWrite($"\nYou hit for {damage} damage. You earn 3 coins.\n");
        p.Gold += 3;
        SlowWrite($"Gold: {p.Gold}\n");
    }

    static void SceneOutbreak(Player p)
    {
        SlowWrite("\nScene 2 - The Outbreak\n");
        SlowWrite("A tremor. Monsters multiply beyond reason. The town is burning.\n");
        PauseKey("Press any key to run home... ");

        SlowWrite("\nYou reach your home — silence. Ashes. No one answers.\n");
        SlowWrite("Your parents and siblings are gone.\n");
        p.Emotion = "rage";
        SlowWrite("\nRage fills you. A hollow emptiness takes over.\n");

        PauseSeconds(1200); // short pause for drama
    }

    static void SceneSystemActivation(Player p, SystemCore sys)
    {
        SlowWrite("\nScene 3 - The System Awakens\n");
        SlowWrite("A glowing panel appears in front of you: SYSTEM ACTIVATED — Potential Detected.\n");
        SlowWrite("[1] Accept the System\n[2] Refuse\n> ");
        char c = Console.ReadKey(true).KeyChar;
        SlowWrite("\n");

        if (c == '1')
        {
            sys.BindTo(p);
            SlowWrite("System bound. You feel a power you never knew.\n");
        }
        else
        {
            SlowWrite("You refuse... but fate does not accept refusal. The System insists.\n");
            sys.BindTo(p);
            SlowWrite("System bound anyway.\n");
        }
    }

    static void SceneUpgrade(Player p, SystemCore sys)
    {
        SlowWrite("\nScene 4 - First Upgrade\n");
        SlowWrite("You test your new abilities.\n");
        p.LevelUp(1);
        p.Equip(new Gear("Salvaged Blade", 3));
        p.Equip(new Gear("Cloth Vest", 1));
        SlowWrite($"Level: {p.Level} | Attack: {p.Attack} | Defense: {p.Defense}\n");
        SlowWrite("\nNew quest: Defeat the three cataclysm bosses.\n");
        PauseKey("Press any key to continue your journey... ");
    }

    static bool SceneBossDesert(Player p, SystemCore sys)
    {
        SlowWrite("\nDESERT ARC - Goblin King\n");
        SlowWrite("The Goblin King is stronger than ordinary goblins. A blue crystal glows on his helm.\n");
        PauseKey("Prepare for battle (press any key)... ");

        Boss goblinKing = new Boss("Goblin King", 20, 4, 1);
        bool won = DoBattle(p, goblinKing);
        if (!won) { SlowWrite("\nYou fall... Game Over.\n"); return false; }

        SlowWrite("\nThe crystal shatters. A shadowy figure watches from the dunes.\n");
        SlowWrite("\"So... one pawn has been removed. Interesting.\"\n");
        PauseSeconds(900);
        return true;
    }

    static bool SceneBossOcean(Player p, SystemCore sys)
    {
        SlowWrite("\nOCEAN ARC - Slime Overlord\n");
        SlowWrite("Villagers say the sea turned violent the same night the desert fell.\n");
        PauseKey("Dive into the ocean cavern (press any key)... ");

        Boss slime = new Boss("Slime Overlord", 30, 3, 2);
        bool won = DoBattle(p, slime);
        if (!won) { SlowWrite("\nYou fall... Game Over.\n"); return false; }

        SlowWrite("\nAnother crystal found and broken. The same figure appears: \"Just like him...\"\n");
        PauseSeconds(900);
        return true;
    }

    static bool SceneBossForest(Player p, SystemCore sys)
    {
        SlowWrite("\nFOREST ARC - Orc Warlord\n");
        SlowWrite("The forest roars with unnatural war cries.\n");
        PauseKey("Enter the warlord's clearing (press any key)... ");

        Boss orc = new Boss("Orc Warlord", 40, 5, 3);
        bool won = DoBattle(p, orc);
        if (!won) { SlowWrite("\nYou fall... Game Over.\n"); return false; }

        SlowWrite("\nAs the third crystal dies, the shadow steps forward — clearer, taller.\n");
        PauseSeconds(800);
        SlowWrite("\nIt's your older brother.\n");
        SlowWrite("\"Carlo... you are stronger than I expected. I had to do this.\"\n");
        SlowWrite("Plot twist: he survived the outbreak and is behind the corruption.\n");

        PauseKey("Press any key to respond... ");
        SlowWrite("\nYour world tilts. Family, vengeance, and truth collide.\n");
        return true;
    }

    static void SceneFinalSetup(Player p, SystemCore sys)
    {
        SlowWrite("\nFinal Quest Activation\n");
        SlowWrite("System: NEW QUEST - Confront the Corrupted One.\n");
        SlowWrite("Sky cracks open with dark energy. This is where you decide fate.\n");

        SlowWrite("\n[1] Pursue your brother to end this\n[2] Try to save him\n> ");
        char ch = Console.ReadKey(true).KeyChar;
        SlowWrite("\n");

        if (ch == '2')
        {
            SlowWrite("\nYou attempt to save him. Family first.\n");
            // Simple branching effect
            bool success = rng.Next(0, 2) == 1;
            if (success)
            {
                SlowWrite("Your words break through his madness. The final boss battle becomes a tragic duel — but hope returns.\n");
                PauseKey("Press any key to face the final confrontation... ");
                Boss corrupted = new Boss("Corrupted One", 60, 6, 5);
                DoBattle(p, corrupted);
                SlowWrite("\nEnding: He survives, the corruption is purged, the world begins to heal.\n");
            }
            else
            {
                SlowWrite("He refuses. You have no choice. The final duel ends with sacrifice.\n");
                PauseKey("Press any key to face the final confrontation... ");
                Boss corrupted = new Boss("Corrupted One", 80, 8, 6);
                DoBattle(p, corrupted);
                SlowWrite("\nEnding: He falls. You become the legend — bearing his memory.\n");
            }
        }
        else
        {
            SlowWrite("\nYou choose to end the threat and pursue him with iron resolve.\n");
            PauseKey("Press any key to face the final confrontation... ");
            Boss corrupted = new Boss("Corrupted One", 100, 10, 8);
            DoBattle(p, corrupted);
            SlowWrite("\nEnding: The corrupted brother falls. The world is saved, but your heart is scarred.\n");
        }
    }

    static void EndScreen()
    {
        SlowWrite("\n--- Chapter Complete ---\n");
        SlowWrite("The System whispers: \"The end is near... salvation or destruction lies in your hands.\"\n");
        SlowWrite("\nThank you for playing this prototype. Press any key to exit.\n");
        Console.ReadKey(true);
    }

    // ---------- Combat ----------
    static bool DoBattle(Player p, Boss b)
    {
        SlowWrite($"\nBattle Start: {b.Name} (HP: {b.HP})\n");
        while (p.IsAlive && b.IsAlive)
        {
            SlowWrite($"\nYour HP: {p.HP} | {b.Name} HP: {b.HP}\n");
            SlowWrite("[A] Attack  [H] Heal (small)  [S] Use Skill  [x] Flee\n> ");
            char choice = Console.ReadKey(true).KeyChar;
            SlowWrite("\n");
            if (choice == 'x' || choice == 'X') { SlowWrite("You flee! The quest fails.\n"); return false; }

            if (choice == 'A' || choice == 'a')
            {
                int dmg = rng.Next(1, p.Attack + 1);
                b.HP -= dmg;
                SlowWrite($"You strike for {dmg} damage.\n");
            }
            else if (choice == 'H' || choice == 'h')
            {
                int heal = Math.Max(1, rng.Next(2, 6));
                p.HP += heal;
                SlowWrite($"You heal {heal} HP.\n");
            }
            else if (choice == 'S' || choice == 's')
            {
                if (p.SkillCooldown <= 0)
                {
                    int skillDmg = p.Attack + rng.Next(2, 6);
                    b.HP -= skillDmg;
                    p.SkillCooldown = 3;
                    SlowWrite($"Skill used! You deal {skillDmg} damage.\n");
                }
                else
                {
                    SlowWrite($"Skill on cooldown ({p.SkillCooldown} turns).\n");
                }
            }
            else
            {
                SlowWrite("Invalid action — you lose an opening.\n");
            }

            // Boss turn if still alive
            if (b.IsAlive)
            {
                int bdmg = rng.Next(1, b.Attack + 1) - p.Defense;
                bdmg = Math.Max(0, bdmg);
                p.HP -= bdmg;
                SlowWrite($"{b.Name} hits you for {bdmg} damage.\n");
            }

            // reduce cooldowns
            if (p.SkillCooldown > 0) p.SkillCooldown--;

            // simple short delay to keep flow
            PauseSeconds(250);
        }

        if (!p.IsAlive) return false;
        SlowWrite($"\nYou defeated {b.Name}!\n");
        p.Gold += b.RewardGold;
        p.Exp += 10;
        if (p.Exp >= 20)
        {
            p.LevelUp(1);
            p.Exp = 0;
        }
        return true;
    }

    // ---------- Utilities ----------
    static void SlowWrite(string text)
    {
        foreach (char ch in text)
        {
            Console.Write(ch);
            Thread.Sleep(SPEED);
        }
    }

    static void PauseKey(string prompt)
    {
        SlowWrite(prompt);
        Console.ReadKey(true);
        SlowWrite("\n");
    }

    static void PauseSeconds(int ms)
    {
        Thread.Sleep(ms);
    }
}

// ---------- Player, Gear, Boss, SystemCore ----------
class Player
{
    public string Name { get; private set; }
    public int Level { get; private set; } = 1;
    public int HP { get; set; } = 30;
    public int BaseAttack { get; private set; } = 2;
    public int BaseDefense { get; private set; } = 0;
    public int Attack { get { return BaseAttack + (Equipped != null ? Equipped.Attack : 0) + Level; } }
    public int Defense { get { return BaseDefense + (EquippedArmor != null ? EquippedArmor.Defense : 0); } }
    public int Gold { get; set; } = 0;
    public int Exp { get; set; } = 0;
    public int SkillCooldown { get; set; } = 0;
    public string Emotion { get; set; } = "determined";
    public Gear Equipped { get; private set; }
    public Gear EquippedArmor { get; private set; }

    public bool IsAlive { get { return HP > 0; } }

    public Player(string name)
    {
        Name = name;
    }

    public void Equip(Gear g)
    {
        if (g.Type == GearType.Weapon) Equipped = g;
        else EquippedArmor = g;
    }

    public void LevelUp(int inc)
    {
        Level += inc;
        HP += 10 * inc;
    }
}

enum GearType { Weapon, Armor }

class Gear
{
    public string Name { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public GearType Type { get; private set; }

    public Gear(string name, int power)
    {
        Name = name;
        // simple rule: odd => weapon, even => armor (for this prototype)
        if (power % 2 == 1) { Type = GearType.Weapon; Attack = power; Defense = 0; }
        else { Type = GearType.Armor; Type = GearType.Armor; Defense = power / 2; Attack = 0; }
    }
}

class Boss
{
    public string Name { get; private set; }
    public int HP { get; set; }
    public int Attack { get; private set; }
    public int RewardGold { get; private set; }

    public Boss(string name, int hp, int attack, int reward)
    {
        Name = name;
        HP = hp;
        Attack = attack;
        RewardGold = reward;
    }

    public bool IsAlive { get { return HP > 0; } }
}

class SystemCore
{
    public void BindTo(Player p)
    {
        // small narrative effect - bind increases potential
        p.LevelUp(1);
        p.Gold += 5;
        p.Exp += 5;
    }
}
