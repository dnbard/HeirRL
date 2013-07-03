using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

using HeirRL.Source;
using HeirRL.Level;
using HeirRL.Events;

namespace HeirRL.Characters
{
    public class Player:Character
    {
        public static Player Instance { get; protected set; }

        public static Player CreateNew(GridElement location)
        {
            if (!location.Passable) location = location.GetEmptyNearbyElement();
            var newPlayer = new Player(location);
            Instance = newPlayer;
            return newPlayer;
        }

        public static Player GetOrCreate(GridElement location)
        {
            if (Instance != null) return Instance;
            if (!location.Passable) location = location.GetEmptyNearbyElement();
            return CreateNew(location);
        }

        Dictionary<Keys, int> MovementDictionary = new Dictionary<Keys, int>();
        List<Keys> WaitTurnKeys = new List<Keys>();

        private Player(GridElement location)
            : base(location)
        {
            Parent.Creature = this;

            #region Movement Dictionary Set
            MovementDictionary.Add(Keys.W, (int)MapGridLinkage.North);
            MovementDictionary.Add(Keys.Up, (int)MapGridLinkage.North);
            MovementDictionary.Add(Keys.NumPad8, (int)MapGridLinkage.North);

            MovementDictionary.Add(Keys.D, (int)MapGridLinkage.East);
            MovementDictionary.Add(Keys.Right, (int)MapGridLinkage.East);
            MovementDictionary.Add(Keys.NumPad6, (int)MapGridLinkage.East);

            MovementDictionary.Add(Keys.S, (int)MapGridLinkage.South);
            MovementDictionary.Add(Keys.Down, (int)MapGridLinkage.South);
            MovementDictionary.Add(Keys.NumPad2, (int)MapGridLinkage.South);

            MovementDictionary.Add(Keys.A, (int)MapGridLinkage.West);
            MovementDictionary.Add(Keys.Left, (int)MapGridLinkage.West);
            MovementDictionary.Add(Keys.NumPad4, (int)MapGridLinkage.West);

            MovementDictionary.Add(Keys.Q, (int)MapGridLinkage.NorthWest);
            MovementDictionary.Add(Keys.NumPad7, (int)MapGridLinkage.NorthWest);

            MovementDictionary.Add(Keys.E, (int)MapGridLinkage.NorthEast);
            MovementDictionary.Add(Keys.NumPad9, (int)MapGridLinkage.NorthEast);

            MovementDictionary.Add(Keys.Z, (int)MapGridLinkage.SouthWest);
            MovementDictionary.Add(Keys.NumPad1, (int)MapGridLinkage.SouthWest);

            MovementDictionary.Add(Keys.C, (int)MapGridLinkage.SouthEast);
            MovementDictionary.Add(Keys.NumPad3, (int)MapGridLinkage.SouthEast);
            #endregion

            #region Wait Turn List Set
            WaitTurnKeys.Add(Keys.S);
            WaitTurnKeys.Add(Keys.NumPad5);
            #endregion
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var pair in MovementDictionary)            
                if (KeyboardManager.IsKeyDown(pair.Key))
                {
                    var moveTo = Parent.NearbyElements[pair.Value];
                    if (moveTo != null)
                    {
                        if (moveTo.Passable)
                        {
                            Parent.Creature = null;
                            moveTo.Creature = this;
                            Parent = moveTo;
                            
                            IngameTime.Instance.Increment(MovementCost);
                            KeyboardManager.RegisterKeyAction();
                            break;
                        }
                        else if (moveTo.Creature != null)
                        {

                        }
                    }
                }            

            foreach (var key in WaitTurnKeys)            
                if (KeyboardManager.IsKeyDown(key))
                {
                    IngameTime.Instance.Increment(MovementCost);
                    KeyboardManager.RegisterKeyAction();
                    break;
                }
        }
    }
}
