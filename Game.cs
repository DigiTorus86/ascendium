using System.Text;
using Ascendium.Ai;
using Ascendium.Ai.Results;
using Ascendium.Components;
using Ascendium.Components.Enemies;
using Ascendium.Content;
using Ascendium.Core;
using Ascendium.Types;
using Ascendium.Types.Factories;
using Ascendium.Ui;
using Ascendium.Ui.Frames;

public class Game
{
    private TimeSpan _displayDelay = TimeSpan.FromMilliseconds(250);
    private Encoding _originalEncoding;
    private Player _player;
    private Map _map;
    private TextFrame _textFrame;
    private StatusFrame _statusFrame;
    private bool _exit;
    private string _savedGamePath;


    /// <summary>
    /// Initializes a new instance of <see cref="Game"/>.
    /// </summary>
    public Game()
    {
        _savedGamePath = Path.Combine(Directory.GetCurrentDirectory(), "saves");
        Directory.CreateDirectory(_savedGamePath);

        _originalEncoding = Console.OutputEncoding;
        _textFrame = new TextFrame(0, 24, 76, 6);
        _map = new Map(3, 3);
        _player = new Player()
        {
            Name = "Pablo",
            MaxHealth = 20,
            Health = 20,
            MaxMana = 10,
            Mana = 10,
            Left = 10,
            Top = 10,
            NextLeft = 10,
            NextTop = 10
        };
        _statusFrame = new StatusFrame(_player, 51, 0, 25, 24);
        
    }

    /// <summary>
    /// The main game running loop.
    /// </summary>
    public void Run()
    {
        Console.Clear();
        Console.CursorVisible = false;

        ShowTitle();
        Story.ShowChapter(1);
        
        _textFrame.Text = _map.CurrentRoom.Description;
        _player.Update();
        _map.CurrentRoom.SetVisibility(_player);
        _map.Update();
        DrawScreen();
        

        do
        {
            bool allowUpdate = CheckKeyPress();
            
            if (allowUpdate)
            {
                _map.CurrentRoom.SetVisibility(_player);
                _map.Draw();

                var characters = _map.CurrentRoom.GetCharacters(); 
                foreach(Character character in characters)
                {
                    BaseResult result = character.Controller.Update(_map.CurrentRoom);
                    character.Draw();
                    result.Draw();

                    if (result.ShowModally && result is InteractionResult interaction)
                    {
                        _textFrame.AddTextLine(result.Messages.FirstOrDefault());
                        _textFrame.Draw();

                        bool done = false;
                        Prompt prompt = NpcInteractions.GetPrompt(interaction.Target.Name);
                        do
                        {                            
                            
                            if (prompt.Text.Length > 0)
                            {
                                ModalTextFrame mtf = new ModalTextFrame(10, 5, 30, 20);
                                mtf.Text = prompt.Text;
                                mtf.Draw();
                                DrawScreen();
                            }
                         
                            ModalSelectFrame msf = new ModalSelectFrame(10, 5, 30, 8, prompt.ResponseOptions);
                            msf.Title = $"To {interaction.Target.Name}:";
                            msf.Draw();
                            DrawScreen();

                            done = msf.SelectedValue == "EXIT";
                            prompt = NpcInteractions.GetPrompt(interaction.Target.Name, msf.SelectedValue);
                        } while (!done);
                    }
                    else if (result.ShowModally)
                    {
                        _statusFrame.Mode = StatusMode.Status;
                        _statusFrame.Draw();

                        var mtf = new ModalTextFrame(10, 10, 56, 12);
                        foreach (string msg in result.Messages)
                        {
                            _textFrame.AddTextLine(msg);
                            mtf.AddTextLine(msg);
                        }

                        _textFrame.Draw();
                        mtf.Draw();

                        DrawScreen();
                    }
                    else if (result.Messages.Any())
                    {
                        foreach (string msg in result.Messages)
                        {
                            _textFrame.AddTextLine(msg);
                            _textFrame.Draw();
                            Thread.Sleep(_displayDelay);
                        }
                    }
                }

                _statusFrame.Mode = StatusMode.Status;
                _statusFrame.Draw();
            }
        } while (!_exit && _player.Health > 0);

        // Restore console starting state
        Console.OutputEncoding = _originalEncoding;
        Console.Clear();
        Console.CursorVisible = true;
        Console.ForegroundColor = ConsoleColor.White;
    }

    /// <summary>
    /// Checks for keypresses and executes the appropriate logic for each key.
    /// </summary>
    /// <returns>True if the keypress was tactical and consumed a game turn, otherwise false.</returns>
    private bool CheckKeyPress()
    {
        bool allowUpdate = false;

        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        // -- Tactical key presses --- //
        if (keyInfo.Key == ConsoleKey.UpArrow)
        {
            if (_map.CurrentRoom.CanMoveTo(_player.Left, _player.Top - 1))
            {
                _player.NextTop--;
            }
            
            allowUpdate = true;
        }

        if (keyInfo.Key == ConsoleKey.DownArrow)
        {
            if (_map.CurrentRoom.CanMoveTo(_player.Left, _player.Top + 1))
            {
                _player.NextTop++;
            }

            allowUpdate = true;
        }

        if (keyInfo.Key == ConsoleKey.LeftArrow)
        {
            if (_map.CurrentRoom.CanMoveTo(_player.Left - 1, _player.Top))
            {
                _player.NextLeft--;
            }

            allowUpdate = true;
        }

        if (keyInfo.Key == ConsoleKey.RightArrow)
        {
            if (_map.CurrentRoom.CanMoveTo(_player.Left + 1, _player.Top))
            {
                _player.NextLeft++;
            }

            allowUpdate = true;
        }

        if (keyInfo.Key == ConsoleKey.C) // Cast
        {
            var spells = _player.GetSpellList();
            var msf = new ModalSelectValueFrame(10, 10, 30, 10, spells);
            
            if (spells.Any())
            {
                msf.Title = "Select spell to cast:";
            }
            else
            {
                msf.Title = "You do not know any spells.";
            }
            
            msf.Draw();
            _map.Draw();

            if (msf.SelectedValue.Length > 0)
            {
                Spell? spell = _player.GetSpellFromName(msf.SelectedValue);
                if (spell is null)
                {
                    return true;
                }

                 Character? target = _player;
                if (spell.EffectCategory == EffectCategoryType.Attack || 
                    spell.EffectCategory == EffectCategoryType.Debuff)
                {
                    target = TargetCharacter();
                }

                if (target is null)
                {
                    return true;
                }

                // TODO: check spell cost, casting time, target
                _textFrame.AddTextLine($"{_player.Name} casts {msf.SelectedValue}.");

                allowUpdate = true;
            }

            DrawScreen();
        }

        if (keyInfo.Key == ConsoleKey.A) // Attack with current weapon
        {
            _textFrame.AddTextLine($"{_player.Name} attacks with {_player.GetPrimaryWeapon().Name.ToLower()}.");
            _textFrame.Draw();
            
            PlayerController controller = _player.Controller as PlayerController ?? new PlayerController(_player);
            BaseResult result = controller.Attack(_map.CurrentRoom);
            result.Draw();

            foreach (string msg in result.Messages)
            {
                _textFrame.AddTextLine(msg);
                _textFrame.Draw();
                Thread.Sleep(_displayDelay);
            }

            allowUpdate = true;
        }

        if (keyInfo.Key == ConsoleKey.T) // Toggle current weapon
        {
            _textFrame.AddTextLine($"{_player.Name} switches weapons to use {_player.ToggleWeapon().Name.ToLower()}.");
            _textFrame.Draw();
            
            allowUpdate = true;
        }

        if (keyInfo.Key == ConsoleKey.U) // Use an item
        {
            var items = _player.GetItemList();
            var msf = new ModalSelectValueFrame(10, 10, 30, 10, items);
            
            if (items.Any())
            {
                msf.Title = "Select item to use:";
            }
            else
            {
                msf.Title = "You do not have any items.";
            }
            
            msf.Draw();

            if (msf.SelectedValue.Length > 0)
            {
                _textFrame.AddTextLine($"{_player.Name} uses {msf.SelectedValue}.");
                // TODO: remove from inventory, do effect
                allowUpdate = true;
            }

            DrawScreen();
        }

        if (keyInfo.Key == ConsoleKey.L) // Look
        {
            var sb = new StringBuilder();
            sb.Append("You see: ");
            bool addedSomething = false;
            foreach(Character character in _map.CurrentRoom.GetCharactersNotPlayer().Where(c => c.IsVisible))
            {
                if (addedSomething)
                {
                    sb.Append(", ");
                }

                sb.Append($"{character.Name} (level {character.Level})");
                addedSomething = true;
            }

            if (!addedSomething)
            {
                sb.Append("nothing");
            }

            sb.Append('.');
            _textFrame.AddTextLine(sb.ToString());
            _textFrame.Draw();

            allowUpdate = true;
        }

        if (keyInfo.Key == ConsoleKey.W) // Wait
        {
            allowUpdate = true;
        }

        // --- Administrative key presses --- //
        if (keyInfo.Key == ConsoleKey.Spacebar)
        {
            _textFrame.ScrollUp();
            _textFrame.Draw();
        }

        if (keyInfo.Key == ConsoleKey.Backspace)
        {
            _textFrame.ScrollDown();
            _textFrame.Draw();
        }

        if (keyInfo.Key == ConsoleKey.Tab)
        {
            _statusFrame.Mode = (_statusFrame.Mode == StatusMode.Attributes) ? 
                                _statusFrame.Mode = StatusMode.Status : 
                                _statusFrame.Mode + 1;
            
            _statusFrame.Draw();
        }

        if (keyInfo.Key == ConsoleKey.H) // Help display
        {
            var modal = new ModalTextFrame(2, 2, 64, 20);
            modal.SetText(Help.HelpText);
            modal.Draw();

            DrawScreen();
        }

        // TODO: add confirmation check
        _exit = keyInfo.Key == ConsoleKey.Q;

        // Clear out the key buffer
        while(Console.KeyAvailable)
        {
            Console.ReadKey(false);
        }

        return allowUpdate;
    }

    /// <summary>
    /// Displays the title screen.
    /// </summary>
    public void ShowTitle()
    {
        Console.Clear();
        Console.CursorVisible = false;

        for (int i = 0; i < Title.TitleScreen.Length; i++)
        {
            Console.Write("        ");
            Console.WriteLine(Title.TitleScreen[i]);
        }

        const string startNewGame = "Start New Game";
        const string loadSavedGame = "Load Saved Game"; 
        var options = new List<string>() {
            startNewGame,
            loadSavedGame
        };

        ModalSelectFrame msf = new ModalSelectFrame(25, 17, 20, 6, options);
        msf.Draw();

        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();

        switch (msf.SelectedValue)
        {
            case startNewGame:
                StartNewGame();
                break;

            case loadSavedGame:
                LoadSavedGame();
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Starts a new game.
    /// </summary>
    public void StartNewGame()
    {
        _player.Abilities.Clear();
        _player.Conditions.Clear();
        _player.Equipment.Clear();
        _player.Items.Clear();

        var enraged = ConditionFactory.GetCondition(ConditionType.Enraged);
        enraged.TurnsRemaining = 30;
        _player.Conditions.Add(enraged);

        _player.Health = 20;
        _player.MaxMana = 10;
        _player.Mana = 10;
        _player.Left = 10;
        _player.Top = 10;
        _player.NextLeft = 10;
        _player.NextTop = 10;

        var sword = ItemFactory.GetItem(ItemType.GreatSword); // Testing!
        var bow = ItemFactory.GetItem(ItemType.ThrowingAxe);
        _player.Equipment.Add(sword.EquipSlot, sword);
        _player.Equipment.Add(bow.EquipSlot, bow);

        _player.Items.Add(ItemFactory.GetItem(ItemType.HealingTea));
        _player.Items.Add(ItemFactory.GetItem(ItemType.MysticWine));
        _player.Spells.Add(SpellFactory.GetSpell(SpellType.ManaDart));
        _player.Spells.Add(SpellFactory.GetSpell(SpellType.Heal));
        _player.Spells.Add(SpellFactory.GetSpell(SpellType.Purify));

        Enemy enemy1 = EnemyFactory.CreateEnemy(EnemyType.Kobold, 1);
        enemy1.Left = 15;
        enemy1.Top = 10;

        Enemy enemy2 = EnemyFactory.CreateEnemy(EnemyType.Gnome, 1);
        enemy2.Left = 30;
        enemy2.Top = 20;

        var npc1 = new Character(CharacterType.NPC)
        {
            Name = "Bubba",
            Symbol = "@",
            ForegroundColor = ConsoleColor.Green, 
            MaxHealth = 10,
            Health = 10,
            Left = 20,
            Top = 20,
            NextLeft = 20,
            NextTop = 20
        };

        _map = new Map(3, 3);
        _map.CurrentRoom.Components.Add(_player);
        _map.CurrentRoom.Components.Add(enemy1);
        _map.CurrentRoom.Components.Add(enemy2);
        _map.CurrentRoom.Components.Add(npc1);
    }

    /// <summary>
    /// Loads a previously-saved game.
    /// </summary>
    public void LoadSavedGame()
    {
        var files = Directory.EnumerateFiles(_savedGamePath);

        var msf = new ModalSelectFrame(10, 5, 25, 10, files);
        msf.Title = "Select Saved Game";
        msf.Draw();

        StartNewGame(); // TEMPORARY!

        Console.Clear();
    }

    /// <summary>
    /// Redraws all elements on the screen.
    /// </summary>
    private void DrawScreen()
    {
        _map.Draw();
        _textFrame.Draw();
        _statusFrame.Draw();
    }

    /// <summary>
    /// Allows the player to select the target of a spell or other attack on the map.
    /// </summary>
    /// <returns>The targeted <see cref="Character>."/></returns>
    private Character? TargetCharacter()
    {
        Character? character = null;

        // Initially target closest character
        var characters = _map.CurrentRoom.GetCharactersNotPlayer().Where(c => c.IsVisible && !c.IsDead()).ToArray();
        character = CharacterSupport.GetNearestVisbleCharacter(_player.Left, _player.Top, true, characters);

        if (character is null)
        {
            _textFrame.AddTextLine("There are no visible targets.");
            _textFrame.Draw();
            return character;
        }

        character = CharacterSupport.GetNearestVisbleCharacter(_player.Left, _player.Top, true, characters);
        HighlightCharacter(character);

        int index = 0;
        bool exit = false;
        do
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Tab || keyInfo.Key == ConsoleKey.Spacebar)
            {
                character = characters[index];
                index = (index < characters.Length - 1) ? index + 1 : 0;
                _map.Draw();
                HighlightCharacter(character);
            }

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                exit = true;
            }

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                character = null;
                exit = true;
            }

        } while(exit == false);

        _map.Draw();
        return character;
    } 

    private void HighlightCharacter(Character character)
    {
        if (character.CharacterType == CharacterType.Enemy)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        Console.BackgroundColor = ConsoleColor.Black;
        Console.SetCursorPosition(character.Left, character.Top + 1);
        Console.Write(Glyph.ArrowUp);
    }
}